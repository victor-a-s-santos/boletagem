import { Component, OnInit, ViewChild } from '@angular/core';
import { Location, DecimalPipe, DatePipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { conformToMask } from 'text-mask-core';
import { DateTime } from 'luxon';

import { Futures } from '../../model/domain/Futures';
import { ValidatorService } from '../../services/validator.service';
import { DialogService } from '../../services/dialog.service';
import { AuthService } from '../../services/auth.service';
import { Tipo } from '../../model/domain/Tipo';
import { TicketService } from '../../services/ticket.service';
import { TipoOperacao, TicketType } from '../../model/domain/Entity';
import { MenuConstants } from '../../infrastructure/MenuConstants';
import { Roles } from '../auth/auth.role';
import { SnackbarService } from '../../services/snackbar.service';

@Component({
  selector: 'app-futuros',
  templateUrl: './futuros.component.html',
  styleUrls: ['./futuros.component.scss']
})
export class FuturosComponent implements OnInit {

  model: Futures = null;
  canValidate = false;
  isEditing = false;
  TiposCompra: Tipo<number>[];

  history: any[];

  @ViewChild('thisForm') public frm: NgForm;

  constructor(
    private service: TicketService,
    public validator: ValidatorService,
    private snackBarService: SnackbarService,
    private dialogService: DialogService,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private datePipe: DatePipe,
    private decimalPipe: DecimalPipe,
    private location: Location
  ) { }

  validators = {
    portfolioDocument: (value) => this.validator.validateDocument(value),
    brokerDocument: (value) => this.validator.validateDocument(value),
    operationTypeId: (value) => [TipoOperacao.Compra, TipoOperacao.Venda].includes(value),
    unitPrice: (value) => this.coalesce(value).length === 0
      || conformToMask(value, this.validator.currencyMask, {}),
    percentageDiscount: (value) => this.coalesce(value).length === 0
      || conformToMask(value, this.validator.decimalMask, {}),
    tradingDate: (value) => this.coalesce(value).length === 0
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
    this.model = new Futures();
    this.TiposCompra = this.service.ListarTipoOperacoesPadrao();

    const id = idBoleta === null ? +this.route.snapshot.paramMap.get('id') : idBoleta;
    this.model.id = id;
    this.model.operationTypeId = TipoOperacao.Compra;

    if (id) {
      this.service.GetFuturesTicket(id).subscribe(q => {
        Object.assign(this.model, q);

        this.model.portfolioDocument = this.validator.formatDocument(this.model.portfolioDocument);
        this.model.brokerDocument = this.validator.formatDocument(this.model.brokerDocument);

        this.model.percentageDiscount = this.decimalPipe.transform(this.model.percentageDiscount, '1.0-8', 'pt-BR') as any;

        this.model.unitPrice = this.decimalPipe.transform(this.model.unitPrice, '1.0-8', 'pt-BR') as any;

        this.model.tradingDate = this.datePipe.transform(this.model.tradingDate, 'dd/MM/yyyy') as any;

        this.isEditing = true;
        this.canValidate = true;

        this.setViewOnly();
      }, () => this.router.navigate([MenuConstants.Tickets.Futures]));
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
    data.percentageDiscount = this.validator.getCurrency(data.percentageDiscount);
    data.unitPrice = this.validator.getCurrency(data.unitPrice);
    data.portfolioDocument = this.validator.sanitize(data.portfolioDocument);
    data.brokerDocument = this.validator.sanitize(data.brokerDocument);

    data.tradingDate = new Date(data.tradingDate.toString().replace( /(\d{2})\/(\d{2})\/(\d{4})/, '$2/$1/$3'));

    const method = (editing && !duplicate)
      ? this.service.UpdateFuturesTicket(this.model.id, data)
      : this.service.RegisterFuturesTicket(data);

    return method
      .pipe(catchError(err => {
        return throwError('Erro ao cadastrar a boleta.');
      }))
      .pipe(map((future: Futures) => {
        if (!editing) {
          this.model.id = future.id;
          this.model.operationDate = future.operationDate;
          this.model.statusId = future.statusId;

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
          .subscribe((future: Futures) => {
            this.service.ForwardFuturesTicket(future.id).subscribe(res => {
              this.model.statusDescription = res.statusDescription;

              this.setViewOnly();

              this.snackBarService.open(
                'Boleta encaminhada.', 'OK', {
                  verticalPosition: 'top',
                  horizontalPosition: 'right'
                });

              this.getTicket(future.id);
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

  reject(): void {
    this.dialogService.reject(
      'Reprovar Boleta',
      'Deseja reprovar esta boleta?'
    ).subscribe(result => {
      if (result) {
        this.service.Reject(this.model.id, result.justification).subscribe(() => {
          this.snackBarService.open('Boleta atualizada com sucesso.', 'OK');
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
          this.router.navigate([MenuConstants.Todos, { boletaId: TicketType.Futures }]);
        });
    });
  }

  coalesce(value: any) { return value || ''; }

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
