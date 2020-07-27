import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MonitorGeralComponent } from './monitor-geral.component';

describe('MonitorGeralComponent', () => {
  let component: MonitorGeralComponent;
  let fixture: ComponentFixture<MonitorGeralComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MonitorGeralComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MonitorGeralComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
