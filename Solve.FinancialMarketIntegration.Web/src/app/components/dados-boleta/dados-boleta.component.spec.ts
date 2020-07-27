import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DadosBoletaComponent } from './dados-boleta.component';

describe('DadosBoletaComponent', () => {
  let component: DadosBoletaComponent;
  let fixture: ComponentFixture<DadosBoletaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DadosBoletaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DadosBoletaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
