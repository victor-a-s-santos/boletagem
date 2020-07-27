import { Component } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { finalize } from 'rxjs/operators';
import { AuthService } from '../../../services/auth.service';
import { LoaderService } from '../../../services/loader.service';
import { SnackbarService } from '../../../services/snackbar.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  message: string;
  isAuthenticating = false;

  // FORM CONTROLS
  username = new FormControl('', [Validators.required, Validators.minLength(3)]);
  password = new FormControl('', [Validators.required, Validators.minLength(4)]);
  participante = new FormControl('', Validators.required);

  // FORM GROUPS
  login = new FormGroup({
    username: this.username,
    password: this.password
  });

  constructor(
    private authService: AuthService,
    private router: Router,
    private snackBarService: SnackbarService,
    private loaderService: LoaderService
    ) {

  }

  signin() {
    this.loaderService.addLoader();
    this.username.markAsDirty();
    this.password.markAsDirty();

    if (!this.login.valid) { return; }

    this.isAuthenticating = true;

    this.authService.login(this.username.value, this.password.value)
      .pipe(finalize(() => this.isAuthenticating = false))
      .subscribe((result) => {
        this.loaderService.removeLoader();

        if (result) {
          this.router.navigateByUrl('/');
          return;
        }

        this.snackBarService.open('Usuário ou senha inválidos', 'OK');
      }, (result) => {
        this.loaderService.removeLoader();

        if (result.status === 400) {
          this.snackBarService.open('Usuário ou senha inválidos', 'OK');
        } else if (result.status === 403) {
          this.snackBarService.open('Usuário não existe ou bloqueado', 'OK');
        } else if (result.status === 401) {
          this.router.navigate(['/password-reset', this.username.value]);
        } else if (result.message) {
          this.snackBarService.open(result.message, 'OK');
        } else if (result.error) {
          const { error: { message } } = result;
          this.snackBarService.open(message, 'OK');
        }
      });
  }
}
