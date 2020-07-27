import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CotaComponent } from './cota.component';

describe('CotaComponent', () => {
  let component: CotaComponent;
  let fixture: ComponentFixture<CotaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CotaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CotaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
