import { Group } from '../../model/domain/Group';

export class User {
  id = 0;
  name: string = null;
  email: string = null;
  userDocument: string = null;
  userName: string = null;
  active = true;
  accountManagerId: number = null;
  isMaster: boolean = null;
  isExternalUser = true;
  lockoutEndDateUtc: Date = null;

  groups: Group[] = null;
}
