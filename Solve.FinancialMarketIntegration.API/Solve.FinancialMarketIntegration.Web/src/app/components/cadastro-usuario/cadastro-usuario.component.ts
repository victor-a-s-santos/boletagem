import { Location, DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { SnackbarService } from '../../services/snackbar.service';
import { UserService } from '../../services/user.service';
import { ValidatorService } from '../../services/validator.service';
import { User } from '../../model/domain/User';
import { Group } from '../../model/domain/Group';
import { AccountManager } from '../../model/domain/AccountManager';
import { SecurityService } from '../../services/security.service';
import { AuthService } from '../../services/auth.service';
import { AuthToken } from '../auth/auth.token';
import { Roles } from '../auth/auth.role';

@Component({
  selector: 'app-cadastro-usuario',
  templateUrl: './cadastro-usuario.component.html',
  styleUrls: ['./cadastro-usuario.component.css'],
  encapsulation: ViewEncapsulation.None
})

export class CadastroUsuarioComponent implements OnInit {

  @ViewChild('thisForm') public frm: NgForm;

  public model: User = null;
  canValidate = false;
  isEditing = false;
  groups: Group[] = null;
  accountManagers: AccountManager[] = null;
  public auth: AuthToken = null;
  tipoUsuario: string = null;
  gestora: AccountManager = null;
  nomeGestora: string = null;

  constructor(
    private service: UserService,
    public validator: ValidatorService,
    private snackBarService: SnackbarService,
    private router: Router,
    private route: ActivatedRoute,
    private location: Location,
    private securityService: SecurityService,
    private authService: AuthService,
    private datePipe: DatePipe
  ) {
    this.auth = authService.getAuth();
  }

  validators = {
    userDocument: (value) => this.validator.validatePhysicalDocument(value)
  };

  ngOnInit() {
    if (this.canCreateMaster()) {
      this.securityService.GetGroups().subscribe(g => {
        this.groups = g;
      });
    } else {
      this.securityService.GetMasterGroupsByUsername(this.authService.authToken.username).subscribe(g => {
        this.groups = g;
      });
    }

    this.securityService.GetAccountManagers().subscribe(a => {
      this.accountManagers = a;
    });

    this.getUser();

    this.updateTitle();

    this.securityService.GetAccountManager(this.authService.authToken.username).subscribe(g => {
      if (g != null) {
        this.gestora = g;
        this.nomeGestora = g.document + ' - ' + g.name;
      }
    });
  }

  getUser() {
    this.model = new User();

    const id = +this.route.snapshot.paramMap.get('id');
    this.model.id = id;

    if (id) {
      this.service.GetUser(id).subscribe(q => {
        Object.assign(this.model, q);

        if (this.model.userDocument) {
          this.model.userDocument = this.validator.formatDocument(this.model.userDocument);
        }

        if (this.model.lockoutEndDateUtc) {
          this.model.lockoutEndDateUtc = this.datePipe.transform(this.model.lockoutEndDateUtc, 'dd/MM/yyyy') as any;
        }

        this.isEditing = true;
        this.canValidate = true;
      }, () => this.router.navigate(['/cadastro-usuario']));
    }
  }

  submit() {
    const editing = this.isEditing;

    this.register()
    .pipe(map((user: User) => {
      if (!editing) {
        this.model.id = user.id;

        this.isEditing = true;
        const url = this.router.createUrlTree([this.model.id], { relativeTo: this.route });
        this.location.go(url.toString());
      }

      return this.model;
    }))
    .subscribe(r => {
      const msg = editing
          ? 'Usuário alterado.'
          : 'Usuário cadastrado.';

      this.snackBarService.open(
        msg, 'OK', {
          verticalPosition: 'top',
          horizontalPosition: 'right'
        });
    }, (result) => {
      let err = result;
      if (result.status === 409) {
        err = 'Esse login já existe em nossa base!';
      } else if (result.message) {
        err = result.message;
      }

      this.snackBarService.open(
        err, 'OK', {
          verticalPosition: 'top',
          horizontalPosition: 'right'
        });
    });
  }

  register() {
    const editing = this.isEditing;

    Object.values(this.frm.controls).forEach(c => {
      c.markAsTouched();
      c.updateValueAndValidity();
    });

    this.canValidate = true;

    const errors = this.getErrors();

    if (this.frm.invalid || errors.length > 0) {
      return throwError('Verifique os campos destacados.');
    }

    const data = Object.assign({}, this.model);

    data.userDocument = this.validator.sanitize(data.userDocument);
    data.id = null;
    data.isMaster = this.canCreateMaster() && this.model.isExternalUser;

    if (!this.canCreateMaster()) {
      data.accountManagerId = this.gestora.id;
    }

    if (data.lockoutEndDateUtc != null) {
      data.lockoutEndDateUtc = new Date(data.lockoutEndDateUtc.toString().replace( /(\d{2})\/(\d{2})\/(\d{4})/, '$2/$1/$3'));
    }

    const method = editing
      ? this.service.UpdateUser(this.model.id, data)
      : this.service.RegisterUser(data);

    return method;
  }

  coalesce(value: any) { return value || ''; }

  hasError(prop: string) {
    if (!this.canValidate) {
      return false;
    }

    return !this.validators[prop](this.model[prop]);
  }

  getErrors(): string[] {
    const validationErrors = [];

    Object.keys(this.validators).forEach(k => {
      if (!this.validators[k](this.model[k])) {
        validationErrors.push(k);
      }
    });

    return validationErrors;
  }

  canCreateMaster() {
    const roles = [
      Roles.CreateMasterUser
    ];
    return this.authService.hasRoles(roles);
  }

  updateTitle() {
    if (this.canCreateMaster()) {
      if (this.model.isExternalUser) {
        this.tipoUsuario = 'Master';
      } else {
        this.tipoUsuario = 'Interno';
      }
    } else {
      this.tipoUsuario = 'Subordinado';
    }
  }

  canCreateSubordinate() {
    const roles = [
      Roles.CreateSubordinateUser
    ];
    return this.authService.hasRoles(roles);
  }
}
