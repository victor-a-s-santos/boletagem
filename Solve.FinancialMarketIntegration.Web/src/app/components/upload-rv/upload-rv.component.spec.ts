import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadRvComponent } from './upload-tv.rvmponent';

describe('UploadRvComponent', () => {
  let component: UploadRvComponent;
  let fixture: ComponentFixture<UploadRvComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UploadRvComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UploadRvComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
