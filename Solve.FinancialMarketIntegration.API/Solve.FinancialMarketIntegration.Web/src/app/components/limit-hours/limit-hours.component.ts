import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-limit-hours',
  templateUrl: './limit-hours.component.html',
  styleUrls: ['./limit-hours.component.scss']
})
export class LimitHoursComponent implements OnInit {

  phone: string;
  tel_href_type = 'tel:';
  email: string;
  email_href_type = 'mailto:';

  constructor() { }

  ngOnInit() {
    this.setContact();
  }

  setContact() {
    this.phone = '+55 11 3299-6807';
    this.email = 'suporte_boletagem@socopa.com.br';
  }

}
