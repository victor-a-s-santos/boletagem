import { Injectable } from '@angular/core';
import { Tipo } from '../model/domain/Tipo';
import { Currency, TipoOperacao } from '../model/domain/Entity';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { CurrencyTerm } from '../model/domain/CurrencyTerm';
import { IDataResult } from '../model/OperationResult';
import { RepositoryService, IRepositoryService } from './seedwork/IRepositoryService';

@Injectable({
  providedIn: 'root'
})
export class TermoMoedaService extends RepositoryService<CurrencyTerm> {

  constructor(private http: HttpClient) {
    super();
  }
  public getUrl(p: string = ''): string {
    return `${environment.apiUrls.tickets}/currency-term/` + p;
  }
  public GetHttp() {
    return this.http;
  }

  ListarTipoOperacaoTermoMoeda(): Tipo<number>[] {
    return [
      Tipo.Criar(TipoOperacao.MoedaComprada, 'Moeda Comprada'),
      Tipo.Criar(TipoOperacao.MoedaVendida, 'Moeda Vendida')
    ];
  }

  ListarMoedas(): Tipo<number>[] {
    return [
      Tipo.Criar(Currency.USD, 'Dólar'),
      Tipo.Criar(Currency.Euro, 'Euro'),
      Tipo.Criar(Currency.Yene, 'Yene')
    ];
  }

  public ListaCotacaoVencimento(): Tipo<number>[] {
    return [
      Tipo.Criar(1, 'P-TAX - D1'),
    ];
  }
}
