import { Boleta, ContraParte, Titulo, TipoOperacao, TipoLiquidacao, TipoMercado } from './Entity';

export class Margin extends Boleta {
  // Dados Carteira

  portfolioDocument: string = null;
  portfolioAccount: string = null;
  portfolioName: string = null;
  portfolioClearingAccount: string = null;
  portfolioCode: string = null;

  marketTypeId: TipoMercado = null;
  operationTypeId: TipoOperacao = null;
  amount: number = null;
  unitPrice: number = null;
  operationValue: number = null;
  securityType: string = null;
  securityName: string = null;
  securityCode: string = null;
  dueDate: Date = null;
  purchaseDate: Date = null;
  command = '0';
  quotation: number = null;
  asset: string = null;

  // Contraparte
  counterpartName: string = null;
  counterpartDocument: string = null;
  counterpartClearingAccount: string = null;
  counterpartBrokerAccount: string = null;

  // Observacoes
  annotations: string = null;

}
