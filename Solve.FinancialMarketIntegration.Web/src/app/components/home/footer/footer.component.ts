import { Component, OnInit } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { IEnvironment } from '../../../../environments/environment.base';
import { TicketService } from '../../../services/ticket.service';

@Component({
    selector: 'app-footer',
    templateUrl: './footer.component.html',
    styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {
    constructor(
      private service: TicketService
    ) {
        this.environment = environment;
    }

    environment: IEnvironment;
    versionData: any = null;

    ngOnInit() {
      this.service.ApiVersion().subscribe(v => this.versionData = v);
    }
}
