import { Location } from '@angular/common';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { SnackbarService } from '../../services/snackbar.service';
import { SecurityService } from '../../services/security.service';
import { Group } from '../../model/domain/Group';
import { Role } from '../../model/domain/Role';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class CadastroGrupoComponent implements OnInit {

  @ViewChild('thisForm') public frm: NgForm;

  public model: Group = null;
  isEditing = false;
  roles: Role[] = [];

  constructor(
    private service: SecurityService,
    private snackBarService: SnackbarService,
    private router: Router,
    private location: Location,
    private route: ActivatedRoute,
    private securityService: SecurityService
  ) { }

  ngOnInit() {
    this.securityService.GetRoles().subscribe(r => {
      this.roles = r;
    });

    this.getGroup();
  }

  getGroup() {
    this.model = new Group();

    const id = +this.route.snapshot.paramMap.get('id');
    this.model.id = id;

    if (id) {
      this.service.GetGroup(id).subscribe(q => {
        Object.assign(this.model, q);

        this.isEditing = true;
      }, () => this.router.navigate(['/grupos-cadastro']));
    }
  }

  submit() {
    const editing = this.isEditing;

    if (editing) {
      this.register(this.frm)
        .pipe(catchError(err => {
          this.snackBarService.open(
            err, 'OK', {
              verticalPosition: 'top',
              horizontalPosition: 'right'
            });
          return throwError(err);
        }))
        .subscribe(r => {
          const msg = editing
            ? 'Grupo alterado.'
            : 'Grupo cadastrado.';

          this.snackBarService.open(
            msg, 'OK', {
              verticalPosition: 'top',
              horizontalPosition: 'right'
            });
        }, () => {});
    }
  }

  register(f: NgForm) {
    const editing = this.isEditing;

    Object.values(f.controls).forEach(c => {
      c.markAsTouched();
      c.updateValueAndValidity();
    });

    const data = Object.assign({}, this.model);

    const method = editing
      ? this.service.UpdateGroup(this.model.id, data)
      : this.service.RegisterGroup(data);

    return method
      .pipe(catchError(err => {
        return throwError('Erro ao cadastrar o grupo.');
      }))
      .pipe(map((group: Group) => {
        if (!editing) {
          this.model.id = group.id;

          this.isEditing = true;

          const url = this.router.createUrlTree([this.model.id], { relativeTo: this.route });
          this.location.go(url.toString());
        }

        return this.model;
      }));
  }
}
