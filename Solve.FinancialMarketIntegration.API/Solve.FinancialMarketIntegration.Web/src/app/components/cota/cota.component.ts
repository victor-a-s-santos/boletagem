import { Component, OnInit, ViewChild } from '@angular/core';
import { Location, DecimalPipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { conformToMask } from 'text-mask-core';

import { FundQuota } from '../../model/domain/FundQuota';
import { TipoOperacao, TipoLiquidacao, TipoFundo, TicketType } from '../../model/domain/Entity';
import { TicketService } from '../../services/ticket.service';
import { ValidatorService } from '../../services/validator.service';
import { DialogService } from '../../services/dialog.service';
import { AuthService } from '../../services/auth.service';
import { Tipo } from '../../model/domain/Tipo';
import { MenuConstants } from '../../infrastructure/MenuConstants';
import { Roles } from '../auth/auth.role';
import { SnackbarService } from '../../services/snackbar.service';

@Component({
  selector: 'app-cota',
  templateUrl: './cota.component.html',
  styleUrls: ['./cota.component.css']
})
export class CotaComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: TicketService,
    public validator: ValidatorService,
    private snackBarService: SnackbarService,
    private authService: AuthService,
    private dialogService: DialogService,
    private location: Location
  ) { }

  @ViewChild('thisForm') public frm: NgForm;

  TipoOperacao = TipoOperacao;
  TipoLiquidacao = TipoLiquidacao;

  ListaTipoLiquidacao: Tipo<number>[];
  ListaTipoOperacao: Tipo<number>[];

  isEditing = false;
  canValidate = false;
  model: FundQuota = null;

  numberMask: any;

  decimalPipe = new DecimalPipe('pt');

  // TODO: utilizar um validador já existente, talvez uma diretiva do próprio angular
  validators = {
    portfolioDocument: (value) => this.validator.validateDocument(value),
    operationTypeId: (value) => [TipoOperacao.Compra, TipoOperacao.Venda].includes(value),
    operationValue: (value) => this.showOperationValue() ?
      (this.coalesce(value).length > 0 && conformToMask(value, this.validator.currencyMaskDoubleDecimal, {})) :
      (this.coalesce(value).length === 0 || conformToMask(value, this.validator.currencyMaskDoubleDecimal, {})),
    amount: (value) => this.isQuotaValueRequired() ?
      ((this.coalesce(value).length > 0) && (conformToMask(value, this.validator.decimalMask, {}))) :
      (this.coalesce(value).length === 0 || conformToMask(value, this.validator.decimalMask, {})),
    quotaValue: (value) => this.isQuotaValueRequired() ?
      (this.coalesce(value).length > 0 && conformToMask(value, this.validator.currencyMask, {})) :
      (this.coalesce(value).length === 0 || conformToMask(value, this.validator.currencyMask, {})),
    settlementTypeId: (value) => [TipoLiquidacao.CETIP, TipoLiquidacao.TED].includes(value),
    fundType: (value) => [TipoFundo.CFF, TipoFundo.CFA].includes(value),
    fundDocument: (value) => this.validator.validateDocument(value),
    counterpartDocument: (value) => this.coalesce(value).length === 0
      || this.validator.validateDocument(value),
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
    this.model = new FundQuota();
    const id = idBoleta === null ? +this.route.snapshot.paramMap.get('id') : idBoleta;
    this.model.command = '0';

    if (id) {
      this.service.GetFundQuotaTicket(id).subscribe(q => {
        Object.assign(this.model, q);

        this.model.portfolioDocument = this.validator.formatDocument(this.model.portfolioDocument);
        this.model.fundDocument = this.validator.formatDocument(this.model.fundDocument);

        if (this.model.counterpartDocument) {
          this.model.counterpartDocument = this.validator.formatDocument(this.model.counterpartDocument);
        }

        this.model.operationValue = this.decimalPipe.transform(this.model.operationValue, '1.0-2', 'pt-BR') as any;

        if (this.model.amount) {
          this.model.amount = this.decimalPipe.transform(this.model.amount, '1.0-8', 'pt-BR') as any;
        }

        if (this.model.quotaValue) {
          this.model.quotaValue = this.decimalPipe.transform(this.model.quotaValue, '1.0-8', 'pt-BR') as any;
        }

        this.isEditing = true;
        this.canValidate = true;

        this.setViewOnly();
      }, () => this.router.navigate([MenuConstants.Tickets.Quota]));
    } else {
      if (!this.model.statusId) {
        this.model.operationDate = new Date();
      }
    }

    // Implementar domínio
    // this.ListaTipoOperacao = this.service.ListarTipoOperacaoCota();
    // this.ListaTipoLiquidacao = this.service.ListarTipoLiquidacaoCota();
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

    if (!this.showOperationValue()) {
      data.operationValue = null;
    }

    if (data.settlementTypeId === TipoLiquidacao.CETIP) {
      data.isCetipVoice = data.isCetipVoice || false;
      data.hasSameOwnership = false;

      data.fundBank = null;
      data.fundBranch = null;
      data.fundAccount = null;

      data.counterpartDocument = this.validator.sanitize(data.counterpartDocument);
    }

    if (this.isCETIPVoiceFixed()) {
      data.isCetipVoice = true;
    }

    if (data.settlementTypeId === TipoLiquidacao.TED) {
      data.hasSameOwnership = data.hasSameOwnership || false;
      data.isCetipVoice = false;

      if (!this.hasCounterpart()) {
        data.counterpartName = null;
        data.counterpartDocument = null;
      } else {
        data.counterpartDocument = this.validator.sanitize(data.counterpartDocument);
      }
      data.counterpartCetipAccount = null;
      data.command = '0';
      data.portfolioCetipAccount = null;
    }

    if (!data.isFIDC) {
      data.fundClassSeries = null;
    }

    if (!data.isNewFund) {
      data.issuerName = null;
    }

    data.operationDate = null;
    data.statusId = null;
    data.id = null;

    data.amount = this.validator.getCurrency(data.amount);
    data.quotaValue = this.validator.getCurrency(data.quotaValue);
    data.operationValue = this.validator.getCurrency(data.operationValue);
    data.portfolioDocument = this.validator.sanitize(data.portfolioDocument);
    data.fundDocument = this.validator.sanitize(data.fundDocument);

    const method = (editing && !duplicate)
      ? this.service.UpdateFundQuotaTicket(this.model.id, data)
      : this.service.RegisterFundQuotaTicket(data);

    return method
      .pipe(catchError(err => {
        return throwError('Erro ao cadastrar a boleta.');
      }))
      .pipe(map((fund: FundQuota) => {
        if (!editing) {
          this.model.id = fund.id;
          this.model.operationDate = fund.operationDate;
          this.model.statusId = fund.statusId;

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
          .subscribe((fund: FundQuota) => {
            this.service.ForwardQuotaTicket(fund.id).subscribe(res => {
              this.model.statusDescription = res.statusDescription;

              Object.values(this.frm.controls).forEach(c => {
                c.disable();
              });

              this.snackBarService.open(
                'Boleta encaminhada.', 'OK', {
                  verticalPosition: 'top',
                  horizontalPosition: 'right'
                });

              this.getTicket(fund.id);
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
          duration: 60000,
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

  isRedemption() {
    return this.model.operationTypeId === TipoOperacao.Venda;
  }

  isCETIP() {
    return this.model.settlementTypeId === TipoLiquidacao.CETIP;
  }

  hasCounterpart() {
    return this.isCETIP() || (this.isTED() && !this.model.hasSameOwnership);
  }

  isCETIPVoiceFixed() {
    return this.model.isSecondaryMarket && this.isCETIP() && (this.model.fundType === 'CFF');
  }

  isTED() {
    return this.model.settlementTypeId === TipoLiquidacao.TED;
  }

  showOperationValue() {
    return (this.model.isSecondaryMarket && this.model.isIssueUnitPrice) || (!this.model.isSecondaryMarket && !this.model.isIssueUnitPrice);
  }

  isQuotaValueRequired() {
    return (this.model.isSecondaryMarket && !this.model.isIssueUnitPrice) || (!this.model.isSecondaryMarket && this.model.isIssueUnitPrice);
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
