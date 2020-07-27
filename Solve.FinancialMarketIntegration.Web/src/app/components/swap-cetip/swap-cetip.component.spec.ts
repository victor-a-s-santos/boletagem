import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SwapCetipComponent } from './swap-cetip.component';

describe('SwapCetipComponent', () => {
  let component: SwapCetipComponent;
  let fixture: ComponentFixture<SwapCetipComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SwapCetipComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SwapCetipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
