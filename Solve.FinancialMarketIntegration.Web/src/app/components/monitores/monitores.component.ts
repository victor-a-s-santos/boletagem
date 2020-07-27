import { Component, OnInit, ViewChild } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { Location } from '@angular/common';
import { NgForm } from '@angular/forms';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import {
  ActivatedRoute,
  Router,
  ActivatedRouteSnapshot
} from '@angular/router';
import { throwError, of, Observable, Subscription } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { DateTime } from 'luxon';

import { DomainService, TicketType } from '../../services/domain.service';
import { TicketService } from '../../services/ticket.service';
import { DialogService } from '../../services/dialog.service';
import { SnackbarService } from '../../services/snackbar.service';
import { TicketStatus, TicketType as TicketTypeEnum, Boleta } from '../../model/domain/Entity';
import { FundQuota } from '../../model/domain/FundQuota';
import { Selic } from '../../model/domain/Selic';
import { Contracted } from '../../model/domain/Contracted';
import { Tipo } from '../../model/domain/Tipo';
import { Cetip } from '../../model/domain/Cetip';
import { Futures } from '../../model/domain/Futures';
import { SwapCetip } from '../../model/domain/SwapCetip';
import { Margin } from '../../model/domain/Margem';
import { ValidatorService } from '../../services/validator.service';
import { CurrencyTerm } from '../../model/domain/CurrencyTerm';
import { MenuConstants } from '../../infrastructure/MenuConstants';
import { AuthService } from '../../services/auth.service';
import { Roles } from '../auth/auth.role';
import { VariableIncome } from '../../model/domain/VariableIncome';
import { DownloadService } from '../../services/download.service';


@Component({
  selector: 'app-monitores',
  templateUrl: './monitores.component.html',
  styleUrls: ['./monitores.component.scss']
})
export class MonitoresComponent implements OnInit {
  constructor(
    private ticketService: TicketService,
    private domainService: DomainService,
    private authService: AuthService,
    private dialogService: DialogService,
    private snackBarService: SnackbarService,
    private route: ActivatedRoute,
    private router: Router,
    private location: Location,
    public validator: ValidatorService,
    private downloadService: DownloadService
  ) {}

  @ViewChild('thisForm') public frm: NgForm;

  ActiveTicketId = 0;
  Timestamp = DateTime.local();
  TicketStatus = TicketStatus;
  TicketTypeEnum = TicketTypeEnum;
  MenuConstants = MenuConstants;

  // Filtros
  Hoje: Date = new Date();
  PeriodoDe: Date = new Date(this.Hoje.getFullYear(), this.Hoje.getMonth(), this.Hoje.getDate());
  PeriodoAte: Date = new Date(this.Hoje.getFullYear(), this.Hoje.getMonth(), this.Hoje.getDate());
  CarteiraFundo = '';
  Nome = '';
  CNPJ = '';

  Quotas: FundQuota[] = [];
  PrivateFixedIncome: Cetip[] = [];
  PublicFixedIncome: Selic[] = [];
  Contracted: Contracted[] = [];
  Futures: Futures[] = [];
  SwapCetip: SwapCetip[] = [];
  Margin: Margin[] = [];
  CurrencyTerm: CurrencyTerm[] = [];
  VariableIncome: VariableIncome[] = [];

  SelectionQuotas = new SelectionModel<FundQuota>(true, []);
  SelectionPrivateFixedIncome = new SelectionModel<Cetip>(true, []);
  SelectionPublicFixedIncome = new SelectionModel<Selic>(true, []);
  SelectionContracted = new SelectionModel<Contracted>(true, []);
  SelectionFutures = new SelectionModel<Futures>(true, []);
  SelectionSwapCetip = new SelectionModel<SwapCetip>(true, []);
  SelectionMargin = new SelectionModel<Margin>(true, []);
  SelectionCurrencyTerm = new SelectionModel<CurrencyTerm>(true, []);
  SelectionVariableIncome = new SelectionModel<VariableIncome>(true, []);

  ListaStatus: Tipo<any>[];
  TicketTypes: TicketType[];
  WorkflowStatus: Tipo<any>[];

  WorkflowStatusDescription: { [x: number]: string } = { };
  OperationTypesDescription: { [x: number]: string } = { };
  SettlemenTypesDescription: { [x: number]: string } = { };
  MarketTypesDescription: { [x: number]: string } = { };

  WorkflowStatusFilter: number;
  TicketTypeFilter: number;
  IsTicketTypeFilter: boolean;

  ColumnsQuota: string[] = [
    'actions',
    'id',
    'statusId',
    'operationDate',
    'portfolioName',
    'portfolioDocument',
    // specifics
    'cetipAccount',
    'operationTypeId',
    'settlementTypeId',
    'operationValue',
    'fundName',
    'counterpartName',
    'isCetipVoice',
    'mnemonicCode',
    'counterpartCetipAccount',
    'amount',
    'quotaValue',
    'fundType',
    'portfolioAccount',
    'fundBank',
    'fundBranch',
    'fundAccount',
    'isNewFund',
    'elapsedTime'
  ];

  ColumnsPrivateFixedIncome: string[] = [
    'actions',
    'id',
    'statusId',
    'operationDate',
    'portfolioName',
    'portfolioDocument',
    // specifics
    'portfolioCetipAccount',
    'operationTypeId',
    'operationValue',
    'unitPrice',
    'amount',
    'assetType',
    'assetCode',
    'expirationDate',
    'counterpartName',
    'counterpartCetipAccount',
    'portfolioAccount',
    'elapsedTime'
  ];

  ColumnsPublicFixedIncome: string[] = [
    'actions',
    'id',
    'statusId',
    'operationDate',
    'portfolioName',
    'portfolioDocument',
    // specifics
    'portfolioSelicAccount',
    'operationTypeId',
    'settlementTypeId',
    'operationValue',
    'unitPrice',
    'amount',
    'security',
    'securityId',
    'expirationDate',
    'counterpartName',
    'counterpartSelicAccount',
    'settlementDate',
    'portfolioAccount',
    'command',
    'elapsedTime'
  ];

  ColumnsContracted: string[] = [
    'actions',
    'id',
    'statusId',
    'operationDate',
    'portfolioName',
    'portfolioDocument',
    // specifics
    'portfolioSelicAccount',
    'returnDate',
    'counterpartName',
    'valueOutward',
    'valueReturn',
    'issueTax',
    'security',
    'securityId',
    'expirationDate',
    'portfolioAccount',
    'command',
    'elapsedTime'
  ];

  ColumnsFutures: string[] = [
    'actions',
    'id',
    'statusId',
    'operationDate',
    'portfolioName',
    'portfolioDocument',
    // specifics
    'portfolioB3Account',
    'tradingDate',
    'operationTypeId',
    'broker',
    'brokerCode',
    'paperCode',
    'amount',
    'portfolioAccount',
    'elapsedTime'
  ];

  ColumnsSwapCetip: string[] = [
    'actions',
    'id',
    'statusId',
    'operationDate',
    'portfolioName',
    'portfolioDocument',
    // specifics
    'portfolioCetipAccount',
    'operationValue',
    'counterpartName',
    'counterpartCetipAccount',
    'portfolioAccount',
    'elapsedTime'
  ];

  ColumnsMargin: string[] = [
    'actions',
    'id',
    'statusId',
    'operationDate',
    'portfolioName',
    'portfolioDocument',
    // specifics
    'portfolioClearingAccount',
    'operationTypeId',
    'marketTypeId',
    'operationValue',
    'amount',
    'unitPrice',
    'securityName',
    'securityCode',
    'dueDate',
    'counterpartName',
    'counterpartClearingAccount',
    'command',
    'portfolioAccount',
    'elapsedTime'
  ];

  ColumnsCurrencyTerm: string[] = [
    'actions',
    'id',
    'statusId',
    'operationDate',
    'portfolioName',
    'portfolioDocument',
    'portfolioCetipAccount',
    'operationTypeId',
    'operationValue',
    'counterpartName',
    'counterpartCetipAccount',
    'portfolioAccount',
    'elapsedTime'
  ];

  ColumnsVariableIncome: string[] = [
    'actions',
    'id',
    'statusId',
    'operationDate',
    'stockExchangeDate',
    'b3Account',
    'brokerCode',
    'buyTotal',
    'sellTotal',
    'total'
  ];

  TicketRoleMap = {
    [TicketTypeEnum.Quota]: Roles.ViewMonitorFundQuota,
    [TicketTypeEnum.CETIP]: Roles.ViewMonitorPrivateFixedIncome,
    [TicketTypeEnum.SELIC]: Roles.ViewMonitorPublicFixedIncome,
    [TicketTypeEnum.Contracted]: Roles.ViewMonitorContracted,
    [TicketTypeEnum.Futures]: Roles.ViewMonitorFutures,
    [TicketTypeEnum.SwapCetip]: Roles.ViewMonitorSwapCetip,
    [TicketTypeEnum.Margin]: Roles.ViewMonitorMargin,
    [TicketTypeEnum.CurrencyTerm]: Roles.ViewMonitorCurrencyTerm,
    [TicketTypeEnum.VariableIncome]: Roles.ViewMonitorVariableIncome
  };

  TicketRoleCreateMap = {
    [TicketTypeEnum.Quota]: Roles.CreateTicketFundQuota,
    [TicketTypeEnum.CETIP]: Roles.CreateTicketPrivateFixedIncome,
    [TicketTypeEnum.SELIC]: Roles.CreateTicketPublicFixedIncome,
    [TicketTypeEnum.Contracted]: Roles.CreateTicketContracted,
    [TicketTypeEnum.Futures]: Roles.CreateTicketFutures,
    [TicketTypeEnum.SwapCetip]: Roles.CreateTicketSwapCetip,
    [TicketTypeEnum.Margin]: Roles.CreateTicketMargin,
    [TicketTypeEnum.CurrencyTerm]: Roles.CreateTicketCurrencyTerm,
    [TicketTypeEnum.VariableIncome]: Roles.CreateTicketVariableIncome
  };

  ngOnInit() {
    const boletaId = this.route.snapshot.paramMap.get('boletaId');

    if (boletaId) {
      this.ActiveTicketId = +boletaId;
      this.TicketTypeFilter = this.ActiveTicketId;
    }

    if (!this.PeriodoAte) {
      this.PeriodoAte = DateTime.local().toJSDate();
    }

    if (!this.PeriodoDe) {
      this.PeriodoDe = DateTime.local().toJSDate();
    }

    this.populate();

    this.domainService.ListTicketTypes().subscribe(types => {
      this.TicketTypes = types;
    });
    this.ticketService.GetWorflowStatus().subscribe(wfStatus => {
      this.WorkflowStatusDescription = wfStatus;
      this.WorkflowStatusDescription[-1] = 'Em Elaboração';
      this.WorkflowStatus = Object.keys(wfStatus).map(k => ({
        id: k,
        name: wfStatus[k]
      })) as any;
    });

    this.domainService.ListOperationTypes().subscribe(types => {
      const descriptions = {};
      types.forEach(t => {
        descriptions[t.id] = t.name;
      });
      this.OperationTypesDescription = descriptions;
    });

    this.domainService.ListSettlementTypes().subscribe(types => {
      const descriptions = {};
      types.forEach(t => {
        descriptions[t.id] = t.name;
      });
      this.SettlemenTypesDescription = descriptions;
    });

    this.domainService.ListMarketTypes().subscribe(types => {
      const descriptions = {};
      types.forEach(t => {
        descriptions[t.id] = t.name;
      });
      this.MarketTypesDescription = descriptions;
    });
  }

  populate() {
    this.load(this.TicketTypeFilter);
  }

  load(ticketType: TicketTypeEnum = null, ignoreViewChanges: boolean = false) {
    const params: any[] = [
      this.PeriodoDe,
      this.PeriodoAte,
      this.WorkflowStatusFilter,
      this.CarteiraFundo,
      this.Nome,
      this.validator.sanitize(this.CNPJ || ''),
    ];

    if (this.periodoError()) {
      return ;
    }

    const methods: { [x: number]: () => Subscription } = {
      [TicketTypeEnum.Quota]: () => this.mapTicketData(
          this.ticketService.ListFundQuotaTicket(...params)
        ).subscribe(s => this.Quotas = s),
      [TicketTypeEnum.CETIP]: () => this.mapTicketData(
          this.ticketService.ListPrivateFixedIncomeTicket(...params)
        ).subscribe(s => this.PrivateFixedIncome = s),
      [TicketTypeEnum.SELIC]: () => this.mapTicketData(
          this.ticketService.ListPublicFixedIncomeTicket(...params)
        ).subscribe(s => this.PublicFixedIncome = s),
      [TicketTypeEnum.Contracted]: () => this.mapTicketData(
          this.ticketService.ListContractedTicket(...params)
        ).subscribe(s => this.Contracted = s),
      [TicketTypeEnum.Futures]: () => this.mapTicketData(
          this.ticketService.ListFuturesTicket(...params)
        ).subscribe(s => this.Futures = s),
      [TicketTypeEnum.SwapCetip]: () => this.mapTicketData(
          this.ticketService.ListSwapCetipTicket(...params)
        ).subscribe(s => this.SwapCetip = s),
      [TicketTypeEnum.Margin]: () => this.mapTicketData(
          this.ticketService.ListMarginTicket(...params)
        ).subscribe(s => this.Margin = s),
      [TicketTypeEnum.CurrencyTerm]: () => this.mapTicketData(
          this.ticketService.ListCurrencyTerm(...params)
        ).subscribe(s => this.CurrencyTerm = s),
        [TicketTypeEnum.VariableIncome]: () => this.mapTicketData(
          this.ticketService.ListVariableIncome(...params)
        ).subscribe(s => this.VariableIncome = s)
    };

    if (ticketType) {
      methods[ticketType]();

      if (!ignoreViewChanges) {
        this.ActiveTicketId = ticketType;
        this.IsTicketTypeFilter = true;
      }

      return;
    }

    Object.keys(methods).forEach(k => {
      if (this.authService.hasRole(this.TicketRoleMap[k])) {
        methods[k]();
      }
    });

    this.IsTicketTypeFilter = false;
  }

  mapTicketData(source: Observable<any>) {
    return source
      .pipe(catchError(err => {
        if (err.status === 404) {
          return of([]);
        }
        return throwError(err);
      }))
      .pipe(map((s: any[]) => {
        s.forEach(t => {
          if (t.workflowStartDate) {
            const time = DateTime.fromISO(t.workflowStartDate);
            t.elapsedTime = this.Timestamp.diff(time, ['hours', 'minutes']);
          } else {
            t.elapsedTime = null;
          }

          if (t.portfolioDocument != null) {
            t.portfolioDocument = t.portfolioDocument.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/g, '\$1.\$2.\$3\/\$4\-\$5');
          }

          if (t.counterpartDocument != null) {
            t.counterpartDocument = t.counterpartDocument.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/g, '\$1.\$2.\$3\/\$4\-\$5');
          }

          if (t.portfolio != null && t.portfolio.document != null) {
            t.portfolio.document = t.portfolio.document.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/g, '\$1.\$2.\$3\/\$4\-\$5');
          }

          if (t.counterpart != null && t.counterpart.document != null) {
            t.counterpart.document = t.counterpart.document.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/g, '\$1.\$2.\$3\/\$4\-\$5');
          }
        });
        return s;
      }));
  }

  filter() {
    this.populate();
  }

  clear() {
    this.frm.reset();
    this.populate();
  }

  export() {
    const method = this.ticketService.ExportTickets(
      this.PeriodoDe,
      this.PeriodoAte,
      this.WorkflowStatusFilter,
      this.CarteiraFundo,
      this.Nome,
      this.CNPJ
    );

    return method
      .pipe(catchError(err => {
        return throwError('Erro ao exportar.');
      }))
      .subscribe((data: Blob) => {
        this.downloadService.downloadFile(data);
      });
  }

  periodoError() {
    if ((this.PeriodoAte == null) || (this.PeriodoDe == null)) {
      return true;
    }
    const dateDiff = (this.PeriodoAte.getTime() - this.PeriodoDe.getTime()) / 86400000;
    return (dateDiff > 30) || (dateDiff < 0);
  }

  isAllSelected(ticketType: TicketTypeEnum) {
    const tickets: { [x: number]: () => any } = {
      [TicketTypeEnum.Quota]: () => this.SelectionQuotas.selected.length === this.Quotas.length,
      [TicketTypeEnum.CETIP]: () => this.SelectionPrivateFixedIncome.selected.length === this.PrivateFixedIncome.length,
      [TicketTypeEnum.SELIC]: () => this.SelectionPublicFixedIncome.selected.length === this.PublicFixedIncome.length,
      [TicketTypeEnum.Contracted]: () => this.SelectionContracted.selected.length === this.Contracted.length,
      [TicketTypeEnum.Futures]: () => this.SelectionFutures.selected.length === this.Futures.length,
      [TicketTypeEnum.SwapCetip]: () => this.SelectionSwapCetip.selected.length === this.SwapCetip.length,
      [TicketTypeEnum.Margin]: () => this.SelectionMargin.selected.length === this.Margin.length,
      [TicketTypeEnum.CurrencyTerm]: () => this.SelectionCurrencyTerm.selected.length === this.CurrencyTerm.length,
      [TicketTypeEnum.VariableIncome]: () => this.SelectionVariableIncome.selected.length === this.VariableIncome.length
    };

    return tickets[ticketType]();
  }

  masterToggle(ticketType: TicketTypeEnum) {
    const tickets: { [x: number]: () => any } = {
      [TicketTypeEnum.Quota]: () => [this.SelectionQuotas, this.Quotas],
      [TicketTypeEnum.CETIP]: () => [this.SelectionPrivateFixedIncome, this.PrivateFixedIncome],
      [TicketTypeEnum.SELIC]: () => [this.SelectionPublicFixedIncome, this.PublicFixedIncome],
      [TicketTypeEnum.Contracted]: () => [this.SelectionContracted, this.Contracted],
      [TicketTypeEnum.Futures]: () => [this.SelectionFutures, this.Futures],
      [TicketTypeEnum.SwapCetip]: () => [this.SelectionSwapCetip, this.SwapCetip],
      [TicketTypeEnum.Margin]: () => [this.SelectionMargin, this.Margin],
      [TicketTypeEnum.CurrencyTerm]: () => [this.SelectionCurrencyTerm, this.CurrencyTerm],
      [TicketTypeEnum.VariableIncome]: () => [this.SelectionVariableIncome, this.VariableIncome]
    };

    const data = tickets[ticketType]();

    this.isAllSelected(ticketType) ?
      data[0].clear() :
      data[1].forEach(row => {
        if (this.canForward(ticketType, row)) {
          data[0].select(row);
        }
      });
  }

  masterClear(ticketType: TicketTypeEnum) {
    const tickets: { [x: number]: () => any } = {
      [TicketTypeEnum.Quota]: () => this.SelectionQuotas,
      [TicketTypeEnum.CETIP]: () => this.SelectionPrivateFixedIncome,
      [TicketTypeEnum.SELIC]: () => this.SelectionPublicFixedIncome,
      [TicketTypeEnum.Contracted]: () => this.SelectionContracted,
      [TicketTypeEnum.Futures]: () => this.SelectionFutures,
      [TicketTypeEnum.SwapCetip]: () => this.SelectionSwapCetip,
      [TicketTypeEnum.Margin]: () => this.SelectionMargin,
      [TicketTypeEnum.CurrencyTerm]: () => this.SelectionCurrencyTerm,
      [TicketTypeEnum.VariableIncome]: () => this.SelectionVariableIncome,
    };

    const data = tickets[ticketType]();

    (data as SelectionModel<any>).clear();
  }

  hasForwardRole(ticketType: TicketTypeEnum) {
    return this.authService.hasRole(this.TicketRoleCreateMap[ticketType]);
  }

  hasApproveRole(ticketType: TicketTypeEnum) {
    const tickets: { [x: number]: () => any } = {
      [TicketTypeEnum.Quota]: () => [this.SelectionQuotas, this.Quotas],
      [TicketTypeEnum.CETIP]: () => [this.SelectionPrivateFixedIncome, this.PrivateFixedIncome],
      [TicketTypeEnum.SELIC]: () => [this.SelectionPublicFixedIncome, this.PublicFixedIncome],
      [TicketTypeEnum.Contracted]: () => [this.SelectionContracted, this.Contracted],
      [TicketTypeEnum.Futures]: () => [this.SelectionFutures, this.Futures],
      [TicketTypeEnum.SwapCetip]: () => [this.SelectionSwapCetip, this.SwapCetip],
      [TicketTypeEnum.Margin]: () => [this.SelectionMargin, this.Margin],
      [TicketTypeEnum.CurrencyTerm]: () => [this.SelectionCurrencyTerm, this.CurrencyTerm],
      [TicketTypeEnum.VariableIncome]: () => [this.SelectionVariableIncome, this.VariableIncome]
    };

    const data = tickets[ticketType]();

    for (const row of data[1]) {
      if (this.canApprove(row)) {
        return true;
      }
    }

    return false;
  }

  canForward(ticketType: TicketTypeEnum, row: Boleta) {
    return (!row.statusId && !row.statusDescription)
      && this.authService.hasRole(this.TicketRoleCreateMap[ticketType]);
  }

  canApprove(row: Boleta) {
    return row.statusId && row.statusRequiredRoles && this.authService.hasRoles(row.statusRequiredRoles);
  }

  canCheck(ticketType: TicketTypeEnum, row: Boleta) {
    return this.canForward(ticketType, row) || this.canApprove(row);
  }

  forwardMany(ticketType: TicketTypeEnum) {
    const methods: { [x: number]: () => any } = {
      [TicketTypeEnum.Quota]: () => [this.SelectionQuotas, (id) => this.ticketService.ForwardQuotaTicket(id)],
      [TicketTypeEnum.CETIP]: () => [this.SelectionPrivateFixedIncome, (id) => this.ticketService.ForwardPrivateFixedIncomeTicket(id)],
      [TicketTypeEnum.SELIC]: () => [this.SelectionPublicFixedIncome, (id) => this.ticketService.ForwardPublicFixedIncomeTicket(id)],
      [TicketTypeEnum.Contracted]: () => [this.SelectionContracted, (id) => this.ticketService.ForwardContractedTicket(id)],
      [TicketTypeEnum.Futures]: () => [this.SelectionFutures, (id) => this.ticketService.ForwardFuturesTicket(id)],
      [TicketTypeEnum.SwapCetip]: () => [this.SelectionSwapCetip, (id) => this.ticketService.ForwardSwapCetipTicket(id)],
      [TicketTypeEnum.Margin]: () => [this.SelectionMargin, (id) => this.ticketService.ForwardMarginTicket(id)],
      [TicketTypeEnum.CurrencyTerm]: () => [this.SelectionCurrencyTerm, (id) => this.ticketService.ForwardCurrencyTerm(id)],
      [TicketTypeEnum.VariableIncome]: () => [this.SelectionVariableIncome, (id) => this.ticketService.ForwardCurrencyTerm(id)]
    };

    const data = methods[ticketType]();

    if (data[0].selected.length === 0 ) {
      return;
    }

    this.dialogService.confirm('Encaminhar boletas', `Deseja encaminhar ${data[0].selected.length} boletas?`)
      .subscribe(result => {
        if (result) {
          const promises: Promise<any>[] = [];
          (data[0].selected as Boleta[]).forEach(b => {
            promises.push((data[1](b.id) as Observable<any>).toPromise());
          });
          Promise.all(promises).then(() => this.load(ticketType, true));
          this.masterClear(ticketType);
        }
      });


  }

  approveMany(ticketType: TicketTypeEnum) {
    const methods: { [x: number]: () => any } = {
      [TicketTypeEnum.Quota]: () => [this.SelectionQuotas, (id) => this.ticketService.Approve(id, '')],
      [TicketTypeEnum.CETIP]: () => [this.SelectionPrivateFixedIncome, (id) => this.ticketService.Approve(id, '')],
      [TicketTypeEnum.SELIC]: () => [this.SelectionPublicFixedIncome, (id) => this.ticketService.Approve(id, '')],
      [TicketTypeEnum.Contracted]: () => [this.SelectionContracted, (id) => this.ticketService.Approve(id, '')],
      [TicketTypeEnum.Futures]: () => [this.SelectionFutures, (id) => this.ticketService.Approve(id, '')],
      [TicketTypeEnum.SwapCetip]: () => [this.SelectionSwapCetip, (id) => this.ticketService.Approve(id, '')],
      [TicketTypeEnum.Margin]: () => [this.SelectionMargin, (id) => this.ticketService.Approve(id, '')],
      [TicketTypeEnum.CurrencyTerm]: () => [this.SelectionCurrencyTerm, (id) => this.ticketService.Approve(id, '')],
      [TicketTypeEnum.VariableIncome]: () => [this.SelectionVariableIncome, (id) => this.ticketService.Approve(id, '')]
    };

    const data = methods[ticketType]();

    if (data[0].selected.length === 0 ) {
      return;
    }

    this.dialogService.confirm('Aprovar boletas', `Deseja aprovar ${data[0].selected.length} boletas?`)
      .subscribe(result => {
        if (result) {
          const promises: Promise<any>[] = [];
          (data[0].selected as Boleta[]).forEach(b => {
            promises.push((data[1](b.id) as Observable<any>).toPromise());
          });
          Promise.all(promises).then(() => this.load(ticketType, true));
          this.masterClear(ticketType);
        }
      });
  }

  setActiveTicket(id: number) {
    const url = this.router.createUrlTree([{ boletaId: id }], { relativeTo: this.route });
    this.location.go(url.toString());
  }

  openTicket(ticket: string, id: number) {
    this.router.navigate([ticket, id]);
  }

  canDisplay(ticketType: TicketTypeEnum) {
    return this.authService.hasRole(this.TicketRoleMap[ticketType])
      && (!this.IsTicketTypeFilter || (this.IsTicketTypeFilter && this.ActiveTicketId === ticketType));
  }
}
