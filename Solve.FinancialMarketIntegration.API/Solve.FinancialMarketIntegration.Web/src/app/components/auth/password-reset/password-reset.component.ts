import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { SnackbarService } from '../../../services/snackbar.service';
import { FormControl, Validators, FormGroup, ValidatorFn, AbstractControl } from '@angular/forms';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-password-reset',
  templateUrl: './password-reset.component.html',
  styleUrls: ['./password-reset.component.css']
})
export class PasswordResetComponent implements OnInit {
  message: string;
  isAuthenticating = false;
  usernameURL: string = null;

  // FORM CONTROLS
  username = new FormControl({value: '', disabled: true}, [Validators.required, Validators.minLength(3)]);
  oldPassword = new FormControl('', [Validators.required, Validators.minLength(4)]);
  newPassword = new FormControl('', [ ]);
  newPasswordConfirmation = new FormControl('', [ ]);

  // FORM GROUPS
  login = new FormGroup({
    username: this.username,
    oldPassword: this.oldPassword,
    newPassword: this.newPassword,
    newPasswordConfirmation: this.newPasswordConfirmation
  });

  constructor(private authService: AuthService,
    private router: Router,
    private snackBarService: SnackbarService,
    private route: ActivatedRoute
  ) {

  }

  ngOnInit() {
    this.usernameURL = this.route.snapshot.paramMap.get('login');

    this.newPassword.setValidators([
      Validators.required,
      Validators.minLength(4)
    ]);

    this.newPasswordConfirmation.setValidators([
      Validators.required,
      Validators.minLength(4),
      this.newPasswordConfirmationValid(this.newPassword)
    ]);

    if (this.usernameURL != null) {
      this.username.setValue(this.usernameURL);
    } else {
      this.username.enable();
    }
  }

  resetPassword() {
    this.username.markAsDirty();
    this.oldPassword.markAsDirty();
    this.newPassword.markAsDirty();
    this.newPasswordConfirmation.markAsDirty();

    if (!this.login.valid) { return; }

    this.isAuthenticating = true;

    if (this.newPassword.value === this.newPasswordConfirmation.value) {
      this.authService.resetPassword(
        this.username.value,
        this.oldPassword.value,
        this.newPassword.value
      )
      .pipe(finalize(() => this.isAuthenticating = false))
      .subscribe((result) => {
        if (result) {
          this.router.navigateByUrl('/');
          return;
        }

        this.snackBarService.open('Usuário ou senha inválidos', 'OK');
      }, (result) => {
        if (result.status === 400) {
          this.snackBarService.open('Usuário ou senha inválidos', 'OK');
        } else if (result.status === 401) {
          this.snackBarService.open('A nova senha já foi utilizada anteriormente.', 'OK');
        } else if (result.status === 403) {
          this.snackBarService.open('Usuário não existe ou bloqueado', 'OK');
        } else if (result.message) {
          this.snackBarService.open(result.message, 'OK');
        } else if (result.error) {
          const { error: { message } } = result;
          this.snackBarService.open(message, 'OK');
        }
      });
    } else {
      this.snackBarService.open('A confirmação de senha não bate com a nova senha.', 'OK');

      this.isAuthenticating = false;
    }
  }

  newPasswordConfirmationValid(controlNewPassword: FormControl): ValidatorFn {
      return (control: AbstractControl): { [key: string]: boolean } | null => {
          if (control.value !== undefined && (control.value !== controlNewPassword.value)) {
              return { 'newPasswordConfirmation': true };
          }
          return null;
      };
  }
}
