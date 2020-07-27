import { Component, Input, ViewChild, OnChanges } from '@angular/core';
import { ValidatorService } from '../../services/validator.service';
import { NgForm } from '@angular/forms';
import { Portfolio } from '../../model/domain/Entity';

@Component({
  selector: 'app-dados-carteira',
 templateUrl: './dados-carteira.component.html',
  styleUrls: ['./dados-carteira.component.scss']
})
export class DadosCarteiraComponent implements OnChanges {
  @ViewChild('thisForm') carteiraForm: NgForm;
  @Input() tipo = '';
  @Input() carteira: Portfolio = new Portfolio();
  @Input() canEdit = false;

  constructor(public validator: ValidatorService) { }

  ngOnChanges() {
    this.setViewOnly();
  }

  setViewOnly() {
    setTimeout(() => {
      if (!this.canEdit) {
        Object.values(this.carteiraForm.controls).forEach(c => {
          c.disable();
        });
      }
    }, 10);
  }

  public Submit(): void {
    // tslint:disable-next-line: forin
    for (const inner in this.carteiraForm.controls) {
      this.carteiraForm.form.get(inner).markAsTouched();
      this.carteiraForm.form.get(inner).updateValueAndValidity();
    }
  }
}
