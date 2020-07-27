import { Component, OnInit, ViewChild } from '@angular/core';
import { Location, DecimalPipe, DatePipe } from '@angular/common';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { DateTime } from 'luxon';

import { Tipo } from '../../model/domain/Tipo';
import { TicketService } from '../../services/ticket.service';
import { TicketCetipService } from '../../services/ticketCetip.service';
import { DialogService } from '../../services/dialog.service';
import { AuthService } from '../../services/auth.service';
import { ValidatorService } from '../../services/validator.service';
import { Cetip } from '../../model/domain/Cetip';
import { TipoOperacao, TicketType } from '../../model/domain/Entity';
import { conformToMask } from 'text-mask-core';
import { MenuConstants } from '../../infrastructure/MenuConstants';
import { Roles } from '../auth/auth.role';
import { SnackbarService } from '../../services/snackbar.service';

@Component({
  selector: 'app-cetip',
  templateUrl: './cetip.component.html',
  styleUrls: ['./cetip.component.css']
})
export class CetipComponent implements OnInit {

  public model: Cetip = null;

  isEditing = false;
  canValidate = false;
  TipoOperacao = TipoOperacao;

  decimalPipe = new DecimalPipe('pt');

  @ViewChild('thisForm') public frm: NgForm;

  TiposCompra: Tipo<number>[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: TicketService,
    public validator: ValidatorService,
    protected ticketCetipService: TicketCetipService,
    private datePipe: DatePipe,
    private authService: AuthService,
    private dialogService: DialogService,
    private snackBarService: SnackbarService,
    private location: Location
  ) { }

  validators = {
    portfolioDocument: (value) => this.validator.validateDocument(value),
    operationTypeId: (value) => [TipoOperacao.Compra, TipoOperacao.Venda].includes(value),
    isSecondaryMarket: (value) => [true, false].includes(value),
    operationValue: (value) => this.coalesce(value).length === 0
      || conformToMask(value, this.validator.currencyMaskDoubleDecimal, {}),
    amount: (value) => this.coalesce(value).length === 0
      || conformToMask(value, this.validator.decimalMask, {}),
    unitPrice: (value) => this.coalesce(value).length === 0
      || conformToMask(value, this.validator.currencyMask, {}),
    counterpartDocument: (value) => this.coalesce(value).length === 0
      || this.validator.validateDocument(value),
    expirationDate: (value) => value != null &&
      this.model.operationDate != null &&
      new Date(value.toString().replace(/(\d{2})\/(\d{2})\/(\d{4})/, '$2/$1/$3'))
      >= new Date(this.model.operationDate.toString().replace( /(\d{2})\/(\d{2})\/(\d{4})/, '$2/$1/$3')) &&
      (this.coalesce(value).length === 0
        || DateTime.fromFormat(value, 'dd/MM/yyyy').isValid),
    issueDate: (value) => this.coalesce(value).length === 0
      || DateTime.fromFormat(value, 'dd/MM/yyyy').isValid
  };

  ngOnInit() {
    this.getTicket();
  }

  setViewOnly(): void {
    setTimeout(() => {
      if (!this.canEdit()) {
        Object.values(this.frm.controls).forEach(c => {
          c.disable();
        });
      }
    }, 10);
  }

  getTicket(idBoleta: number = null): void {
    this.model = new Cetip();
    this.TiposCompra = this.service.ListarTipoOperacoesPadrao();

    const id = idBoleta === null ? +this.route.snapshot.paramMap.get('id') : idBoleta;
    this.model.id = id;
    this.model.command = '0';

    if (id) {
      this.service.GetPrivateFixedIncomeTicket(id).subscribe(q => {
        Object.assign(this.model, q);

        this.model.portfolioDocument = this.validator.formatDocument(this.model.portfolioDocument);

        if (this.model.counterpartDocument) {
          this.model.counterpartDocument = this.validator.formatDocument(this.model.counterpartDocument);
        }

        this.model.operationDate = new Date(this.model.operationDate.toString().replace( /(\d{2})\/(\d{2})\/(\d{4})/, '$2/$1/$3'));
        this.model.operationDate.setHours(0, 0, 0, 0);

        if (this.model.acquisitionDate != null) {
          this.model.acquisitionDate = this.datePipe.transform(this.model.acquisitionDate, 'dd/MM/yyyy') as any;
        }

        if (this.model.expirationDate != null) {
          this.model.expirationDate = this.datePipe.transform(this.model.expirationDate, 'dd/MM/yyyy') as any;
        }

        if (this.model.issueDate != null) {
          this.model.issueDate = this.datePipe.transform(this.model.issueDate, 'dd/MM/yyyy') as any;
        }

        if (this.model.amount != null) {
          this.model.amount = this.decimalPipe.transform(this.model.amount, '1.0-4', 'pt-BR') as any;
        }

        if (this.model.issueFee != null) {
          this.model.issueFee = this.decimalPipe.transform(this.model.issueFee, '1.3-6', 'pt-BR') as any;
        }

        this.model.operationValue = this.decimalPipe.transform(this.model.operationValue, '1.0-8', 'pt-BR') as any;
        this.model.unitPrice = this.decimalPipe.transform(this.model.unitPrice, '1.0-8', 'pt-BR') as any;

        this.isEditing = true;
        this.canValidate = true;

        this.setViewOnly();
      }, () => this.router.navigate([MenuConstants.Tickets.PrivateFixedIncome]));
    } else {
      if (!this.model.statusId) {
        const start = new Date();
        start.setHours(0, 0, 0, 0);

        this.model.operationDate = start;
      }
    }
  }

  submit(): void {
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
      }, () => this.router.navigate(['/cetip']));
  }

  register(f: NgForm, duplicate: boolean = false) {
    const editing = this.isEditing;

    if (!this.canEdit() && !duplicate) {
      return;
    }

    Object.values(f.controls).forEach(c => {
      c.markAsTouched();
      c.updateValueAndValidity();
    });

    this.canValidate = true;

    const errors = this.getErrors();

    if (f.invalid || errors.length > 0) {
      return throwError('Verifique os campos destacados.');
    }

    const data = Object.assign({}, this.model);

    data.operationDate = null;
    data.statusId = null;
    data.id = null;

    data.amount = this.validator.getCurrency(data.amount);
    data.operationValue = this.validator.getCurrency(data.operationValue);
    data.unitPrice = this.validator.getCurrency(data.unitPrice);
    data.issueFee = this.validator.getCurrency(data.issueFee);
    data.portfolioDocument = this.validator.sanitize(data.portfolioDocument);
    data.counterpartDocument = this.validator.sanitize(data.counterpartDocument);

    if (data.acquisitionDate != null) {
      data.acquisitionDate = new Date(data.acquisitionDate.toString().replace( /(\d{2})\/(\d{2})\/(\d{4})/, '$2/$1/$3'));
    }

    if (data.expirationDate != null) {
      data.expirationDate = new Date(data.expirationDate.toString().replace( /(\d{2})\/(\d{2})\/(\d{4})/, '$2/$1/$3'));
    }

    if (data.issueDate) {
      data.issueDate = new Date(data.issueDate.toString().replace( /(\d{2})\/(\d{2})\/(\d{4})/, '$2/$1/$3'));
    }

    if (data.operationDate) {
      data.operationDate = new Date(data.operationDate.toString().replace( /(\d{2})\/(\d{2})\/(\d{4})/, '$2/$1/$3'));
    }

    if (this.isEscritoTermo()) {
      data.assetCode = null;
    } else {
      data.objectAction = null;
    }

    if (f.invalid) {
      return throwError('Verifique os campos destacados.');
    }

    const method = (editing && !duplicate)
      ? this.service.UpdatePrivateFixedIncomeTicket(this.model.id, data)
      : this.service.RegisterPrivateFixedIncomeTicket(data);

    return method
      .pipe(catchError(err => {
        return throwError('Erro ao cadastrar a boleta.');
      }))
      .pipe(map((fixedIncome: Cetip) => {
        if (!editing) {
          this.model.id = fixedIncome.id;
          this.model.operationDate = fixedIncome.operationDate;
          this.model.statusId = fixedIncome.statusId;

          this.isEditing = true;

          const url = this.router.createUrlTree([this.model.id], { relativeTo: this.route });
          this.location.go(url.toString());
        }

        return this.model;
      }));

  }

  calculateOperationValue(): void {
    const amount = this.validator.getCurrency(this.model.amount);
    const unitPrice = this.validator.getCurrency(this.model.unitPrice);
    const operationValue = amount * unitPrice;
    this.model.operationValue = this.transformDecimal(operationValue);
  }

  isOperationValueModified(): boolean {
    const amount = this.validator.getCurrency(this.model.amount);
    const unitPrice = this.validator.getCurrency(this.model.unitPrice);
    const operationValue = amount * unitPrice;

    return this.validator.getCurrency(this.model.operationValue) !== operationValue;
  }

  transformDecimal(val: number): number {
    return this.decimalPipe.transform(val, '1.0-8', 'pt-BR') as any;
  }

  forward(): void {
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
          .subscribe((fixedIncome: Cetip) => {
            this.service.ForwardPrivateFixedIncomeTicket(fixedIncome.id).subscribe(res => {
              this.model.statusDescription = res.statusDescription;

              this.setViewOnly();

              this.snackBarService.open(
                'Boleta encaminhada.', 'OK', {
                  verticalPosition: 'top',
                  horizontalPosition: 'right'
                });

              this.getTicket(fixedIncome.id);
            });
          }, () => {});
        }
    });

  }

  approve(): void {
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

  reject(): void {
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

  coalesce(value: any) { return value || ''; }

  isVenda(): boolean {
    return this.model.operationTypeId === TipoOperacao.Venda;
  }

  /**
   * @description Is asset type equals 'termo'
   */
  isEscritoTermo(): boolean {
    return this.model.assetType != null && this.model.assetType.toLowerCase() === 'termo';
  }

  hasError(prop: string): boolean {
    if (!this.canValidate) {
      return false;
    }

    return !this.validators[prop](this.model[prop]);
  }

  getErrors(): string[] {
    const validationErrors = [];

    Object.keys(this.validators).forEach(k => {
      if (!this.validators[k](this.model[k])) {
        validationErrors.push(k);
      }
    });

    return validationErrors;
  }

  duplicate(): void {
    this.register(this.frm, true).subscribe(newTicket => {
      this.snackBarService.open(
        'Boleta duplicada.', 'Visualizar no Monitor', {
          verticalPosition: 'top',
          horizontalPosition: 'right',
          duration: 6000
        }).onAction().subscribe(a => {
          this.router.navigate([MenuConstants.Todos, { boletaId: TicketType.CETIP }]);
        });
    });
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
}
