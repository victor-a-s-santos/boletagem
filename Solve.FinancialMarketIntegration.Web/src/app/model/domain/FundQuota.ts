import { Boleta, TipoLiquidacao, TipoOperacao, TipoFundo } from './Entity';

export class FundQuota extends Boleta {
  // Dados Carteira

  portfolioDocument: string = null;
  portfolioAccount: string = null;
  portfolioName: string = null;
  portfolioCode: string = null;
  portfolioCetipAccount: string = null;

  // Dados da Operação

  operationTypeId: TipoOperacao = null;
  fullRedeem: boolean = null;
  settlementTypeId: TipoLiquidacao = null;
  isSecondaryMarket = false;
  isIssueUnitPrice = false;
  operationValue: number = null;
  amount: number = null;
  quotaValue: number = null;
  hasSameOwnership: boolean = null;
  isCetipVoice: boolean = null;

  // - Dados do Fundo

  fundName: string = null;
  fundDocument: string = null;
  isFIDC = false;
  fundClassSeries: string = null;
  isNewFund = false;
  issuerName: string = null;
  fundType: TipoFundo = null;
  mnemonicCode: string = null;

  fundBank: string = null;
  fundAccount: string = null;
  fundBranch: string = null;

  // - Dados da Contraparte

  counterpartName: string = null;
  counterpartDocument: string = null;
  counterpartCetipAccount: string = null;
  command = '0';

  annotations: string = null;

  constructor() {
    super();
  }
}
