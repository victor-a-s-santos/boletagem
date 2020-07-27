export interface AuthRole {
  id: number;
  name: string;
}


export enum Roles {
  CreateTicketFundQuota = 1,
  CreateTicketPrivateFixedIncome = 2,
  CreateTicketPublicFixedIncome = 3,
  CreateTicketFutures = 4,
  CreateTicketMargin = 5,
  CreateTicketContracted = 6,
  CreateTicketCurrencyTerm = 7,
  CreateTicketVariableIncome = 8,
  CreateTicketSwapCetip = 9,

  ApprovePendingApprovalAdm = 10,
  ApprovePendingSettlementOpen = 11,
  ApproveActiveSettlementOpen = 12,
  ApprovePendingSettlementMiddle = 13,
  ApproveActiveSettlementMiddle = 14,
  ApprovePendingSettlementCustody = 15,
  ApproveActiveSettlement = 16,

  ViewApprovalHistory = 17,

  ViewMonitorFundQuota = 18,
  ViewMonitorPrivateFixedIncome = 19,
  ViewMonitorPublicFixedIncome = 20,
  ViewMonitorFutures = 21,
  ViewMonitorMargin = 22,
  ViewMonitorContracted = 23,
  ViewMonitorCurrencyTerm = 24,
  ViewMonitorVariableIncome = 25,
  ViewMonitorSwapCetip = 26,
  ApproveAccountManagerPendingTicket = 27,
  CreateMasterUser = 28,
  CreateSubordinateUser = 29
}
