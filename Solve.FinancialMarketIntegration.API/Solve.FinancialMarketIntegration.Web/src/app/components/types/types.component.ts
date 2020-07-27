import { Component, OnInit } from '@angular/core';

import { DomainService, TicketType } from '../../services/domain.service';

@Component({
  templateUrl: './types.component.html',
})
export class TypesComponent implements OnInit {

  types: TicketType[] = [];

  constructor(
    private service: DomainService
  ) { }

  ngOnInit() {

  }

  list() {
    this.service.ListTicketTypes().subscribe(types => {
      this.types = types;
    });
  }
}
