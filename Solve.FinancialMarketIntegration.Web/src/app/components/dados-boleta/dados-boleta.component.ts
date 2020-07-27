import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-dados-boleta',
  templateUrl: './dados-boleta.component.html',
  styleUrls: ['./dados-boleta.component.scss']
})
export class DadosBoletaComponent implements OnInit {

  @Input() titulo = '';
  @Input() operationDate: Date = null;
  @Input() id: number = null;
  @Input() statusDescription: string = null;
  constructor() { }

  ngOnInit() {
  }
}
