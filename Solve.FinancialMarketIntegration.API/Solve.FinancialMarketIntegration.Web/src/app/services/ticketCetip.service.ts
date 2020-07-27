import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Cetip } from '../model/domain/Cetip';
import { Observable } from 'rxjs';
import { RepositoryService } from './seedwork/IRepositoryService';

@Injectable({
  providedIn: 'root'
})
export class TicketCetipService extends RepositoryService<Cetip> {

  constructor(private http: HttpClient) {
    super();
  }
  public getUrl(p: string = ''): string {
    return `${environment.apiUrls.tickets}/cetip/` + p;
  }
  public GetHttp() {
    return this.http;
  }
}
