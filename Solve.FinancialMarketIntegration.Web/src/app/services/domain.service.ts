import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';

export interface TicketType {
  id: number;
  name: string;
}

export interface OperationType {
  id: number;
  name: string;
}

export interface SettlementType {
  id: number;
  name: string;
}

export interface MarketType {
  id: number;
  name: string;
}
@Injectable({
  providedIn: 'root'
})
export class DomainService {
  constructor(
    private http: HttpClient,
  ) {}

  public ListTicketTypes(): Observable<TicketType[]> {
    return this.http.get<TicketType[]>(`${environment.apiUrls.tickets}/ticket/types`);
  }

  public ListOperationTypes(): Observable<OperationType[]> {
    return this.http.get<OperationType[]>(`${environment.apiUrls.tickets}/ticket/operation-types`);
  }

  public ListSettlementTypes(): Observable<SettlementType[]> {
    return this.http.get<SettlementType[]>(`${environment.apiUrls.tickets}/ticket/settlement-types`);
  }

  public ListMarketTypes(): Observable<MarketType[]> {
    return this.http.get<MarketType[]>(`${environment.apiUrls.tickets}/ticket/market-types`);
  }
}
