import { TipoOperacao, TipoLiquidacao, Boleta } from './Entity';

export class Selic extends Boleta {
  constructor() {
    super();
  }

  // Dados da operação
  id: number = null;
  operationTypeId: TipoOperacao = null;
  amount: number = null;
  unitPrice: number = null;
  settlementDate: Date = null;
  acquisitionDate: Date = null;
  operationValue: number = null;
  operationDate: Date = null;
  settlementTypeId: TipoLiquidacao = null;

  // Dados Carteira/Fundo
  portfolioName = '';
  portfolioCode = '';
  portfolioDocument = '';
  portfolioAccount = '';
  portfolioSelicAccount = '';

  // Dados do título
  security = '';
  securityId = '';
  expirationDate: Date = null;
  issueTax: number = null;
  issueDate: Date = null;

  // Dados da Contraparte
  counterpartName = '';
  counterpartDocument = '';
  counterpartSelicAccount = '';
  command = '';

  annotations = '';

  DataLiquidacaoObrigatoria(): boolean {
    return this.operationTypeId === TipoOperacao.Venda && this.settlementTypeId === TipoLiquidacao.Term;
  }
}
