import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CetipComponent } from './cetip.component';

describe('CetipComponent', () => {
  let component: CetipComponent;
  let fixture: ComponentFixture<CetipComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CetipComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CetipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
