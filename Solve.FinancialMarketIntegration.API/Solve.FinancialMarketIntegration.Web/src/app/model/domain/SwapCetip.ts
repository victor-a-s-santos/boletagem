import { TipoOperacao, Boleta } from "./Entity";

export class SwapCetip extends Boleta {
  constructor() {
    super();
  }

  // Dados da operação
  id: number = null;
  operationDate: Date = null;
  operationValue: number = null;
  expirationDate: Date = null;
  command: string = null;
  activeIndex: string = null;
  activeIndexPercent: number = null;
  activeIndexTax: number = null;
  activeIndexBase: number = null;
  activeInterestType: number = null;
  passiveIndex: string = null;
  passiveIndexPercent: number = null;
  passiveIndexTax: number = null;
  passiveIndexBase: number = null;
  passiveInterestType: number = null;
  annotations:  string = null;

  // Dados da Contraparte
  counterpartName = '';
  counterpartDocument = '';
  counterpartCetipAccount = '';

  // Dados Carteira/Fundo
  portfolioName = '';
  portfolioCode = '';
  portfolioDocument = '';
  portfolioAccount = '';
  portfolioCetipAccount = '';
}
