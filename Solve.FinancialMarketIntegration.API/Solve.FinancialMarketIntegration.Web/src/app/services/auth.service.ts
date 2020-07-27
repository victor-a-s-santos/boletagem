import { Injectable } from '@angular/core';

import { Observable, of, concat, empty } from 'rxjs';
import { flatMap, map, toArray } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { AuthToken } from '../components/auth/auth.token';
import { environment } from '../../environments/environment';
import { AuthRole, Roles } from '../components/auth/auth.role';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  authKey = '.authToken';
  authToken: AuthToken = null;

  constructor(private http: HttpClient) {

  }

  // store the URL so we can redirect after logging in
  redirectUrl: string;

  login(username: string, password: string): Observable<boolean> {
    return concat(
      this.http.post<AuthToken>(environment.apiUrls.security + '/auth/token', { username, password }).pipe(flatMap((authTokenResut) => {
        // armazena o resultado da autenticação para através do token obter as roles do usuário
        this.authToken = authTokenResut;
        return of(authTokenResut);
      })),
      this.http.get<number[]>(environment.apiUrls.security + '/auth/user/roles')).pipe(toArray(),
        map(([, authRolesResult]) => {
          this.authToken.roles = authRolesResult as number[];

          if (this.authToken && this.authToken.roles) {
            localStorage.setItem(this.authKey, JSON.stringify(this.authToken));
            return true;
          }

          return false;
        })
      );
  }

  resetPassword(username: string, oldPassword: string, newPassword: string): Observable<boolean> {
    return concat(
      this.http.post<AuthToken>(environment.apiUrls.security + '/auth/reset-password', { username, oldPassword, newPassword })
        .pipe(flatMap((authTokenResut) => {
          // armazena o resultado da autenticação para através do token obter as roles do usuário
          this.authToken = authTokenResut;
          return of(authTokenResut);
        })),
      this.http.get<number[]>(environment.apiUrls.security + '/auth/user/roles')
    ).pipe(toArray(),
        map(([, authRolesResult]) => {
          this.authToken.roles = authRolesResult as number[];

          if (this.authToken && this.authToken.roles) {
            localStorage.setItem(this.authKey, JSON.stringify(this.authToken));
            return true;
          }

          return false;
        })
      );
  }

  logout(): void {
    localStorage.removeItem(this.authKey);
  }

  getToken(): string {
    const auth = this.getAuth();
    if (auth) {
      return auth.token;
    }
    return '';
  }

  isLoggedIn(): boolean {
    return this.getAuth() !== null;
  }

  hasRole(role: Roles) {
    const auth = this.getAuth();
    if (!auth) {
      return false;
    }

    return auth.roles.some(r => r === role);
  }

  hasRoles(roles: Roles[]) {
    const auth = this.getAuth();
    if (!auth) {
      return false;
    }

    const result = auth.roles.some(r => roles.some(rr => rr === r));

    return result;
  }

  getAuth(): AuthToken {
    if (!this.authToken) {
      const auth = localStorage.getItem(this.authKey);
      if (auth) {
        this.authToken = JSON.parse(auth);
      }
    }

    if (this.authToken && this.tokenIsValid()) {
      return this.authToken;
    }

    this.logout();
    return null;
  }

  private tokenIsValid(): boolean {
    const tokenExpirationDate = new Date(this.authToken.tokenExpiration);
    return tokenExpirationDate > new Date();
  }
}
