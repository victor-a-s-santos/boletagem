import { TipoOperacao, Boleta } from './Entity';

export class Contracted extends Boleta {
  constructor() {
    super();
  }

  // Dados da operação
  id: number = null;
  operationDate: Date = null;
  operationTypeId: TipoOperacao = null;
  amount: number = null;
  unitPriceOutward: number = null;
  unitPriceReturn: number = null;
  returnDate: Date = null;
  valueOutward: number = null;
  valueReturn: number = null;
  operationValue: number = null;
  command: string = null;
  annotations: string = null;

  // Dados do título
  security = '';
  securityId = '';
  expirationDate: Date = null;
  issueTax: number = null;

  // Dados da Contraparte
  counterpartName = '';
  counterpartDocument = '';
  counterpartSelicAccount = '';

  // Dados Carteira/Fundo
  portfolioName = '';
  portfolioCode = '';
  portfolioDocument = '';
  portfolioAccount = '';
  portfolioSelicAccount = '';
}
