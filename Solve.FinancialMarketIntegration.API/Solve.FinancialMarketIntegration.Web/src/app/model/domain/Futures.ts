import { TipoOperacao, Boleta } from './Entity';

export class Futures extends Boleta {
  constructor() {
    super();
  }

  // Dados da operação
  id: number = null;
  operationDate: Date = null;
  operationTypeId: TipoOperacao = null;
  amount: number = null;
  tradingDate: Date = null;
  percentageDiscount: number = null;
  unitPrice: number = null;
  paperCode: string = null;
  paperSerie: string = null;
  annotations: string = null;

  // Dados da corretora
  broker: string = null;
  brokerCode: string = null;
  brokerAccount: string = null;
  brokerDocument: string = null;

  // Dados Carteira/Fundo
  portfolioName = '';
  portfolioCode = '';
  portfolioDocument = '';
  portfolioAccount = '';
  portfolioB3Account = '';
}
