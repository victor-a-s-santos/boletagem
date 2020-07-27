import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { IDataResult } from '../../model/OperationResult';

export interface IRepositoryService<T> {
  Get(id: number): Observable<T>;
  List(): Observable<T[]>;
}

export abstract class RepositoryService<T> implements IRepositoryService<T> {
  constructor() {
  }

  Get(id: number): Observable<T> {
    return this.GetHttp().get<T>(this.getUrl(id.toString()));
  }

  List(): Observable<T[]> {
    return this.GetHttp().get<T[]>(this.getUrl());
  }

  Save(e: T): Observable<IDataResult<T>> {
    return this.GetHttp().post<IDataResult<T>>(this.getUrl(), e);
  }

  abstract GetHttp(): HttpClient;
  abstract getUrl(p?: string): string;
}
