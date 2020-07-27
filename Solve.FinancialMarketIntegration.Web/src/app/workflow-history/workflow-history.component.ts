import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { TicketService } from '../services/ticket.service';
import { Step, Result } from '../model/domain/WorkflowHistory';

@Component({
  selector: 'app-workflow-history',
  templateUrl: './workflow-history.component.html',
  styleUrls: ['./workflow-history.component.scss']
})
export class WorkflowHistoryComponent implements OnChanges {

  @Input()
  ticketId: number;

  @Input()
  hideDraftStatus: boolean;

  history: Array<any>;

  constructor(private service: TicketService) { }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.ticketId.currentValue !== changes.ticketId.previousValue) {
      this.getHistory();
    }
  }

  /**
   * @description Call get workflow history from API service
   */
  getHistory(): void {
    if (this.ticketId > 0) {
      this.service.GetHistory(this.ticketId).subscribe(history => {
        const mapped = history
          .filter(s => {
            return s.results.length > 0
              && (!this.hideDraftStatus || (this.hideDraftStatus && s.statusId !== -1));
          })
          .reduce((acc, curr) => {
            return acc.concat(curr.results.map(r =>  {
              const obj = { ...curr, ...r};
              delete obj.results;
              return obj;
            }));
        }, []);

        this.history = mapped.sort((a, b) => {
          const vA = new Date(a.date);
          const vB = new Date(b.date);
          if (vA > vB) {
            return 1;
          } else if (vA < vB) {
            return -1;
          }

          return 0;
        });
      }, () => { this.history = []; });
    }
  }

}
