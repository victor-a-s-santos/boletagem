import { TipoFundo, TipoOperacao, Entity, Portfolio, Boleta, Counterpart } from './Entity';

export class Cetip extends Boleta {

  constructor() {
    super();

  }

  // Dados do Ativo
  operationTypeId: TipoOperacao = null;
  assetType = null;
  assetCode = null;
  expirationDate = null;
  amount: number = null;
  unitPrice = null;
  issueFee = null;

  operationValue = null;

  issueDate: Date = null;
  acquisitionDate: Date = null;

  isSecondaryMarket: boolean = null;
  annotations = null;
  objectAction: string = null;

  // Dados Carteira/Fundo
  portfolioName = '';
  portfolioDocument = '';
  portfolioAccount = '';
  portfolioCetipAccount = '';

  // Dados da Contraparte
  counterpartName = '';
  counterpartDocument = '';
  counterpartCetipAccount = '';
  command = '';
}
