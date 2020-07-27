using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Enums
{
    public enum TipoOperacaoEnum
    {
        Compra = 1,
        Venda = 2,
        Aplicacao = 3,
        Resgate = 4,
        Deposito = 5,
        Retirada = 6,
        MoedaComprada = 7,
        MoedaVendida = 8,
        Transferencia = 9
    }
}
