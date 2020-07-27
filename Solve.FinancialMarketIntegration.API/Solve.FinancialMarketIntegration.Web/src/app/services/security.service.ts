import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Group } from '../model/domain/Group';
import { AccountManager } from '../model/domain/AccountManager';

@Injectable({
  providedIn: 'root'
})
export class SecurityService {
  constructor(
    private http: HttpClient
  ) { }

  RegisterGroup(group: Group) {
    return this.http.post(`${environment.apiUrls.security}/groups/register/`, group);
  }

  UpdateGroup(id: number, group: Group) {
    return this.http.put(`${environment.apiUrls.security}/groups/update/${id}`, group);
  }

  GetGroup(id: number) {
    return this.http.get<Group>(`${environment.apiUrls.security}/groups/${id}`);
  }

  GetGroups() {
    return this.http.get<Group[]>(`${environment.apiUrls.security}/groups`);
  }

  GetMasterGroupsByUsername(username: string) {
    return this.http.get<Group[]>(`${environment.apiUrls.security}/groups/from-manager/${username}`);
  }

  GetRoles() {
    return this.http.get<Group[]>(`${environment.apiUrls.security}/roles`);
  }

  GetAccountManagers() {
    return this.http.get<AccountManager[]>(`${environment.apiUrls.security}/account-managers`);
  }

  GetAccountManager(username: string) {
    return this.http.get<AccountManager>(`${environment.apiUrls.security}/user-account-manager/${username}`);
  }
}
