import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DadosCarteiraComponent } from './dados-carteira.component';

describe('DadosCarteiraComponent', () => {
  let component: DadosCarteiraComponent;
  let fixture: ComponentFixture<DadosCarteiraComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DadosCarteiraComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DadosCarteiraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
