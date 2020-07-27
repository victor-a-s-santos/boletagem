export interface StatusDetail {

  statusId: number;
  tickets?: StatusDetailTicket[];
}

export interface StatusDetailTicket {
  ticketTypeId: number;
  count: number;
}
