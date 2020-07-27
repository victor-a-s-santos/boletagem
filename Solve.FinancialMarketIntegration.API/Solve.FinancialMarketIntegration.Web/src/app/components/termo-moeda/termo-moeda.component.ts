import { Component, OnInit, ViewChild } from '@angular/core';
import { Location, DecimalPipe, DatePipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { DateTime } from 'luxon';

import { DadosCarteiraComponent } from '../dados-carteira/dados-carteira.component';
import { CurrencyTerm } from '../../model/domain/CurrencyTerm';
import { TicketService } from '../../services/ticket.service';
import { ValidatorService } from '../../services/validator.service';
import { DialogService } from '../../services/dialog.service';
import { Tipo } from '../../model/domain/Tipo';
import { TermoMoedaService } from '../../services/termo-moeda.service';
import { AuthService } from '../../services/auth.service';
import { MenuConstants } from '../../infrastructure/MenuConstants';
import { TicketType } from '../../model/domain/Entity';
import { Roles } from '../auth/auth.role';
import { SnackbarService } from '../../services/snackbar.service';

@Component({
  selector: 'app-termo-moeda',
  templateUrl: './termo-moeda.component.html',
  styleUrls: ['./termo-moeda.component.css'],
})
export class TermoMoedaComponent implements OnInit {
  decimalPipe = new DecimalPipe('pt');

  @ViewChild('thisForm') frm: NgForm;
  @ViewChild('carteira') carteiraComponent: DadosCarteiraComponent;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: TicketService,
    public validator: ValidatorService,
    private datePipe: DatePipe,
    private dialogService: DialogService,
    private snackBarService: SnackbarService,
    private termoService: TermoMoedaService,
    private authService: AuthService,
    private location: Location
  ) { }

  validators: {[x: string]: (value: any) => boolean } = {
    expirationDate: (value) => this.coalesce(value).length === 0
      || DateTime.fromFormat(value, 'dd/MM/yyyy').isValid
  };

  isEditing = false;
  canValidate = false;
  model: CurrencyTerm = CurrencyTerm.Create();

  Moedas: Tipo<number>[] = null;
  TiposOperacao: Tipo<number>[] = null;

  listaCotacaovencimento: Tipo<number>[];

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
    this.model = new CurrencyTerm();

    this.model.portfolio = this.carteiraComponent.carteira;

    this.TiposOperacao = this.termoService.ListarTipoOperacaoTermoMoeda();
    this.listaCotacaovencimento = this.termoService.ListaCotacaoVencimento();
    this.Moedas = this.termoService.ListarMoedas();

    const id = idBoleta === null ? +this.route.snapshot.paramMap.get('id') : idBoleta;

    if (id) {
      this.service.GetCurrencyTerm(id).subscribe(q => {
        Object.assign(this.model, q);

        this.carteiraComponent.carteira = this.model.portfolio;

        if (this.model.expirationDate) {
          this.model.expirationDate = this.datePipe.transform(this.model.expirationDate, 'dd/MM/yyyy') as any;
        }

        if (this.model.operationDate) {
          this.model.operationDate = new Date(this.model.operationDate);
        }

        this.model.operationValue = this.decimalPipe.transform(this.model.operationValue, '1.0-2', 'pt-BR') as any;
        this.model.futureFee = this.decimalPipe.transform(this.model.futureFee, '1.0-8', 'pt-BR') as any;

        this.isEditing = true;
        this.canValidate = true;

        this.setViewOnly();
      }, () => this.router.navigate([MenuConstants.Tickets.CurrencyTerm]));
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

    if (f.invalid) {
      return throwError('Verifique os campos destacados.');
    }

    this.canValidate = true;

    const errors = this.getErrors();

    if (f.invalid || errors.length > 0) {
      return throwError('Verifique os campos destacados.');
    }

    const data = Object.assign({}, this.model);
    data.id = null;

    data.portfolio.document = this.validator.sanitize(this.model.portfolio.document);
    data.counterpart.document = this.validator.sanitize(this.model.counterpart.document);

    data.operationValue = this.validator.getCurrency(this.model.operationValue);
    data.futureFee = this.validator.getCurrency(this.model.futureFee);
    data.expirationDate = new Date(this.model.expirationDate.toString().replace( /(\d{2})\/(\d{2})\/(\d{4})/, '$2/$1/$3'));

    this.carteiraComponent.Submit();

    const method = (editing && !duplicate)
      ? this.service.UpdateCurrencyTerm(this.model.id, data)
      : this.service.RegisterCurrencyTerm(data);

    return method
      .pipe(catchError(err => {
        return throwError('Erro ao cadastrar a boleta.');
      }))
      .pipe(map((term: CurrencyTerm) => {
        if (!editing) {
          this.model.id = term.id;
          this.model.operationDate = term.operationDate;
          this.model.statusId = term.statusId;

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
          .subscribe((term: CurrencyTerm) => {
            this.service.ForwardCurrencyTerm(term.id).subscribe(res => {
              this.model.statusDescription = res.statusDescription;

              this.setViewOnly();

              this.snackBarService.open(
                'Boleta encaminhada.', 'OK', {
                  verticalPosition: 'top',
                  horizontalPosition: 'right'
                });

              this.getTicket(term.id);
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
          this.router.navigate([MenuConstants.Todos, { boletaId: TicketType.Quota }]);
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
