import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from '../../services/user.service';
import { User } from '../../model/domain/User';
import { SecurityService } from '../../services/security.service';
import { AccountManager } from '../../model/domain/AccountManager';
import { AuthService } from '../../services/auth.service';
import { Roles } from '../auth/auth.role';
import { DownloadService } from '../../services/download.service';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class UsuariosComponent implements OnInit {

  displayedColumns = ['actions', 'name', 'email', 'userName', 'active'];
  model: User[] = null;
  accountManagers: AccountManager[] = null;
  selectedAccManagerId: number = null;
  isExternalUser = true;

  constructor(
    private service: UserService,
    private router: Router,
    private securityService: SecurityService,
    private authService: AuthService,
    private downloadService: DownloadService
  ) { }

  ngOnInit() {
    if (this.canCreateMaster()) {
      this.securityService.GetAccountManagers().subscribe(a => {
        this.accountManagers = a;
      });

      this.getUsers(this.selectedAccManagerId);
    } else if (this.canCreateSubordinate()) {
      this.securityService.GetAccountManager(this.authService.authToken.username).subscribe(g => {
        if (g != null) {
          this.selectedAccManagerId = g.id;

          this.getUsers(this.selectedAccManagerId);
        }
      });
    }
  }

  getUsers(accountManagerId: number = null) {
    if (!this.isExternalUser) {
      this.selectedAccManagerId = null;
    }

    this.service.GetUsers(accountManagerId, this.isExternalUser).subscribe(q => {
      this.model = q;
    });
  }

  openUser(id: number = null) {
    if (id === null) {
      this.router.navigate(['/cadastro-usuario']);
    } else {
      this.router.navigate(['/cadastro-usuario', id]);
    }
  }

  canCreateMaster() {
    const roles = [
      Roles.CreateMasterUser
    ];
    return this.authService.hasRoles(roles);
  }

  canCreateSubordinate() {
    const roles = [
      Roles.CreateSubordinateUser
    ];
    return this.authService.hasRoles(roles);
  }

  exportUsers() {
    const method = this.service.ExportUsers(
      this.selectedAccManagerId,
      this.isExternalUser
    );

    return method
      .pipe(catchError(err => {
        return throwError('Erro ao exportar.');
      }))
      .subscribe((data: Blob) => {
        this.downloadService.downloadFile(data);
      });
  }
}
