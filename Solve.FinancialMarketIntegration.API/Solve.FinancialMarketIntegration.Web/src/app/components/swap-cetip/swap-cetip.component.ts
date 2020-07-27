import { Component, OnInit, ViewChild } from '@angular/core';
import { Location, DecimalPipe, DatePipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { conformToMask } from 'text-mask-core';
import { DateTime } from 'luxon';

import { SwapCetip } from '../../model/domain/SwapCetip';
import { TipoJuros, TicketType } from '../../model/domain/Entity';
import { TicketService } from '../../services/ticket.service';
import { ValidatorService } from '../../services/validator.service';
import { SnackbarService } from '../../services/snackbar.service';
import { DialogService } from '../../services/dialog.service';
import { AuthService } from '../../services/auth.service';
import { Tipo } from '../../model/domain/Tipo';
import { MenuConstants } from '../../infrastructure/MenuConstants';
import { Roles } from '../auth/auth.role';

@Component({
  selector: 'app-swap-cetip',
  templateUrl: './swap-cetip.component.html',
  styleUrls: ['./swap-cetip.component.css']
})
export class SwapCetipComponent implements OnInit {
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

  ListaTipoJuros: Tipo<number>[];
  ListaBaseIndexador: Tipo<number>[];

  model: SwapCetip = null;
  canValidate = false;
  isEditing = false;

  @ViewChild('thisForm') public frm: NgForm;

  validators = {
    portfolioDocument: (value) => this.validator.validateDocument(value),
    operationValue: (value) => this.coalesce(value).length === 0
      || conformToMask(value, this.validator.currencyMask, {}),
    activeIndexPercent: (value) => this.coalesce(value).length === 0
      || conformToMask(value, this.validator.decimalMask, {}),
    activeIndexTax: (value) => this.coalesce(value).length === 0
      || conformToMask(value, this.validator.currencyMask, {}),
    counterpartDocument: (value) => this.coalesce(value).length === 0
      || this.validator.validateDocument(value),
    passiveIndexPercent: (value) => this.coalesce(value).length === 0
      || conformToMask(value, this.validator.decimalMask, {}),
    passiveIndexTax: (value) => this.coalesce(value).length === 0
      || conformToMask(value, this.validator.currencyMask, {}),
    activeInterestType: (value) => [TipoJuros.Exponencial, TipoJuros.Linear].includes(value),
    passiveInterestType: (value) => [TipoJuros.Exponencial, TipoJuros.Linear].includes(value),
    activeIndexBase: (value) => [1, 2].includes(value),
    passiveIndexBase: (value) => [1, 2].includes(value),
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
    this.model = new SwapCetip();
    this.ListaBaseIndexador = this.service.ListarBasesIndexador();
    this.ListaTipoJuros = this.service.ListarTipoJuros();

    const id = idBoleta === null ? +this.route.snapshot.paramMap.get('id') : idBoleta;
    this.model.id = id;
    this.model.command = '0';

    if (id) {
      this.service.GetSwapCetipTicket(id).subscribe(q => {
        Object.assign(this.model, q);

        this.model.portfolioDocument = this.validator.formatDocument(this.model.portfolioDocument);
        this.model.counterpartDocument = this.validator.formatDocument(this.model.counterpartDocument);

        this.model.operationValue = this.decimalPipe.transform(this.model.operationValue, '1.0-8', 'pt-BR') as any;

        this.model.activeIndexPercent = this.decimalPipe.transform(this.model.activeIndexPercent, '1.2-2', 'pt-BR') as any;
        this.model.activeIndexTax = this.decimalPipe.transform(this.model.activeIndexTax, '1.0-8', 'pt-BR') as any;

        this.model.passiveIndexPercent = this.decimalPipe.transform(this.model.passiveIndexPercent, '1.2-2', 'pt-BR') as any;
        this.model.passiveIndexTax = this.decimalPipe.transform(this.model.passiveIndexTax, '1.0-8', 'pt-BR') as any;

        this.model.expirationDate = this.datePipe.transform(this.model.expirationDate, 'dd/MM/yyyy') as any;

        this.isEditing = true;
        this.canValidate = true;

        this.setViewOnly();
      }, () => this.router.navigate([MenuConstants.Tickets.SwapCetip]));
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

    data.operationValue = this.validator.getCurrency(data.operationValue);
    data.activeIndexTax = this.validator.getCurrency(data.activeIndexTax);
    data.activeIndexPercent = this.validator.getCurrency(data.activeIndexPercent);
    data.passiveIndexTax = this.validator.getCurrency(data.passiveIndexTax);
    data.passiveIndexPercent = this.validator.getCurrency(data.passiveIndexPercent);

    data.portfolioDocument = this.validator.sanitize(data.portfolioDocument);
    data.counterpartDocument = this.validator.sanitize(data.counterpartDocument);

    data.expirationDate = new Date(data.expirationDate.toString().replace( /(\d{2})\/(\d{2})\/(\d{4})/, '$2/$1/$3'));

    const method = (editing && !duplicate)
      ? this.service.UpdateSwapCetipTicket(this.model.id, data)
      : this.service.RegisterSwapCetipTicket(data);

    return method
      .pipe(catchError(err => {
        return throwError('Erro ao cadastrar a boleta.');
      }))
      .pipe(map((swapCetip: SwapCetip) => {
        if (!this.isEditing) {
          this.model.id = swapCetip.id;
          this.model.operationDate = swapCetip.operationDate;
          this.model.statusId = swapCetip.statusId;

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
          .subscribe((swapCetip: SwapCetip) => {
            this.service.ForwardSwapCetipTicket(swapCetip.id).subscribe(res => {
              this.model.statusDescription = res.statusDescription;

              this.setViewOnly();

              this.snackBarService.open(
                'Boleta Encaminhada', 'OK', {
                  verticalPosition: 'top',
                  horizontalPosition: 'right'
                });

              // this.getTicket();
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

          this.getTicket();
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
          this.getTicket();
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
          this.router.navigate([MenuConstants.Todos, { boletaId: TicketType.SwapCetip }]);
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
