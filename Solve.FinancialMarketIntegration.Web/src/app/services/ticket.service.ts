import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  TipoOperacao,
  TipoMercado,
  TipoLiquidacao,
  TipoLiquidacaoMargem,
  Moeda,
  TipoJuros
} from '../model/domain/Entity';
import { Tipo } from '../model/domain/Tipo';
import { FundQuota } from '../model/domain/FundQuota';
import { Margin } from '../model/domain/Margem';
import { environment } from '../../environments/environment';
import { Cetip } from '../model/domain/Cetip';
import { Selic } from '../model/domain/Selic';
import { VariableIncome } from '../model/domain/VariableIncome';
import { Contracted } from '../model/domain/Contracted';
import { Futures } from '../model/domain/Futures';
import { StatusDetail } from '../model/domain/Summary';
import { SwapCetip } from '../model/domain/SwapCetip';
import { CurrencyTerm } from '../model/domain/CurrencyTerm';
import { WorkflowHistory, Step } from '../model/domain/WorkflowHistory';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { UploadRv } from '../model/domain/UploadRv';

@Injectable({
  providedIn: 'root'
})
export class TicketService {
  constructor(
    private http: HttpClient
  ) { }

  ListarStatus(): Tipo<number>[] {
    return [
      Tipo.Criar(1, 'Em Processamento'),
      Tipo.Criar(2, 'Não enviada.'),
      Tipo.Criar(3, 'Cancelada'),
      Tipo.Criar(4, 'Finalizada')
    ];
  }

  /**
   *Validado conforme API.
   * @returns {Tipo<number>[]}
   * @memberof TicketService
   */
  Boletas(): Tipo<number>[] {
    return [
      Tipo.Criar(1, 'Cotas'),
      Tipo.Criar(2, 'CETIP'),
      Tipo.Criar(3, 'SELIC'),
      Tipo.Criar(4, 'Compromissada de Compra'),
      Tipo.Criar(5, 'Termo de Moeda'),
      Tipo.Criar(6, 'RV'),
      Tipo.Criar(7, 'SwapCetip'),
      Tipo.Criar(8, 'Futuros'),
      Tipo.Criar(9, 'Margem')
    ];
  }

  ListarTipoOperacoes(): Tipo<number>[] {
    return [
      Tipo.Criar(TipoOperacao.Aplicacao, 'Aplicacao'),
      Tipo.Criar(TipoOperacao.Compra, 'Compra'),
      Tipo.Criar(TipoOperacao.Deposito, 'Deposito'),
      Tipo.Criar(TipoOperacao.MoedaComprada, 'Moeda Comprada'),

      Tipo.Criar(TipoOperacao.MoedaVendida, 'Moeda Vendida'),
      Tipo.Criar(TipoOperacao.Resgate, 'Resgate'),
      Tipo.Criar(TipoOperacao.Retirada, 'Retirada'),
      Tipo.Criar(TipoOperacao.Transferencia, 'Transferencia'),
      Tipo.Criar(TipoOperacao.Venda, 'Venda')
    ];
  }

  ListarCotas(): FundQuota[] {
    // TODO: implementar
    return [];
  }

  ListarTipoOperacaoCota(): Tipo<number>[] {
    return [
      Tipo.Criar(TipoOperacao.Aplicacao, 'Aplicação'),
      Tipo.Criar(TipoOperacao.Resgate, 'Resgate'),
      Tipo.Criar(TipoOperacao.Transferencia, 'Transferência')
    ];
  }
  ListarTipoLiquidacaoCota(): Tipo<number>[] {
    return [
      Tipo.Criar(TipoLiquidacao.CETIP, 'CETIP'),
      Tipo.Criar(TipoLiquidacao.TED, 'TED')
    ];
  }

  ListarTipoOperacoesPadrao(): Tipo<number>[] {
    return [
      Tipo.Criar(TipoOperacao.Compra, 'Compra'),
      Tipo.Criar(TipoOperacao.Venda, 'Venda')
    ];
  }

  ListarTipoOperacoesMargem(): Tipo<number>[] {
    return [
      Tipo.Criar(TipoOperacao.Deposito, 'Depósito'),
      Tipo.Criar(TipoOperacao.Retirada, 'Retirada')
    ];
  }

  ListarTiposMercado(): Tipo<number>[] {
    return [
      Tipo.Criar(TipoMercado.RFTituloPrivado, 'RF - Título Privado'),
      Tipo.Criar(TipoMercado.RFTituloPublico, 'RF - Título Público'),
      Tipo.Criar(TipoMercado.RendaVariavel, 'Renda Variável'),
      Tipo.Criar(TipoMercado.Dinheiro, 'Dinheiro')
    ];
  }

  ListarTiposLiquidacaoCompromissada(): Tipo<number>[] {
    return [
      Tipo.Criar(TipoLiquidacao.Compra, 'Compra'),
      Tipo.Criar(TipoLiquidacao.Venda, 'Venda')
    ];
  }

  ListarTiposLiquidacaoMargem(): Tipo<number>[] {
    return [
      Tipo.Criar(TipoLiquidacaoMargem.SELIC, 'SELIC'),
      Tipo.Criar(TipoLiquidacaoMargem.B3, 'B3'),
      Tipo.Criar(TipoLiquidacaoMargem.Dinheiro, 'Dinheiro')
    ];
  }

  ListarBasesIndexador(): Tipo<number>[] {
    return [
      Tipo.Criar(1, '252 dias'),
      Tipo.Criar(2, '360 dias')
    ];
  }

  ListarTipoJuros(): Tipo<number>[] {
    return [
      Tipo.Criar(TipoJuros.Exponencial, 'Exponencial'),
      Tipo.Criar(TipoJuros.Linear, 'Linear')
    ];
  }

  Summary(
    startDate: Date = null,
    endDate: Date = null,
  ): Observable<StatusDetail[]> {
    const params: any = {};

    if (startDate) {
      params.startDate = startDate.toJSON();
    }

    if (endDate) {
      params.endDate = endDate.toJSON();
    }

    return this.http.get<StatusDetail[]>(
      `${environment.apiUrls.tickets}/ticket/summary`, { params }
    );
  }

  ApiVersion(): Observable<string> {
    return this.http.get<string>(
      `${environment.apiUrls.tickets}/ticket/version`
    );
  }

  //#region :: Quotas ::

  RegisterFundQuotaTicket(ticket: FundQuota) {
    return this.http.post(`${environment.apiUrls.tickets}/quota/`, ticket);
  }

  UpdateFundQuotaTicket(id: number, ticket: FundQuota) {
    return this.http.put(`${environment.apiUrls.tickets}/quota/${id}`, ticket);
  }

  GetFundQuotaTicket(id: number): Observable<FundQuota> {
    return this.http.get<FundQuota>(
      `${environment.apiUrls.tickets}/quota/${id}`
    );
  }

  ListFundQuotaTicket(
    startDate: Date = null,
    endDate: Date = null,
    statusId: number = null,
    portfolioCode: string = null,
    portfolioName: string = null,
    portfolioDocument: string = null
  ): Observable<Array<FundQuota>> {
    const params: any = {};

    if (startDate) {
      params.startDate = startDate.toJSON();
    }

    if (endDate) {
      params.endDate = endDate.toJSON();
    }

    if (statusId) {
      params.statusId = statusId.toString();
    }

    if (portfolioCode) {
      params.portfolioCode = portfolioCode;
    }

    if (portfolioName) {
      params.portfolioName = portfolioName;
    }

    if (portfolioDocument) {
      params.portfolioDocument = portfolioDocument;
    }

    return this.http.get<FundQuota[]>(`${environment.apiUrls.tickets}/quota`, {
      params
    });
  }

  ForwardQuotaTicket(id: number) {
    return this.http.post<{ statusDescription: string }>(
      `${environment.apiUrls.tickets}/quota/${id}/forward`,
      {}
    );
  }

  //#endregion

  //#region :: Private Fixed Income ::

  RegisterPrivateFixedIncomeTicket(ticket: Cetip) {
    return this.http.post(`${environment.apiUrls.tickets}/private-fixed-income`, ticket);
  }

  UpdatePrivateFixedIncomeTicket(id: number, ticket: Cetip) {
    return this.http.put(`${environment.apiUrls.tickets}/private-fixed-income/${id}`, ticket);
  }

  GetPrivateFixedIncomeTicket(id: number): Observable<Cetip> {
    return this.http.get<Cetip>(`${environment.apiUrls.tickets}/private-fixed-income/${id}`);
  }

  ListPrivateFixedIncomeTicket(
    startDate: Date = null,
    endDate: Date = null,
    statusId: number = null,
    portfolioCode: string = null,
    portfolioName: string = null,
    portfolioDocument: string = null
  ): Observable<Array<Cetip>> {
    const params: any = {};

    if (startDate) {
      params.startDate = startDate.toJSON();
    }

    if (endDate) {
      params.endDate = endDate.toJSON();
    }

    if (statusId) {
      params.statusId = statusId.toString();
    }

    if (portfolioCode) {
      params.portfolioCode = portfolioCode;
    }

    if (portfolioName) {
      params.portfolioName = portfolioName;
    }

    if (portfolioDocument) {
      params.portfolioDocument = portfolioDocument;
    }

    return this.http.get<Cetip[]>(`${environment.apiUrls.tickets}/private-fixed-income`, {
      params
    });
  }

  ForwardPrivateFixedIncomeTicket(id: number) {
    return this.http.post<{ statusDescription: string }>(
      `${environment.apiUrls.tickets}/private-fixed-income/${id}/forward`,
      {}
    );
  }

  //#endregion

  //#region :: Public Fixed Income ::

  RegisterPublicFixedIncomeTicket(ticket: Selic) {
    return this.http.post(`${environment.apiUrls.tickets}/public-fixed-income`, ticket);
  }

  UpdatePublicFixedIncomeTicket(id: number, ticket: Selic) {
    return this.http.put(`${environment.apiUrls.tickets}/public-fixed-income/${id}`, ticket);
  }

  GetPublicFixedIncomeTicket(id: number): Observable<Selic> {
    return this.http.get<Selic>(`${environment.apiUrls.tickets}/public-fixed-income/${id}`);
  }

  ListPublicFixedIncomeTicket(
    startDate: Date = null,
    endDate: Date = null,
    statusId: number = null,
    portfolioCode: string = null,
    portfolioName: string = null,
    portfolioDocument: string = null
  ): Observable<Array<Selic>> {
    const params: any = {};

    if (startDate) {
      params.startDate = startDate.toJSON();
    }

    if (endDate) {
      params.endDate = endDate.toJSON();
    }

    if (statusId) {
      params.statusId = statusId.toString();
    }

    if (portfolioCode) {
      params.portfolioCode = portfolioCode;
    }

    if (portfolioName) {
      params.portfolioName = portfolioName;
    }

    if (portfolioDocument) {
      params.portfolioDocument = portfolioDocument;
    }

    return this.http.get<Selic[]>(`${environment.apiUrls.tickets}/public-fixed-income`, {
      params
    });
  }

  ForwardPublicFixedIncomeTicket(id: number) {
    return this.http.post<{ statusDescription: string }>(
      `${environment.apiUrls.tickets}/public-fixed-income/${id}/forward`,
      {}
    );
  }

  //#endregion

  //#region :: Contracted aka Committed ::

  RegisterContractedTicket(ticket: Contracted) {
    return this.http.post(`${environment.apiUrls.tickets}/contracted`, ticket);
  }

  UpdateContractedTicket(id: number, ticket: Contracted) {
    return this.http.put(`${environment.apiUrls.tickets}/contracted/${id}`, ticket);
  }

  GetContractedTicket(id: number): Observable<Contracted> {
    return this.http.get<Contracted>(`${environment.apiUrls.tickets}/contracted/${id}`);
  }

  ListContractedTicket(
    startDate: Date = null,
    endDate: Date = null,
    statusId: number = null,
    portfolioCode: string = null,
    portfolioName: string = null,
    portfolioDocument: string = null
  ):Observable<Array<Selic>> {
    const params: any = {};

    if (startDate) {
      params.startDate = startDate.toJSON();
    }

    if (endDate) {
      params.endDate = endDate.toJSON();
    }

    if (statusId) {
      params.statusId = statusId.toString();
    }

    if (portfolioCode) {
      params.portfolioCode = portfolioCode;
    }

    if (portfolioName) {
      params.portfolioName = portfolioName;
    }

    if (portfolioDocument) {
      params.portfolioDocument = portfolioDocument;
    }

    return this.http.get<Selic[]>(`${environment.apiUrls.tickets}/contracted`, {
      params
    });
  }

  ForwardContractedTicket(id: number) {
    return this.http.post<{ statusDescription: string }>(
      `${environment.apiUrls.tickets}/contracted/${id}/forward`,
      {}
    );
  }

  //#endregion

  //#region :: Futures ::

  RegisterFuturesTicket(ticket: Futures) {
    return this.http.post(`${environment.apiUrls.tickets}/futures`, ticket);
  }

  UpdateFuturesTicket(id: number, ticket: Futures) {
    return this.http.put(`${environment.apiUrls.tickets}/futures/${id}`, ticket);
  }

  GetFuturesTicket(id: number): Observable<Futures> {
    return this.http.get<Futures>(`${environment.apiUrls.tickets}/futures/${id}`);
  }

  ListFuturesTicket(
    startDate: Date = null,
    endDate: Date = null,
    statusId: number = null,
    portfolioCode: string = null,
    portfolioName: string = null,
    portfolioDocument: string = null
  ): Observable<Array<Futures>> {
    const params: any = {};

    if (startDate) {
      params.startDate = startDate.toJSON();
    }

    if (endDate) {
      params.endDate = endDate.toJSON();
    }

    if (statusId) {
      params.statusId = statusId.toString();
    }

    if (portfolioCode) {
      params.portfolioCode = portfolioCode;
    }

    if (portfolioName) {
      params.portfolioName = portfolioName;
    }

    if (portfolioDocument) {
      params.portfolioDocument = portfolioDocument;
    }

    return this.http.get<Futures[]>(`${environment.apiUrls.tickets}/futures`, {
      params
    });
  }

  ForwardFuturesTicket(id: number) {
    return this.http.post<{ statusDescription: string }>(
      `${environment.apiUrls.tickets}/futures/${id}/forward`,
      {}
    );
  }

  //#endregion

  //#region :: Swap CETIP ::

  RegisterSwapCetipTicket(ticket: SwapCetip) {
    return this.http.post(`${environment.apiUrls.tickets}/swap-cetip/`, ticket);
  }

  UpdateSwapCetipTicket(id: number, ticket: SwapCetip) {
    return this.http.put(`${environment.apiUrls.tickets}/swap-cetip/${id}`, ticket);
  }

  GetSwapCetipTicket(id: number): Observable<SwapCetip> {
    return this.http.get<SwapCetip>(`${environment.apiUrls.tickets}/swap-cetip/${id}`);
  }

  ListSwapCetipTicket(
    startDate: Date = null,
    endDate: Date = null,
    statusId: number = null,
    portfolioCode: string = null,
    portfolioName: string = null,
    portfolioDocument: string = null
  ): Observable<Array<SwapCetip>> {
    const params: any = {};

    if (startDate) {
      params.startDate = startDate.toJSON();
    }

    if (endDate) {
      params.endDate = endDate.toJSON();
    }

    if (statusId) {
      params.statusId = statusId.toString();
    }

    if (portfolioCode) {
      params.portfolioCode = portfolioCode;
    }

    if (portfolioName) {
      params.portfolioName = portfolioName;
    }

    if (portfolioDocument) {
      params.portfolioDocument = portfolioDocument;
    }

    return this.http.get<SwapCetip[]>(`${environment.apiUrls.tickets}/swap-cetip`, {
      params
    });
  }

  ForwardSwapCetipTicket(id: number) {
    return this.http.post<{ statusDescription: string }>(
      `${environment.apiUrls.tickets}/swap-cetip/${id}/forward`,
      {}
    );
  }

  //#endregion

  //#region :: Margin ::

  RegisterMarginTicket(ticket: Margin) {
    return this.http.post(`${environment.apiUrls.tickets}/margin/`, ticket);
  }

  UpdateMarginTicket(id: number, ticket: Margin) {
    return this.http.put(`${environment.apiUrls.tickets}/margin/${id}`, ticket);
  }

  GetMarginTicket(id: number): Observable<Margin> {
    return this.http.get<Margin>(`${environment.apiUrls.tickets}/margin/${id}`);
  }

  ListMarginTicket(
    startDate: Date = null,
    endDate: Date = null,
    statusId: number = null,
    portfolioCode: string = null,
    portfolioName: string = null,
    portfolioDocument: string = null
  ): Observable<Array<Margin>> {
    const params: any = {};

    if (startDate) {
      params.startDate = startDate.toJSON();
    }

    if (endDate) {
      params.endDate = endDate.toJSON();
    }

    if (statusId) {
      params.statusId = statusId.toString();
    }

    if (portfolioCode) {
      params.portfolioCode = portfolioCode;
    }

    if (portfolioName) {
      params.portfolioName = portfolioName;
    }

    if (portfolioDocument) {
      params.portfolioDocument = portfolioDocument;
    }

    return this.http.get<Margin[]>(`${environment.apiUrls.tickets}/margin`, {
      params
    });
  }

  ForwardMarginTicket(id: number) {
    return this.http.post<{ statusDescription: string }>(
      `${environment.apiUrls.tickets}/margin/${id}/forward`,
      {}
    );
  }

  //#endregion

  //#region :: RV ::

  // RegisterVariableIncomeTicket(ticket: RendaVariavel) {
  //   return this.http.put(`${environment.apiUrls.tickets}/rv`, ticket);
  // }

  // UpdateVariableIncomeTicket(id: number, ticket: RendaVariavel) {
  //   return this.http.put(`${environment.apiUrls.tickets}/rv/${id}`, ticket);
  // }

  GetVariableIncomeTicket(id: number): Observable<VariableIncome> {
    return this.http.get<VariableIncome>(`${environment.apiUrls.tickets}/VariableIncome/${id}`);
  }

  ListVariableIncome(
    startDate: Date = null,
    endDate: Date = null,
    statusId: number = null,
    portfolioCode: string = null,
    portfolioName: string = null,
    portfolioDocument: string = null
  ): Observable<Array<VariableIncome>> {
    const params: any = {};

    if (startDate) {
      params.startDate = startDate.toJSON();
    }

    if (endDate) {
      params.endDate = endDate.toJSON();
    }

    if (statusId) {
      params.statusId = statusId.toString();
    }

    if (portfolioCode) {
      params.portfolioCode = portfolioCode;
    }

    if (portfolioName) {
      params.portfolioName = portfolioName;
    }

    if (portfolioDocument) {
      params.portfolioDocument = portfolioDocument;
    }

    return this.http.get<VariableIncome[]>(`${environment.apiUrls.tickets}/VariableIncome`, {
      params
    });
  }

  ExportTickets (

    startDate: Date = null,
    endDate: Date = null,
    statusId: number = null,
    portfolioCode: string = null,
    portfolioName: string = null,
    portfolioDocument: string = null
  ): Observable<Blob> {
    const params: any = {};

    if (startDate) {
      params.startDate = startDate.toJSON();
    }

    if (endDate) {
      params.endDate = endDate.toJSON();
    }

    if (statusId) {
      params.statusId = statusId.toString();
    }

    if (portfolioCode) {
      params.portfolioCode = portfolioCode;
    }

    if (portfolioName) {
      params.portfolioName = portfolioName;
    }

    if (portfolioDocument) {
      params.portfolioDocument = portfolioDocument;
    }

    return this.http.get(`${environment.apiUrls.tickets}/ticket/export`, {
      params: params,
      responseType: 'blob'
    });
  }

  // Upload
  UploadVariableIncomeTicketsFile(ticketsFile: UploadRv) {
    return this.http.post(`${environment.apiUrls.files}/FileUpload/variable-income`, ticketsFile);
  }
  GetUploadAccountManagersList() {
    return this.http.get(`${environment.apiUrls.files}/FileUpload/account-managers`);
  }

  //#endregion

  //#region :: Term ::

  RegisterCurrencyTerm(ticket: CurrencyTerm) {
    return this.http.post(`${environment.apiUrls.tickets}/currency-term/`, ticket);
  }

  UpdateCurrencyTerm(id: number, ticket: CurrencyTerm) {
    return this.http.put(`${environment.apiUrls.tickets}/currency-term/${id}`, ticket);
  }

  GetCurrencyTerm(id: number): Observable<CurrencyTerm> {
    return this.http.get<CurrencyTerm>(`${environment.apiUrls.tickets}/currency-term/${id}`);
  }

  ListCurrencyTerm(
    startDate: Date = null,
    endDate: Date = null,
    statusId: number = null,
    portfolioCode: string = null,
    portfolioName: string = null,
    portfolioDocument: string = null
  ): Observable<Array<CurrencyTerm>> {
    const params: any = {};

    if (startDate) {
      params.startDate = startDate.toJSON();
    }

    if (endDate) {
      params.endDate = endDate.toJSON();
    }

    if (statusId) {
      params.statusId = statusId.toString();
    }

    if (portfolioCode) {
      params.portfolioCode = portfolioCode;
    }

    if (portfolioName) {
      params.portfolioName = portfolioName;
    }

    if (portfolioDocument) {
      params.portfolioDocument = portfolioDocument;
    }

    return this.http.get<CurrencyTerm[]>(`${environment.apiUrls.tickets}/currency-term`, {
      params
    });
  }

  ForwardCurrencyTerm(id: number) {
    return this.http.post<{ statusDescription: string }>(
      `${environment.apiUrls.tickets}/currency-term/${id}/forward`,
      {}
    );
  }

  Approve(ticketId: number, comments: string) {
    return this.http.post(`${environment.apiUrls.tickets}/ticket/${ticketId}/approve`, { comments });
  }

  Reject(ticketId: number, comments: string) {
    return this.http.post(`${environment.apiUrls.tickets}/ticket/${ticketId}/reject`, { 'comments': comments });
  }

  //#endregion

  /**
   * @description Get Workflow History
   * @param ticketId
   * @returns array of Step History
   */
  GetHistory(ticketId: number): Observable<Array<Step>> {
    const endpointUrl = `${environment.apiUrls.tickets}/ticket/${ticketId}/history`;
    return this.http.get<WorkflowHistory>(endpointUrl)
                    .pipe(
                      map(r => r.steps),
                    );
  }

  /**
   * @description Get Workflow Status
   */
  GetWorflowStatus(): Observable<any> {
    const endpointUrl = `${environment.apiUrls.workflow}/workflow/status`;
    return this.http.get<any>(endpointUrl);
  }
}

export interface ITicketService<T> {
  RegisterTicket(ticket: T): void;
  UpdateTicket(id: number, ticket: T): void;
  GetTicket(id: number): T;
  ListTicket(): T[];
  ForwardTicket(id: number): string;
}
