import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { User } from '../model/domain/User';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(
    private http: HttpClient
  ) { }

  RegisterUser(user: User) {
    return this.http.post(`${environment.apiUrls.users}/register/`, user);
  }

  UpdateUser(id: number, user: User) {
    return this.http.put(`${environment.apiUrls.users}/update/${id}`, user);
  }

  GetUser(id: number) {
    return this.http.get<User>(`${environment.apiUrls.users}/${id}`);
  }

  GetUsers(accountManagerId: number = null, isExternalUser: boolean = null) {
    let url = `${environment.apiUrls.users}/account-managers`;

    if (accountManagerId) {
      url += `/${accountManagerId}`;
    }

    const params: any = {};
    if (isExternalUser != null) {
      params.isExternalUser = isExternalUser;
    }

    return this.http.get<User[]>(url, {
      params
    });
  }

  ExportUsers(accountManagerId: number = null, isExternalUser: boolean = null): Observable<Blob> {
    let url = `${environment.apiUrls.users}/export`;

    if (accountManagerId) {
      url += `/${accountManagerId}`;
    }

    const params: any = {};
    if (isExternalUser != null) {
      params.isExternalUser = isExternalUser;
    }

    return this.http.get(url, {
      params: params,
      responseType: 'blob'
    });
  }
}
