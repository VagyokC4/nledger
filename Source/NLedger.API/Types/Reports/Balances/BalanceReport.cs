using System.Collections.Generic;
using NLedger.API.Extensions;

namespace NLedger.API.Types.Reports.Balances
{
    public class BalanceReport
    {
        public decimal               Balance  { get; set; }
        public List<AccountBalances> Accounts { get; set; } = new List<AccountBalances>();

        public static BalanceReport FromLedgerResponse(string ledgerResponse)
        {
            return new BalanceReport().BuildBalanceReport(ledgerResponse);
        }
    }
}                                                   