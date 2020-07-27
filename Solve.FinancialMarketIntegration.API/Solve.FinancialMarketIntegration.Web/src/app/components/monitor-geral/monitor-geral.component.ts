import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DateTime } from 'luxon';
import { TicketMonitorStatus } from '../../model/StatusBoleta';
import { TicketService } from '../../services/ticket.service';
import { MenuConstants } from '../../infrastructure/MenuConstants';
import { TicketType, TicketStatus } from '../../model/domain/Entity';

@Component({
  selector: 'app-monitor-geral',
  templateUrl: './monitor-geral.component.html',
  styleUrls: ['./monitor-geral.component.scss']
})
export class MonitorGeralComponent implements OnInit {
  statusList: TicketMonitorStatus[] = [];
  TicketType = TicketType;

  columns = [
    'Status',
    'Quota',
    'CETIP',
    'SELIC',
    'Futures',
    'Margin',
    'Contracted',
    'CurrencyTerm',
    'VariableIncome',
    'SwapCetip'
  ];

  statusMappings = {
    [TicketStatus.Draft]: 'Em Elaboração'
  };

  constructor(
    private ticketService: TicketService,
    private router: Router
  ) { }

  ngOnInit() {
    this.getStatus();
  }

  goTo(boletaId: number) {
    this.router.navigate([MenuConstants.Todos, { boletaId: boletaId }]);
  }

  getStatus(): void {
    this.ticketService.GetWorflowStatus().subscribe(status => {
      this.statusMappings = Object.assign(this.statusMappings, status);
      },
      () => {},
      () => this.getSummary()
    );
  }

  getSummary(): void {
    // TODO: criar dicionário para estes valores
    const ticketMappings = {
      [TicketType.Quota]: 'Quota',
      [TicketType.CETIP]: 'CETIP',
      [TicketType.SELIC]: 'SELIC',
      [TicketType.Contracted]: 'Contracted',
      [TicketType.Futures]: 'Futures',
      [TicketType.SwapCetip]: 'SwapCetip',
      [TicketType.VariableIncome]: 'VariableIncome',
      [TicketType.CurrencyTerm]: 'CurrencyTerm',
      [TicketType.Margin]: 'Margin',
    };

    this.ticketService.Summary(
      DateTime.local().toJSDate(),
      DateTime.local().toJSDate()
    ).subscribe(data => {

      const newData = data
        .sort((a, b) => {
          if ((a.statusId || 0) >= (b.statusId || 0)) { return 1; }
          if ((a.statusId || 0) < (b.statusId || 0)) { return -1; }
        })
        .map(d => {
          const newRow: TicketMonitorStatus = {
            Status: this.statusMappings[d.statusId || 0],
            Quota: 0,
            CETIP: 0,
            SELIC: 0,
            Contracted: 0,
            CurrencyTerm: 0,
            VariableIncome: 0,
            SwapCetip: 0,
            Margin: 0,
            Futures: 0
          };

          (d.tickets || [])
            .forEach((t: any) => {
              newRow[ticketMappings[t.ticketTypeId]] = t.count;
            });

          return newRow;
        });

        this.statusList = newData;
    }, () => { });
  }
}
