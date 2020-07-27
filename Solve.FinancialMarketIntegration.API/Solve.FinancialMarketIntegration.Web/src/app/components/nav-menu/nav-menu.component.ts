import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Roles } from '../auth/auth.role';
import { AuthToken } from '../auth/auth.token';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  public roles = Roles;
  public auth: AuthToken = null;
  constructor(
    private authService: AuthService,
    private router: Router,
  ) {
    this.auth = authService.getAuth();
  }

  canShow(roles: Roles[]) {
    return this.authService.hasRoles(roles);
  }

  signout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  changePassword() {
    this.authService.logout();
    this.router.navigate(['/password-reset', this.auth.username]);
  }

  canCreateAny() {
    const roles = [
      Roles.CreateTicketFundQuota,
      Roles.CreateTicketPrivateFixedIncome,
      Roles.CreateTicketPublicFixedIncome,
      Roles.CreateTicketFutures,
      Roles.CreateTicketMargin,
      Roles.CreateTicketContracted,
      Roles.CreateTicketCurrencyTerm,
      Roles.CreateTicketVariableIncome,
      Roles.CreateTicketSwapCetip
    ];

    return this.authService.hasRoles(roles);
  }

  canUploadAny() {
    const roles = [
      Roles.CreateTicketVariableIncome
    ];

    return this.authService.hasRoles(roles);
  }
}
