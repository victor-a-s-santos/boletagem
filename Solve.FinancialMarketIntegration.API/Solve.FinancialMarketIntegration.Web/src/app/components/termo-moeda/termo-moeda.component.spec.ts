import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TermoMoedaComponent } from './termo-moeda.component';

describe('CotaComponent', () => {
  let component: TermoMoedaComponent;
  let fixture: ComponentFixture<TermoMoedaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [TermoMoedaComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TermoMoedaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
