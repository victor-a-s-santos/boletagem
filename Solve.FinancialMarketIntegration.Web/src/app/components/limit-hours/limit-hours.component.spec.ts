import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LimitHoursComponent } from './limit-hours.component';

describe('LimitHoursComponent', () => {
  let component: LimitHoursComponent;
  let fixture: ComponentFixture<LimitHoursComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LimitHoursComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LimitHoursComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
