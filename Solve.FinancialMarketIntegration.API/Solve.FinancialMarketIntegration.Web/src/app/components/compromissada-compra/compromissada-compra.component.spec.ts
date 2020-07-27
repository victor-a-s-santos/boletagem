import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompromissadaCompraComponent } from './compromissada-compra.component';

describe('CompromissadaComponent', () => {
  let component: CompromissadaCompraComponent;
  let fixture: ComponentFixture<CompromissadaCompraComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompromissadaCompraComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompromissadaCompraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
