import * as jquery from 'jquery';

/* INICIO ANGULAR */
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, LOCALE_ID } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import locale from '@angular/common/locales/pt';
import { registerLocaleData, DatePipe, DecimalPipe } from '@angular/common';

/* FIM ANGULAR */



/* INICIO ANGULAR MATERIAL */

import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatToolbarModule } from '@angular/material/toolbar';
import { ReactiveFormsModule } from '@angular/forms';
import {
  MatNativeDateModule,
  MatFormFieldModule,
  MatInputModule,
  MatTableModule,
  MatButtonModule,
  MatSnackBarModule,
  MAT_SNACK_BAR_DEFAULT_OPTIONS,
  MatDialogModule
} from '@angular/material';

import {
  MatMomentDateModule,
  MAT_MOMENT_DATE_ADAPTER_OPTIONS
} from '@angular/material-moment-adapter';

// https://github.com/text-mask/text-mask/tree/master/angular2
import { TextMaskModule } from 'angular2-text-mask';

// https://www.npmjs.com/package/ngx-mask
import { NgxMaskModule, IConfig } from 'ngx-mask';
export const NgxMaskModule_options: Partial<IConfig> | (() => Partial<IConfig>) = null;


// https://www.npmjs.com/package/ngx-currency
import { NgxCurrencyModule } from 'ngx-currency';

// https://github.com/mattlewis92/angular-confirmation-popover
import { ConfirmationPopoverModule } from 'angular-confirmation-popover';

import { NgxUiLoaderModule, NgxUiLoaderConfig, NgxUiLoaderRouterModule } from 'ngx-ui-loader';
import { NgSelectModule } from '@ng-select/ng-select';

/* FIM TERCEIRO */


import { AppComponent } from './app.component';

registerLocaleData(locale);

import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { CetipComponent } from './components/cetip/cetip.component';
import { CotaComponent } from './components/cota/cota.component';
import { SelicComponent } from './components/selic/selic.component';
import { MargemComponent } from './components/margem/margem.component';
import { TermoMoedaComponent } from './components/termo-moeda/termo-moeda.component';
import { RendaVariavelComponent } from './components/rv/rv.component';
import { SwapCetipComponent } from './components/swap-cetip/swap-cetip.component';
import { DadosBoletaComponent } from './components/dados-boleta/dados-boleta.component';
import { DadosCarteiraComponent } from './components/dados-carteira/dados-carteira.component';
import { RadioListComponent } from './components/infrastructure/radio-list/radio-list.component';
import { MonitorGeralComponent } from './components/monitor-geral/monitor-geral.component';
import { MonitoresComponent } from './components/monitores/monitores.component';
import { FuturosComponent } from './components/futuros/futuros.component';
import { AppRoutingModule } from './app-routing.module';
import { LoginComponent } from './components/auth/login/login.component';
import { AuthInterceptor } from './components/auth/auth.interceptor';
import { CompromissadaCompraComponent } from './components/compromissada-compra/compromissada-compra.component';
import { TypesComponent } from './components/types/types.component';
import { ValidationSummaryComponent } from './components/infrastructure/validation-summary/validation-summary.component';
import { TestesComponent } from './components/infrastructure/testes/testes.component';
import { ConfirmationDialogComponent } from './components/infrastructure/confirmation-dialog/confirmation-dialog.component';
import { DebounceClickDirective } from './debounce-click.directive';
import { LoaderService } from './services/loader.service';
import { SnackbarService } from './services/snackbar.service';
import { Interceptor } from './interceptor.module';
import { CadastroUsuarioComponent } from './components/cadastro-usuario/cadastro-usuario.component';
import { GruposComponent } from './components/grupos/grupos.component';
import { CadastroGrupoComponent  } from './components/grupos/cadastro.component';
import { UsuariosComponent  } from './components/usuarios/usuarios.component';
import { LimitHoursComponent } from './components/limit-hours/limit-hours.component';
import { RejectDialogComponent } from './components/infrastructure/reject-dialog/reject-dialog.component';
import { PasswordResetComponent } from './components/auth/password-reset/password-reset.component';
import { UploadRvComponent } from './components/upload-rv/upload-rv.component';


import { FooterComponent } from './components/home/footer/footer.component';
import { WorkflowHistoryComponent } from './workflow-history/workflow-history.component';

const ngxUiLoaderConfig: NgxUiLoaderConfig = {
  bgsColor: '#4dc6bd',
  bgsOpacity: 0.5,
  bgsPosition: 'bottom-right',
  bgsSize: 60,
  bgsType: 'ball-scale-multiple',
  fgsColor: '#4dc6bd',
  fgsPosition: 'center-center',
  fgsSize: 60,
  fgsType: 'ball-scale-multiple',
  pbColor: '#FF0000',
  pbDirection: 'ltr',
  pbThickness: 5,
  textColor: '#FFFFFF',
  textPosition: 'center-center',
  hasProgressBar: false
};

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CotaComponent,
    CetipComponent,
    SelicComponent,
    MargemComponent,
    CompromissadaCompraComponent,
    TermoMoedaComponent,
    RendaVariavelComponent,
    SwapCetipComponent,
    DadosBoletaComponent,
    DadosCarteiraComponent,
    RadioListComponent,
    TypesComponent,
    MonitorGeralComponent,
    MonitoresComponent,
    FuturosComponent,
    ValidationSummaryComponent,
    ConfirmationDialogComponent,
    TestesComponent,
    DebounceClickDirective,
    LoginComponent,
    CadastroUsuarioComponent,
    GruposComponent,
    CadastroGrupoComponent,
    UsuariosComponent,
    UploadRvComponent,
    WorkflowHistoryComponent,
    LimitHoursComponent,
    RejectDialogComponent,
    FooterComponent,
    PasswordResetComponent
  ],
  imports: [
    ReactiveFormsModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    RouterModule,
    AppRoutingModule,
    MatButtonModule,
    MatCardModule,
    MatCheckboxModule,
    MatDialogModule,
    MatDatepickerModule,
    MatExpansionModule,
    MatFormFieldModule,
    MatInputModule,
    MatMomentDateModule,
    MatNativeDateModule,
    MatSelectModule,
    MatSnackBarModule,
    MatTableModule,
    MatToolbarModule,
    TextMaskModule,
    NgxCurrencyModule,
    NgxMaskModule.forRoot(NgxMaskModule_options),
    ConfirmationPopoverModule.forRoot({
      confirmButtonType: 'danger' // set defaults here
    }),
    NgxUiLoaderRouterModule,
    NgxUiLoaderModule.forRoot(ngxUiLoaderConfig),
    Interceptor,
    NgSelectModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    DatePipe,
    DecimalPipe,
    { provide: LOCALE_ID, useValue: 'pt-BR' },
    { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } },
    { provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: { duration: 3000 } },
    LoaderService,
    SnackbarService,
  ],
  bootstrap: [AppComponent],
  exports: [],
  entryComponents: [
    ConfirmationDialogComponent,
    RejectDialogComponent
  ]
})
export class AppModule { }
