import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { ITipo, Tipo } from '../../../model/domain/Tipo';

@Component({
  selector: 'app-radio-list',
  templateUrl: './radio-list.component.html',
  styleUrls: ['./radio-list.component.scss']
})
export class RadioListComponent implements OnInit {
  @Input() list: ITipo[] = null;
  @Input() name = '';
  @Input() model = null;
  @Output() modelChange = new EventEmitter<number>();
  @Input() invalid = false;

  constructor() {
  }

  ngOnInit() {

  }

  onSelectionChange(item: Tipo<number>) {
    this.model = item.id;
    this.modelChange.emit(this.model);
  }
}
