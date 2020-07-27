import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroGrupoComponent } from './cadastro.component';

describe('CadastroGrupoComponent', () => {
  let component: CadastroGrupoComponent;
  let fixture: ComponentFixture<CadastroGrupoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CadastroGrupoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CadastroGrupoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
