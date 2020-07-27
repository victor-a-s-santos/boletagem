import { AuthRole } from './auth.role';
import { ReturnStatement } from '@angular/compiler';

export class AuthToken {
  name: string;
  username: string;
  token: string;
  tokenExpiration: Date;
  roles: number[];
}
