import { TestBed } from '@angular/core/testing';

import { TermoMoedaService } from './termo-moeda.service';

describe('TermoMoedaService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TermoMoedaService = TestBed.get(TermoMoedaService);
    expect(service).toBeTruthy();
  });
});
