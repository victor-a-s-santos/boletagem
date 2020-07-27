import { Location, DecimalPipe, DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { conformToMask } from 'text-mask-core';
import { of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { DateTime } from 'luxon';

import { TicketService } from '../../services/ticket.service';
import { ValidatorService } from '../../services/validator.service';
import { DialogService } from '../../services/dialog.service';
import { AuthService } from '../../services/auth.service';
import { SnackbarService } from '../../services/snackbar.service';
import { TipoOperacao, TipoLiquidacao, TicketType } from '../../model/domain/Entity';
import { Selic } from '../../model/domain/Selic';
import { Tipo } from '../../model/domain/Tipo';
import { MenuConstants } from '../../infrastructure/MenuConstants';
import { Roles } from '../auth/auth.role';

@Component({
  selector: 'app-selic',
  templateUrl: './selic.component.html',
  styleUrls: ['./selic.component.css']
})
export class SelicComponent implements OnInit {

  @ViewChild('thisForm') public frm: NgForm;

  public model: Selic = null;
  canValidate = false;
  TipoOperacao = TipoOperacao;
  isEditing = false;

  TiposCompra: Tipo<number>[];

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

  validators = {
    portfolioDocument: (value) => this.validator.validateDocument(value),
    operationTypeId: (value) => [TipoOperacao.Compra, TipoOperacao.Venda].includes(value),
    operationValue: (value) => this.coalesce(value).length === 0
      || conformToMask(value, this.validator.currencyMask, {}),
    amount: (value) => this.coalesce(value).length === 0
      || conformToMask(value, this.validator.decimalMask, {}),
    unitPrice: (value) => this.coalesce(value).length === 0
      || conformToMask(value, this.validator.currencyMask, {}),
    counterpartDocument: (value) => this.coalesce(value).length === 0
      || this.validator.validateDocument(value),
    settlementDate: (value) => this.coalesce(value).length === 0
      || DateTime.fromFormat(value, 'dd/MM/yyyy').isValid,
    expirationDate: (value) => this.coalesce(value).length === 0
      || DateTime.fromFormat(value, 'dd/MM/yyyy').isValid,
    acquisitionDate: (value) => this.coalesce(value).length === 0
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
    this.model = new Selic();
    this.TiposCompra = this.service.ListarTipoOperacoesPadrao();

    const id = idBoleta === null ? +this.route.snapshot.paramMap.get('id') : idBoleta;
    this.model.id = id;
    this.model.command = '0';

    if (id) {
      this.service.GetPublicFixedIncomeTicket(id).subscribe(q => {
        Object.assign(this.model, q);

        this.model.portfolioDocument = this.validator.formatDocument(this.model.portfolioDocument);

        if (this.model.counterpartDocument) {
          this.model.counterpartDocument = this.validator.formatDocument(this.model.counterpartDocument);
        }

        this.model.operationValue = this.decimalPipe.transform(this.model.operationValue, '1.0-8', 'pt-BR') as any;
        this.model.unitPrice = this.decimalPipe.transform(this.model.unitPrice, '1.0-8', 'pt-BR') as any;

        if (this.model.amount) {
          this.model.amount = this.decimalPipe.transform(this.model.amount, '1.0-8', 'pt-BR') as any;
        }

        this.model.acquisitionDate = this.datePipe.transform(this.model.acquisitionDate, 'dd/MM/yyyy') as any;
        this.model.settlementDate = this.datePipe.transform(this.model.settlementDate, 'dd/MM/yyyy') as any;
        this.model.expirationDate = this.datePipe.transform(this.model.expirationDate, 'dd/MM/yyyy') as any;

        this.isEditing = true;
        this.canValidate = true;

        this.setViewOnly();
      }, () => this.router.navigate([MenuConstants.Tickets.PublicFixedIncome]));
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
      }, () => {});
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
    data.portfolioDocument = this.validator.sanitize(data.portfolioDocument);
    data.counterpartDocument = this.validator.sanitize(data.counterpartDocument);

    if (data.acquisitionDate != null) {
      data.acquisitionDate = new Date(data.acquisitionDate.toString().replace( /(\d{2})\/(\d{2})\/(\d{4})/, '$2/$1/$3'));
    }
    if (data.settlementDate != null) {
      data.settlementDate = new Date(data.settlementDate.toString().replace( /(\d{2})\/(\d{2})\/(\d{4})/, '$2/$1/$3'));
    }
    if (data.expirationDate != null) {
      data.expirationDate = new Date(data.expirationDate.toString().replace( /(\d{2})\/(\d{2})\/(\d{4})/, '$2/$1/$3'));
    }

    const method = (editing && !duplicate)
      ? this.service.UpdatePublicFixedIncomeTicket(this.model.id, data)
      : this.service.RegisterPublicFixedIncomeTicket(data);

    return method
      .pipe(catchError(err => {
        return throwError('Erro ao cadastrar a boleta.');
      }))
      .pipe(map((fixedIncome: Selic) => {
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
        .subscribe((fixedIncome: Selic) => {
          this.service.ForwardPublicFixedIncomeTicket(fixedIncome.id).subscribe(res => {
            this.model.statusDescription = res.statusDescription;

            Object.values(this.frm.controls).forEach(c => {
              c.disable();
            });

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

  calculateOperationValue() {
    const amount = this.validator.getCurrency(this.model.amount);
    const unitPrice = this.validator.getCurrency(this.model.unitPrice);
    const operationValue = amount * unitPrice;
    this.model.operationValue = this.transformDecimal(operationValue);
  }

  isOperationValueModified() {
    const amount = this.validator.getCurrency(this.model.amount);
    const unitPrice = this.validator.getCurrency(this.model.unitPrice);
    const operationValue = amount * unitPrice;

    return this.validator.getCurrency(this.model.operationValue) !== operationValue;
  }

  transformDecimal(val: number): number {
    return this.decimalPipe.transform(val, '1.0-8', 'pt-BR') as any;
  }

  duplicate() {
    this.register(this.frm, true).subscribe(newTicket => {
      this.snackBarService.open(
        'Boleta duplicada.', 'Visualizar no Monitor', {
          verticalPosition: 'top',
          horizontalPosition: 'right',
          duration: 6000
        }).onAction().subscribe(a => {
          this.router.navigate([MenuConstants.Todos, { boletaId: TicketType.SELIC }]);
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

  isTermo() {
    return this.model.settlementTypeId === TipoLiquidacao.Term;
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
