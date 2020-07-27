import {
  Entity,
  ContraParte,
  TipoLiquidacaoFinanceira,
  Moeda,
  LocalCustodia,
  Praca,
  Clearing,
  Portfolio,
  Counterpart,
  Boleta
} from './Entity';
import { _countGroupLabelsBeforeOption } from '@angular/material';
import { getDOM } from '@angular/platform-browser/src/dom/dom_adapter';

export enum ModalidadeNDF {
  NDF = 1,
  AVista = 2,
  Forward = 3
}

export enum TipoLiquidacaoNDF {
  MoedaComprada = 1,
  MoedaVendida = 2
}

export class CurrencyTerm extends Boleta {

  constructor() {
    super();
  }

  id: number = null;
  portfolio: Portfolio = new Portfolio();

  operationDate: Date = new Date();
  operationValue: number = null;
  operationTypeId: number = null;
  statusId: number = null;

  expirationDate: Date = null;

  cetipSettlement = false;
  contractNumber: string = null;
  futureFee: number = null;
  quoteExpirationTypeId: number = null;
  currencyId: number = null;
  crossRate: boolean = null;

  counterpart: Counterpart = new Counterpart();
  annotations: string = null;
  workflowId: number = null;

  static Create(): CurrencyTerm {
    const e = new CurrencyTerm();
    e.operationDate = new Date();
    e.counterpart = new Counterpart();
    e.portfolio = new Portfolio();
    return e;
  }


  public static Mock(): CurrencyTerm {
    const m = CurrencyTerm.Create();

    m.id = 0;
    m.currencyId = 7;
    m.operationTypeId = 8;
    m.operationValue = 123456789012.34;

    m.quoteExpirationTypeId = 2;
    m.portfolio = Portfolio.Mock();

    m.cetipSettlement = true;
    m.crossRate = false;
    m.counterpart.document = '60701190000104';
    m.counterpart.name = 'nome da contraparte';
    m.counterpart.clearingAccount = '12345678';

    m.expirationDate = new Date(2019, 12);
    m.contractNumber = '987654321012345';
    m.futureFee = 1234.5678901234;

    m.annotations = '0123456789 0123456789 0123456789 0123456789 0123456789 ' +
      '0123456789 0123456789 0123456789 0123456789 0123456789 123456789';


    return m;
  }
}



