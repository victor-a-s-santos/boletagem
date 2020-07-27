import { Component, OnInit, ViewChild } from '@angular/core';
import { Location, DecimalPipe, DatePipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { conformToMask } from 'text-mask-core';
import { DateTime } from 'luxon';

import { Contracted } from '../../model/domain/Contracted';
import { TipoOperacao, TicketType } from '../../model/domain/Entity';
import { TicketService } from '../../services/ticket.service';
import { ValidatorService } from '../../services/validator.service';
import { DialogService } from '../../services/dialog.service';
import { AuthService } from '../../services/auth.service';
import { Tipo } from '../../model/domain/Tipo';
import { MenuConstants } from '../../infrastructure/MenuConstants';
import { Roles } from '../auth/auth.role';
import { SnackbarService } from '../../services/snackbar.service';

@Component({
  selector: 'app-compromissada-compra',
  templateUrl: './compromissada-compra.component.html',
  styleUrls: ['./compromissada-compra.component.css']
})
export class CompromissadaCompraComponent implements OnInit {

  public model: Contracted = null;
  TiposLiquidacao: Tipo<number>[];
  canValidate = false;
  isEditing = false;

  constructor(
    private service: TicketService,
    public validator: ValidatorService,
    private dialogService: DialogService,
    private authService: AuthService,
    private snackBarService: SnackbarService,
    private router: Router,
    private route: ActivatedRoute,
    private datePipe: DatePipe,
    private decimalPipe: DecimalPipe,
    private location: Location
  ) { }

  @ViewChild('thisForm') public frm: NgForm;

  validators: {[x: string]: (value: any) => boolean } = {
    portfolioDocument: (value) => this.validator.validateDocument(value),
    operationTypeId: (value) => [TipoOperacao.Compra, TipoOperacao.Venda].includes(value),
    valueOutward: (value) => this.coalesce(value).length === 0
      || conformToMask(value, this.validator.currencyMask, {}),
    valueReturn: (value) => this.coalesce(value).length === 0
      || conformToMask(value, this.validator.currencyMask, {}),
    unitPriceOutward: (value) => this.coalesce(value).length === 0
      || conformToMask(value, this.validator.currencyMask, {}),
    unitPriceReturn: (value) => this.coalesce(value).length === 0
      || conformToMask(value, this.validator.currencyMask, {}),
    operationValue: (value) => this.coalesce(value).length === 0
      || conformToMask(value, this.validator.currencyMask, {}),
    counterpartDocument: (value) => this.coalesce(value).length === 0
      || this.validator.validateDocument(value),
    returnDate: (value) => this.coalesce(value).length === 0
      || DateTime.fromFormat(value, 'dd/MM/yyyy').isValid,
    expirationDate: (value) => this.coalesce(value).length === 0
      || DateTime.fromFormat(value, 'dd/MM/yyyy').isValid
  };

  ngOnInit() {
    this.getTicket();
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
    this.model = new Contracted();
    this.TiposLiquidacao = this.service.ListarTiposLiquidacaoCompromissada();

    const id = idBoleta === null ? +this.route.snapshot.paramMap.get('id') : idBoleta;
    this.model.id = id;
    this.model.operationTypeId = TipoOperacao.Compra;
    this.model.command = '0';

    if (id) {
      this.service.GetContractedTicket(id).subscribe(q => {
        Object.assign(this.model, q);

        this.model.portfolioDocument = this.validator.formatDocument(this.model.portfolioDocument);

        if (this.model.counterpartDocument) {
          this.model.counterpartDocument = this.validator.formatDocument(this.model.counterpartDocument);
        }

        this.model.valueOutward = this.decimalPipe.transform(this.model.valueOutward, '1.0-8', 'pt-BR') as any;
        this.model.valueReturn = this.decimalPipe.transform(this.model.valueReturn, '1.0-8', 'pt-BR') as any;

        this.model.unitPriceOutward = this.decimalPipe.transform(this.model.unitPriceOutward, '1.0-8', 'pt-BR') as any;
        this.model.unitPriceReturn = this.decimalPipe.transform(this.model.unitPriceReturn, '1.0-8', 'pt-BR') as any;

        if (this.model.operationValue) {
          this.model.operationValue = this.decimalPipe.transform(this.model.operationValue, '1.0-8', 'pt-BR') as any;
        }

        if (this.model.issueTax) {
          this.model.issueTax = this.decimalPipe.transform(this.model.issueTax, '1.0-8', 'pt-BR') as any;
        }

        this.model.returnDate = this.datePipe.transform(this.model.returnDate, 'dd/MM/yyyy') as any;
        this.model.expirationDate = this.datePipe.transform(this.model.expirationDate, 'dd/MM/yyyy') as any;

        this.isEditing = true;
        this.canValidate = true;

        this.setViewOnly();
      }, () => this.router.navigate([MenuConstants.Tickets.Contracted]));
    } else {

      if (!this.model.statusId) {
        this.model.operationDate = new Date();
      }
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
      }, () => { });
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

    data.valueOutward = this.validator.getCurrency(data.valueOutward);
    data.valueReturn = this.validator.getCurrency(data.valueReturn);
    data.unitPriceOutward = this.validator.getCurrency(data.unitPriceOutward);
    data.unitPriceReturn = this.validator.getCurrency(data.unitPriceReturn);
    data.amount = this.validator.getCurrency(data.amount);
    data.operationValue = this.validator.getCurrency(data.operationValue);
    data.issueTax = this.validator.getNumeric(data.issueTax);
    data.portfolioDocument = this.validator.sanitize(data.portfolioDocument);
    data.counterpartDocument = this.validator.sanitize(data.counterpartDocument);

    data.returnDate = new Date(data.returnDate.toString().replace( /(\d{2})\/(\d{2})\/(\d{4})/, '$2/$1/$3'));
    data.expirationDate = new Date(data.expirationDate.toString().replace( /(\d{2})\/(\d{2})\/(\d{4})/, '$2/$1/$3'));

    const method = (editing && !duplicate)
      ? this.service.UpdateContractedTicket(this.model.id, data)
      : this.service.RegisterContractedTicket(data);

    return method
      .pipe(catchError(err => {
        return throwError('Erro ao cadastrar a boleta.');
      }))
      .pipe(map((contracted: Contracted) => {
        if (!editing) {
          this.model.id = contracted.id;
          this.model.operationDate = contracted.operationDate;
          this.model.statusId = contracted.statusId;

          this.isEditing = true;

          const url = this.router.createUrlTree([this.model.id], { relativeTo: this.route });
          this.location.go(url.toString());
        }

        return this.model;
      }));
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
          .subscribe((contracted: Contracted) => {
            this.service.ForwardContractedTicket(contracted.id).subscribe(res => {
              this.model.statusDescription = res.statusDescription;

              this.setViewOnly();

              this.snackBarService.open(
                'Boleta encaminhada.', 'OK', {
                  verticalPosition: 'top',
                  horizontalPosition: 'right'
                });

              this.getTicket(contracted.id);
            });
          }, () => { });
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
          this.router.navigate([MenuConstants.Todos, { boletaId: TicketType.Contracted }]);
        });
    });
  }

  coalesce(value: any) { return value || ''; }

  hasError(prop: string) {
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

  isVenda() {
    return this.model.operationTypeId === TipoOperacao.Venda;
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
