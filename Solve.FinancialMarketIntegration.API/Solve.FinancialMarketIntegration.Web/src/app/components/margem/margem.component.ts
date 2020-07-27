import { Component, OnInit, ViewChild } from '@angular/core';
import { Location, DecimalPipe, DatePipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { conformToMask } from 'text-mask-core';
import { DateTime } from 'luxon';

import { TipoMercado, TicketType } from '../../model/domain/Entity';
import { Margin } from '../../model/domain/Margem';
import { Tipo } from '../../model/domain/Tipo';
import { TicketService } from '../../services/ticket.service';
import { ValidatorService } from '../../services/validator.service';
import { DialogService } from '../../services/dialog.service';
import { AuthService } from '../../services/auth.service';
import { MenuConstants } from '../../infrastructure/MenuConstants';
import { Roles } from '../auth/auth.role';
import { SnackbarService } from '../../services/snackbar.service';

@Component({
  selector: 'app-margem',
  templateUrl: './margem.component.html',
  styleUrls: ['./margem.component.css']
})
export class MargemComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: TicketService,
    public validator: ValidatorService,
    private dialogService: DialogService,
    private authService: AuthService,
    private snackBarService: SnackbarService,
    private datePipe: DatePipe,
    private location: Location
  ) { }

  @ViewChild('thisForm') public frm: NgForm;

  model: Margin;
  TiposCompra: Tipo<number>[];
  TiposMercado: Tipo<number>[];
  TiposLiquidacao: Tipo<number>[];

  isEditing = false;
  canValidate = false;
  ngForm: NgForm;

  numberMask: any;

  decimalPipe = new DecimalPipe('pt');

  validators = {};

  // Init
  ngOnInit() {
    this.model = new Margin();

    this.parametersInit();
    this.validatorsInit();
    this.modelInit();
  }

  setViewOnly() {
    setTimeout(() => {
      if (!this.canEdit()) {
        Object.values(this.frm.controls).forEach(c => {
          c.disable();
        });
      }
    }, 10);
  }

  getTicket(idBoleta: number = null) {
    this.modelInit(idBoleta);
  }

  parametersInit() {
    this.TiposMercado = this.service.ListarTiposMercado();
    this.TiposCompra = this.service.ListarTipoOperacoesMargem();
  }

  validatorsInit() {
    // TODO: utilizar um validador já existente, talvez uma diretiva do próprio angular
    this.validators = {
      portfolioDocument: (value) => this.validator.validateDocument(value),
      marketTypeId: (value) => this.TiposMercado.map(tm => tm.id).includes(value),
      operationValue: (value) => this.coalesce(value).length === 0 || conformToMask(value, this.validator.currencyMask, {}),
      unitPrice: (value) => this.coalesce(value).length === 0 || conformToMask(value, this.validator.currencyMask, {}),
      quotation: (value) => this.coalesce(value).length === 0 || conformToMask(value, this.validator.currencyMask, {}),
      amount: (value) => this.coalesce(value).length === 0 || conformToMask(value, this.validator.decimalMask, {}),
      counterpartDocument: (value) => this.coalesce(value).length === 0 || this.validator.validateDocument(value),
      dueDate: (value) => this.coalesce(value).length === 0
        || DateTime.fromFormat(value, 'dd/MM/yyyy').isValid,
      purchaseDate: (value) => this.coalesce(value).length === 0
        || DateTime.fromFormat(value, 'dd/MM/yyyy').isValid
    };
  }

  modelInit(idBoleta: number = null) {
    const id = idBoleta === null ? +this.route.snapshot.paramMap.get('id') : idBoleta;

    this.model.id = id;

    if (id) {
      this.service.GetMarginTicket(id).subscribe(q => {
        Object.assign(this.model, q);

        this.handleDataFromRepository();

        this.isEditing = true;
        this.canValidate = true;

        this.setViewOnly();
      }, () => this.router.navigate([MenuConstants.Tickets.Margin]));
    } else {
      this.isEditing = false;
      this.model = new Margin();
      this.model.marketTypeId = this.TiposMercado[0].id;
      this.model.operationTypeId = this.TiposCompra[0].id;
      // this.mock();
      // this.handleDataFromRepository();
      // this.calculateOperationValue(null);
    }
  }

  submit() {
    const editing = this.isEditing;

    this.register(this.frm)
      .pipe(catchError(err => {
        this.snackBarService.open(
          err, 'OK', {
            verticalPosition: 'top',
            horizontalPosition: 'right'
          });
        return throwError(err);
      }))
      .subscribe(r => {
        const msg = editing
          ? 'Boleta alterada.'
          : 'Boleta cadastrada.';

        this.snackBarService.open(
          msg, 'OK', {
            verticalPosition: 'top',
            horizontalPosition: 'right'
          });
      }, () => {});
  }

  register(f: NgForm, duplicate: boolean = false) {
    const editing = this.isEditing;

    if (!this.canEdit() && !duplicate) {
      return;
    }

    try {
      const isValid = this.validateForm(f);
      if (isValid) {
        let data = Object.assign({}, this.model);

        data = this.cleanDataForSubmitting(data);

        return this.sendSubmit(data, editing, duplicate);
      }
    } catch (error) {
      return throwError(error);
    }
  }

  forward() {
    if (!this.canEdit()) {
      return;
    }

    this.dialogService.confirm(
      'Encaminhar Boleta',
      'A boleta não poderá mais ser editada, deseja continuar?'
    ).subscribe(result => {
      if (result) {
        this.register(this.frm)
          .pipe(catchError(err => {
            this.snackBarService.open(
              err, 'OK', {
                verticalPosition: 'top',
                horizontalPosition: 'right'
              });
            return throwError(err);
          }))
          .subscribe((margin: Margin) => {
            this.service.ForwardMarginTicket(margin.id).subscribe(res => {
              this.model.statusDescription = res.statusDescription;

              this.setViewOnly();

              this.snackBarService.open(
                'Boleta encaminhada.', 'OK', {
                  verticalPosition: 'top',
                  horizontalPosition: 'right'
                });

              this.getTicket(margin.id);
            });
          }, () => {});
      }
    });
  }

  approve() {
    this.dialogService.confirm(
      'Aprovar Boleta',
      'Deseja aprovar esta boleta?'
    ).subscribe(result => {
      if (result) {
        this.service.Approve(this.model.id, '').subscribe(() => {
          this.snackBarService.open(
            'Boleta atualizada com sucesso.', 'OK', {
              verticalPosition: 'top',
              horizontalPosition: 'right'
            });

          this.getTicket(this.model.id);
        });
      }
    });
  }

  reject() {
    this.dialogService.reject(
      'Reprovar Boleta',
      'Deseja reprovar esta boleta?'
    ).subscribe(result => {
      if (result) {
        this.service.Reject(this.model.id, result.justification).subscribe(() => {
          this.snackBarService.open(
            'Boleta atualizada com sucesso.', 'OK', {
              verticalPosition: 'top',
              horizontalPosition: 'right'
            });
          this.getTicket(this.model.id);
        });
      }
    });
  }

  duplicate() {
    this.register(this.frm, true).subscribe(newTicket => {
      this.snackBarService.open(
        'Boleta duplicada.', 'Visualizar no Monitor', {
          verticalPosition: 'top',
          horizontalPosition: 'right',
          duration: 6000
        }).onAction().subscribe(a => {
          this.router.navigate([MenuConstants.Todos, { boletaId: TicketType.Margin }]);
        });
    });
  }

  // Auxiliares - view
  isRFTituloPrivado() { return this.model.marketTypeId === TipoMercado.RFTituloPrivado; }
  isRFTituloPublico() { return this.model.marketTypeId === TipoMercado.RFTituloPublico; }
  isRendaVariavel()   { return this.model.marketTypeId === TipoMercado.RendaVariavel; }
  isDinheiro()        { return this.model.marketTypeId === TipoMercado.Dinheiro; }

  // Auxiliares - validação
  coalesce(value: any) { return value || ''; }

  getValidators() {
    return this.validators;
  }

  hasError(prop: string) {
    if (!this.canValidate) {
      return false;
    }

    return !this.getValidators()[prop](this.model[prop]);
  }

  getErrors(): string[] {
    const validationErrors = [];
    const validators = this.getValidators();
    Object.keys(validators).forEach(k => {
      if (!validators[k](this.model[k])) {
        validationErrors.push(k);
      }
    });

    return validationErrors;
  }

  validateForm(f: NgForm): boolean {
    Object.values(f.controls).forEach(c => {
      c.markAsTouched();
      c.markAsDirty();
    });

    this.canValidate = true;

    const errors = this.getErrors();
    if (f.invalid || errors.length > 0) {
      throw new Error('Verifique os campos destacados.');
    }

    return true;
  }

  transformDecimal(val: number): number {
    return this.decimalPipe.transform(val, '1.0-8', 'pt-BR') as any;
  }

  formatDocument(val: string): string {
    return this.validator.formatDocument(val);
  }

  transformDateForSubmitting(val: Date) {
    return new Date(val.toString().replace( /(\d{2})\/(\d{2})\/(\d{4})/, '$2/$1/$3'));
  }

  calculateOperationValue(newValue) {
    const amount = this.validator.getCurrency(this.model.amount);
    const unitPrice = this.validator.getCurrency(this.model.unitPrice);
    const operationValue = amount * unitPrice;
    this.model.operationValue = this.transformDecimal(operationValue);
  }

  // Auxiliares - submit
  cleanDataForSubmitting(data: Margin) {
    data.amount = this.validator.getCurrency(data.amount);
    data.operationValue = this.validator.getCurrency(data.operationValue);
    data.unitPrice = this.validator.getCurrency(data.unitPrice);
    data.quotation = this.validator.getCurrency(data.quotation);
    data.portfolioDocument = this.validator.sanitize(data.portfolioDocument);
    data.counterpartDocument = this.validator.sanitize(data.counterpartDocument);

    if (this.isRFTituloPrivado()) {
      data.quotation = null;
      data.asset = null;
      data.counterpartBrokerAccount = null;
      data.securityName = null;
      data.dueDate = this.transformDateForSubmitting(data.dueDate);
      data.purchaseDate = this.transformDateForSubmitting(data.purchaseDate);
    } else if (this.isRFTituloPublico()) {
      data.quotation = null;
      data.asset = null;
      data.counterpartBrokerAccount = null;
      data.securityType = null;
      data.dueDate = this.transformDateForSubmitting(data.dueDate);
      data.purchaseDate = this.transformDateForSubmitting(data.purchaseDate);
    } else if (this.isRendaVariavel()) {
      data.unitPrice = null;
      data.operationValue = null;
      data.securityType = null;
      data.securityCode = null;
      data.dueDate = null;
      data.purchaseDate = null;
    } else if (this.isDinheiro()) {
      data.quotation = null;
      data.asset = null;
      data.unitPrice = null;
      data.securityType = null;
      data.securityCode = null;
      data.dueDate = null;
      data.purchaseDate = null;
      data.counterpartClearingAccount = null;
      data.portfolioClearingAccount = null;
      data.counterpartBrokerAccount = null;
    } else {
      data = null;
    }

    return data;
  }

  handleDataFromRepository() {
    if (this.model.operationValue) { this.model.operationValue = this.transformDecimal(this.model.operationValue); }
    if (this.model.unitPrice) { this.model.unitPrice = this.transformDecimal(this.model.unitPrice); }
    if (this.model.quotation) { this.model.quotation = this.transformDecimal(this.model.quotation); }
    if (this.model.amount) { this.model.amount = this.transformDecimal(this.model.amount); }
    if (this.model.counterpartDocument) { this.model.counterpartDocument = this.formatDocument(this.model.counterpartDocument); }
    if (this.model.portfolioDocument) { this.model.portfolioDocument = this.formatDocument(this.model.portfolioDocument); }
    if (this.model.dueDate) { this.model.dueDate = this.datePipe.transform(this.model.dueDate, 'dd/MM/yyyy') as any; }
    if (this.model.purchaseDate) { this.model.purchaseDate = this.datePipe.transform(this.model.purchaseDate, 'dd/MM/yyyy') as any; }
  }

  sendSubmit(data: Margin, editing: boolean = false, duplicate: boolean = false) {
    const method = (editing && !duplicate)
      ? this.service.UpdateMarginTicket(this.model.id, data)
      : this.service.RegisterMarginTicket(data);

    return method
      .pipe(catchError(err => {
        return throwError('Erro ao cadastrar a boleta.');
      }))
      .pipe(map((margin: Margin) => {
        if (!editing) {
          this.model.id = margin.id;
          this.model.operationDate = margin.operationDate;
          this.model.statusId = margin.statusId;

          this.isEditing = true;

          const url = this.router.createUrlTree([this.model.id], { relativeTo: this.route });
          this.location.go(url.toString());
        }

        return this.model;
      }));
  }

  canEdit() {
    return (!this.model.statusId && !this.model.statusDescription)
      && this.authService.hasRole(Roles.CreateTicketFundQuota);
  }

  canDuplicate() {
    return this.model.id && this.authService.hasRole(Roles.CreateTicketFundQuota);
  }

  canChangeStatus() {
    return this.model.statusId && this.authService.hasRoles(this.model.statusRequiredRoles);
  }

  // Auxiliares - mock
  mock() {
    this.model.amount = 1;
    this.model.unitPrice = 1;
    this.model.operationValue = 1;
    this.model.securityType = 'security type';
    this.model.securityName = 'security name';
    this.model.securityCode = 'security code';
    this.model.dueDate = new Date('2019-03-31T00:00:00');
    this.model.purchaseDate = new Date('2019-02-28T00:00:00');
    this.model.counterpartName = 'counterpart name';
    this.model.counterpartDocument = '04.405.242/0001-16';
    this.model.counterpartClearingAccount = '123';
    this.model.command = 'command';
    this.model.quotation = 1;
    this.model.asset = 'asset';
    this.model.counterpartBrokerAccount = '123';
    this.model.portfolioDocument = '04.405.242/0001-16';
    this.model.portfolioAccount = '123';
    this.model.portfolioClearingAccount = '123';
    this.model.portfolioName = 'portfolio name';
    this.model.annotations = 'annotations';
  }

  isDisabled() {
    return this.model.id != null && this.model.id > 0;
  }
}
