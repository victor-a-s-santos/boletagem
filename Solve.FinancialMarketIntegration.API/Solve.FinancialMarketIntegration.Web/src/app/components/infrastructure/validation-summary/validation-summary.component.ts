import { Component, OnInit, Input } from '@angular/core';
import { NgForm, FormGroup } from '@angular/forms';
import { debug, debuglog } from 'util';
import { $ } from 'protractor';

@Component({
  selector: 'app-validation-summary',
  templateUrl: './validation-summary.component.html',
  styleUrls: ['./validation-summary.component.scss']
})
export class ValidationSummaryComponent implements OnInit {
  @Input() form: NgForm[];
  errors: string[] = [];

  constructor() { }

  ngOnInit() {

    // if (typeof(this.form) instanceof [] === false) {
    //   throw new Error('You must supply the validation summary with an NgForm.');
    // }
    for (let index = 0; index < this.form.length; index++) {
      const element = this.form[index];

      // this.resetErrorMessages();

      element.statusChanges.subscribe(status => {
        this.generateErrorMessages();
      });
    }

    // this.form[0].statusChanges.subscribe(f => {
    //   f.statusChanges.subscribe(status => {
    //     this.resetErrorMessages();
    //     this.generateErrorMessages(f.control);
    //   });
    // });
  }

  resetErrorMessages() {
    this.errors.length = 0;
  }

  generateErrorMessages() {
    this.resetErrorMessages();

    this.form.forEach(x => {
      Object.keys(x.controls).forEach((controlName) => {
        const control = x.controls[controlName];
        if (control.errors === null || control.errors.count === 0) {
          return;
        }

        if (!control.touched) {
          return;
        }

        // Handle the 'required' case
        if (control.errors.required) {
          this.errors.push(`${this.getNomeOuLabel(controlName)} é obrigatório`);
        }
        // Handle 'minlength' case
        if (control.errors.minlength) {
          this.errors.push(`${this.getNomeOuLabel(controlName)} minimum length is ${control.errors.minlength.requiredLength}.`);
        }
      });
    });
  }


  getNomeOuLabel(controlName: string) {
    const e = jQuery(`[name=${controlName}]`).closest('.form-group').find('label').text();
    return e || controlName;
  }


  getElement(elementType, attribute, value) {
    const dom = document.getElementsByTagName(elementType);
    const match = new Array();
    for (let index = 0; index < dom.length; index++) {
      const element = dom[index];
      if (element.getAttribute(attribute) === value) {
        match.push(element);
      }
    }

    return match;
  }
}
