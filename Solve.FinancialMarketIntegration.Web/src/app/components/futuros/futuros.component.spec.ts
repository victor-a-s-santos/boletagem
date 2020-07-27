import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FuturosComponent } from './futuros.component';

describe('FuturosComponent', () => {
  let component: FuturosComponent;
  let fixture: ComponentFixture<FuturosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FuturosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FuturosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
