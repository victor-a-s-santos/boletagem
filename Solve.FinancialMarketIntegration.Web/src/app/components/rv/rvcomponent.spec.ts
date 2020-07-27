import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RendaVariavelComponent } from './rv.component';

describe('MargemComponent', () => {
  let component: RendaVariavelComponent;
  let fixture: ComponentFixture<RendaVariavelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RendaVariavelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RendaVariavelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
