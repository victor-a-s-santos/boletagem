import { notEqual } from 'assert';

export class Entity { }

export abstract class Boleta extends Entity {
  id = 0;
  operationDate?: Date = null;

  operationTypeId = 0;
  statusId?: number = null;
  statusDescription?: string = null;
  workflowStartDate?: string = null;
  statusRequiredRoles: [];


  // TODO: padronizar boletas
  operationType: TipoOperacao = null;
}

export class Ticket {
  OperationDate: Date = null;
  StatusId?: number = null;
  Portfolio: Portfolio = null;

  constructor() {
  }
}

export class Portfolio {
  name: string = null;
  document: string = null;
  account: string = null;
  clearingAccount = '';
  code = '';
  constructor() { }

  static Mock(): Portfolio {
    const m = new Portfolio();
    m.account = '111111';
    m.account = '123456';
    m.code = '123456';
    m.document = '60701190000104';
    m.name = 'Guilherme Stenio';
    return m;
  }
}

export class Counterpart {
  constructor() { }
  name: string = null;
  document: string = null;
  clearingAccount: string = null; // Conta Clearing
  command: string = null;


  static Mock(): Counterpart {
    const m = new Counterpart();
    m.document = '60701190000104';
    m.name = 'nome da contraparte';
    m.clearingAccount = '12345678';
    m.command = '12345';
    return m;
  }
}

export enum TicketType {
  Quota = 1,
  CETIP = 2,
  SELIC = 3,
  Contracted = 4,
  Futures = 5,
  SwapCetip = 6,
  Margin = 7,
  CurrencyTerm = 8,
  QuotaTED = 9,
  QuotaCETIPVoice = 10,
  VariableIncome = 11
}

export enum TicketStatus {
  Draft = 0,
  PendingAdmApproval = 1,
  CancelledByAdm = 2,
  AwaitingSettlement = 3,
  InActiveSettlement = 4,
  SettlementProcessed = 5,
  SettlementCancelled = 6,
}


/**
 * Mapeado conforme API.
 * @export
 * @enum {number}
 */
export enum TipoOperacao {
  Compra = 1,
  Venda = 2,
  Aplicacao = 3,
  Resgate = 4,
  Deposito = 5,
  Retirada = 6,
  MoedaComprada = 7,
  MoedaVendida = 8,
  Transferencia = 9
}

export enum TipoLiquidacaoFinanceira {
  CETIP = 1,
  CC = 2,
  DOC = 3,
  Interno = 4,
  Outros = 99
}

export enum TipoLiquidacao {
  CETIP = 1,
  TED = 2,
  Term = 3,
  InCash = 4,
  Compra,
  Venda
}

export enum TipoFundo {
  CFF = 'CFF',
  CFA = 'CFA'
}


/**
 * @deprecated Utilize "Currency"
 * @export
 * @enum {number}
 */
export enum Moeda {
  Dolar = 1,
  DolarRefBMF = 2,
  DolarMTM = 3,
  Real = 4,
  USD = 5,
  Euro = 6,
  Yene = 7
}

/**
 *Mapeado conforme a API.
 * @export
 * @enum {number}
 */
export enum Currency {
  Real = 1,
  USD = 2,
  Euro = 3,
  Yene = 4
}

export enum LocalCustodia {
  BMF = 1,
  CBLC = 2,
  CETIP = 3,
  SEL = 4,
  Outros = 99
}

export enum Praca {
  Brasil = 1,
  EUA = 2
}

export enum TipoMercado {
  RFTituloPrivado = 1,
  RFTituloPublico = 2,
  RendaVariavel = 3,
  Dinheiro = 4
}

export enum TipoLiquidacaoMargem {
  SELIC = 1,
  B3 = 2,
  Dinheiro = 3
}

export enum TipoJuros {
  Exponencial = 1,
  Linear = 2
}

export enum Clearing {
  Default = 1,
  BMF = 2,
  CBLC = 3,
  CETIP = 4,
  SELIC = 5,
  Outros = 99
}

export class Ativo {
  Ativo = '';
  Codigo = '';
  DataVencimento: Date = null;
  ValorOperacaoAtivo: number = null;
  Quantidade: number = null;
  PrecoUnitario: number = null;
  TaxaEmissao = '';
}

export class Acao {
  Ativo = '';
  CodigoAtivo = '';
  ValorContrato: number = null;
  Quantidade: number = null;
  PrecoUnitario: number = null;
  PercentualDesconto: number = null;
}

export class Titulo {
  constructor() { }

  Titulo = '';
  CodigoTitulo = '';
  DataVencimento: Date = null;
  ValorOperacao: number = null;
  Quantidade: number = null;
  PrecoUnitario: number = null;
}

export class ContraParte {
  Nome: string = null;
  Conta: string = null;
  Comando: string = null;

  CNPJ: string = null;
  ContaCETIPContraparte = '';
  CodigoMnemonico = '';
  AgenciaContraparte = '';
  ContaContraparte = '';
}
