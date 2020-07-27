import { TestBed } from '@angular/core/testing';

import { TicketCetipService } from './ticketCetip.service';

describe('CetipService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TicketCetipService = TestBed.get(TicketCetipService);
    expect(service).toBeTruthy();
  });
});
