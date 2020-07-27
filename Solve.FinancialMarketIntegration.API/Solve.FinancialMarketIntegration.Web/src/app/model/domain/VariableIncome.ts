import { Acao, TipoOperacao, Boleta } from './Entity';

export class VariableIncome extends Boleta {
  constructor() {
    super();
  }

  id: number = null;
  operationDate: Date = null;
  stockExchangeDate: Date = null;
  b3Account: string = null;
  brokerCode: string = null;
  lineItems: VariableIncomeLineItem[] = null;
  buyTotal: number = null;
  sellTotal: number = null;
}

export class VariableIncomeLineItem {
  marketType: string = null;
  buySell: string = null;
  tradingCode: string = null;
  companyName: string = null;
  specification: string = null;
  amount: number = null;
  price: number = null;
}
