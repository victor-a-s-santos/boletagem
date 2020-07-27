import { Component, OnInit, AfterViewChecked, ViewChild } from '@angular/core';
import { Location, DecimalPipe, DatePipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { conformToMask } from 'text-mask-core';

import { UploadRv } from '../../model/domain/UploadRv';
import { TicketService } from '../../services/ticket.service';
import { ValidatorService } from '../../services/validator.service';
import { DialogService } from '../../services/dialog.service';
import { SnackbarService } from '../../services/snackbar.service';

@Component({
  selector: 'app-upload-rv',
  templateUrl: './upload-rv.component.html',
  styleUrls: ['./upload-rv.component.css']
})
export class UploadRvComponent implements OnInit, AfterViewChecked {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private location: Location,
    private service: TicketService,
    private validator: ValidatorService,
    private dialogService: DialogService,
    private snackBarService: SnackbarService,
    private datePipe: DatePipe
  ) { }

  @ViewChild('thisForm') public frm: NgForm;

  isEditing = false;
  canValidate = false;
  isReady = false;
  ngForm: NgForm;
  AccountManagers = [];

  model: UploadRv;

  afuConfig = null;

  validators = {};

  // Init
  ngOnInit() {
    this.parametersInit();
    this.validatorsInit();
    this.modelInit();
  }

  ngAfterViewChecked() {
    if (!this.isReady) {
      setTimeout(() => {
        if (!this.canEdit()) {
          Object.values(this.frm.controls).forEach(c => {
            c.disable();
          });
        }
      }, 0);
      this.isReady = true;
    }
  }

  parametersInit() {
    this.service.GetUploadAccountManagersList().subscribe(q => {
      Object.assign(this.AccountManagers, q);
      this.model.accountManagerId = this.AccountManagers[0].id;
    });
  }

  validatorsInit() {
  }

  modelInit() {
    this.model = new UploadRv();
  }

  canEdit() {
    return true;
  }

  validateForm(f: NgForm): boolean {
    return true;
  }

  submit() {
    const editing = this.isEditing;

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
          ? 'Boleta alterada.'
          : 'Boleta cadastrada.';

        this.snackBarService.open(
          msg, 'OK', {
            verticalPosition: 'top',
            horizontalPosition: 'right'
          });
      }, () => {});
  }

  register(f: NgForm) {
    const editing = this.isEditing;

    if (!this.canEdit()) {
      return;
    }

    try {
      const isValid = this.validateForm(f);
      if (isValid) {
        let data = Object.assign({}, this.model);

        return this.sendSubmit(data, editing);
      }
    } catch (error) {
      return throwError(error);
    }
  }

  openFile(event) {
    const input = event.target;
    const _ = this;
    const reader = new FileReader();
    reader.onload = function(){
      const dataURL = reader.result;
      _.model.fileBase64 = dataURL.toString();
    };
    reader.readAsDataURL(input.files[0]);
  }

  sendSubmit(data: UploadRv, editing: boolean = false) {
    const method = this.service.UploadVariableIncomeTicketsFile(data);

    return method
      .pipe(catchError(err => {
        return throwError('Erro ao cadastrar a boleta.');
      }))
      .pipe(map((margin: UploadRv) => {
        const url = this.router.createUrlTree([this.model.id], { relativeTo: this.route });
        this.location.go(url.toString());

        return this.model;
      }));
  }
}
