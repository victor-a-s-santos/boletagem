import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './components/home/home.component';
import { CetipComponent } from './components/cetip/cetip.component';
import { CotaComponent } from './components/cota/cota.component';
import { SelicComponent } from './components/selic/selic.component';
import { MargemComponent } from './components/margem/margem.component';
import { CompromissadaCompraComponent } from './components/compromissada-compra/compromissada-compra.component';
import { TermoMoedaComponent } from './components/termo-moeda/termo-moeda.component';
import { RendaVariavelComponent } from './components/rv/rv.component';
import { SwapCetipComponent } from './components/swap-cetip/swap-cetip.component';
import { MonitorGeralComponent } from './components/monitor-geral/monitor-geral.component';
import { MonitoresComponent } from './components/monitores/monitores.component';
import { FuturosComponent } from './components/futuros/futuros.component';
import { AuthGuard } from './components/auth/auth.guard';
import { LoginComponent } from './components/auth/login/login.component';
import { TestesComponent } from './components/infrastructure/testes/testes.component';
import { CadastroUsuarioComponent } from './components/cadastro-usuario/cadastro-usuario.component';
import { GruposComponent } from './components/grupos/grupos.component';
import { CadastroGrupoComponent } from './components/grupos/cadastro.component';
import { UsuariosComponent } from './components/usuarios/usuarios.component';
import { PasswordResetComponent } from './components/auth/password-reset/password-reset.component';
import { UploadRvComponent } from './components/upload-rv/upload-rv.component';

const appRoutes: Routes = [
  {
    path: '',
    component: HomeComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: '',
        canActivateChild: [AuthGuard],
        children: [
          { path: 'quota', component: CotaComponent },
          { path: 'quota/:id', component: CotaComponent },
          { path: 'cetip', component: CetipComponent },
          { path: 'cetip/:id', component: CetipComponent },
          { path: 'selic', component: SelicComponent },
          { path: 'selic/:id', component: SelicComponent },
          { path: 'contracted', component: CompromissadaCompraComponent },
          { path: 'contracted/:id', component: CompromissadaCompraComponent },
          { path: 'futures', component: FuturosComponent },
          { path: 'futures/:id', component: FuturosComponent },
          { path: 'swap-cetip', component: SwapCetipComponent },
          { path: 'swap-cetip/:id', component: SwapCetipComponent },
          { path: 'margin', component: MargemComponent },
          { path: 'margin/:id', component: MargemComponent },
          { path: 'currency-term', component: TermoMoedaComponent },
          { path: 'currency-term/:id', component: TermoMoedaComponent },
          { path: 'rv', component: RendaVariavelComponent },
          { path: 'rv/:id', component: RendaVariavelComponent },
          { path: 'status', component: MonitorGeralComponent },
          { path: 'todos', component: MonitoresComponent },
          { path: 'testes', component: TestesComponent },
          { path: 'usuarios', component: UsuariosComponent },
          { path: 'cadastro-usuario', component: CadastroUsuarioComponent },
          { path: 'cadastro-usuario/:id', component: CadastroUsuarioComponent },
          { path: 'grupos', component: GruposComponent },
          { path: 'grupos/:id', component: GruposComponent },
          { path: 'grupos-cadastro', component: CadastroGrupoComponent },
          { path: 'grupos-cadastro/:id', component: CadastroGrupoComponent },
          { path: 'uploads/pesc', component: UploadRvComponent }
        ]
      }
    ]
  },
  { path: 'login', component: LoginComponent },
  { path: 'password-reset/:login', component: PasswordResetComponent }
];


@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes, {
      enableTracing: false // <-- debugging purposes only
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
