import { Injectable } from "@angular/core";
import { TicketMonitorStatus } from "../model/StatusBoleta";

@Injectable({
  providedIn: "root"
})
export class MonitorService {
  constructor() {}

  // ListarStatusDeBoleta(): TicketMonitorStatus[] {
  //   const s1 = new TicketMonitorStatus();
  //   s1.Status = "Teste 1";
  //   s1.CETIP = 1111;
  //   s1.Cotas = 9999;
  //   s1.CotasTED = 3333;
  //   s1.Futuros = 4444;
  //   s1.RV = 5555;
  //   s1.SELIC = 6666;
  //   s1.Termo = 7777;

  //   return [s1, s1, s1, s1, s1, s1, s1, s1, s1];
  // }
}
