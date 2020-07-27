import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MargemComponent } from './margem.component';

describe('MargemComponent', () => {
  let component: MargemComponent;
  let fixture: ComponentFixture<MargemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MargemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MargemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
