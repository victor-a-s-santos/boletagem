import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelicComponent } from './selic.component';

describe('SelicComponent', () => {
  let component: SelicComponent;
  let fixture: ComponentFixture<SelicComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelicComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
