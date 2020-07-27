import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { VariableIncome, VariableIncomeLineItem } from '../../model/domain/VariableIncome';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError } from 'rxjs/operators';

import { TicketService } from '../../services/ticket.service';
import { ValidatorService } from '../../services/validator.service';
import { TipoOperacao } from '../../model/domain/Entity';
import { DialogService } from '../../services/dialog.service';
import { AuthService } from '../../services/auth.service';
import { Roles } from '../auth/auth.role';
import { SnackbarService } from '../../services/snackbar.service';

@Component({
  selector: 'app-rv',
  templateUrl: './rv.component.html',
  styleUrls: ['./rv.component.css']
})
export class RendaVariavelComponent implements OnInit {

  constructor(
    private service: TicketService,
    public validator: ValidatorService,
    private route: ActivatedRoute,
    private snackBarService: SnackbarService,
    private router: Router,
    private location: Location,
    private dialogService: DialogService,
    private authService: AuthService
  ) { }

  model: VariableIncome;
  canValidate = false;
  isEditing = false;
  columns: string[];

  validators = {
    portfolioDocument: (value) => this.validator.validateDocument(value)
  };

  ngOnInit() {
    this.modelInit();
  }

  getTicket() {
    this.modelInit();
  }

  modelInit() {
    this.model = new VariableIncome();
    this.columns = ['MarketType', 'BuySell', 'Asset', 'Amount', 'Price', 'TotalPrice'];

    const id = +this.route.snapshot.paramMap.get('id');
    this.model.id = id;

    if (id) {
      this.service.GetVariableIncomeTicket(id).subscribe(q => {
        Object.assign(this.model, q);

        // Formatações

        this.isEditing = true;
      });
    } else {
      // this.mock();

      if (!this.model.statusId) {
        this.model.operationDate = new Date();
      }
    }
  }

  Submit(f: NgForm) { }

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

  canChangeStatus() {
    return this.model.statusId && this.authService.hasRoles(this.model.statusRequiredRoles);
  }

  approve() {
    this.dialogService.confirm(
      'Aprovar Boleta',
      'Deseja aprovar esta boleta?'
    ).subscribe(result => {
      if (result) {
        this.service.Approve(this.model.id, '').subscribe(() => {
          this.snackBarService.open(
            'Boleta atualizada com sucesso.', 'OK', {
              verticalPosition: 'top',
              horizontalPosition: 'right'
            });

          this.getTicket();
        });
      }
    });
  }

  reject() {
    this.dialogService.confirm(
      'Reprovar Boleta',
      'Deseja reprovar esta boleta?'
    ).subscribe(result => {
      if (result) {
        this.service.Reject(this.model.id, '').subscribe(() => {
          this.snackBarService.open(
            'Boleta atualizada com sucesso.', 'OK', {
              verticalPosition: 'top',
              horizontalPosition: 'right'
            });
          this.getTicket();
        });
      }
    });
  }
}
