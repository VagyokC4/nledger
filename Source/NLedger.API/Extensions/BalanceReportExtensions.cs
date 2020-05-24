using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLedger.API.Types.Reports.Balances;
using ServiceStack;

namespace NLedger.API.Extensions
{
    public static class BalanceReportExtensions
    {
        public static BalanceReport BuildBalanceReport(this BalanceReport balanceReport, string nLedgerReport)
        {
            return balanceReport.PopulateBalanceReport(nLedgerReport);
        }

        private static BalanceReport PopulateBalanceReport(this BalanceReport balanceReport, string nLedgerReport)
        {
            var accounts = new List<string>();

            void PopulateBalanceReportLine(in string line)
            {
                var account     = line.Substring(22);
                var accountTrim = account.Trim();
                var distance    = account.Length - accountTrim.Length;
                var index       = distance / 2;

                if (index == 0)
                {
                    accounts = new List<string> {accountTrim};
                }
                else
                {
                    if (accounts.Count <= index)
                        accounts.Add(accountTrim);
                    else
                        accounts[index] = accountTrim;
                }

                var sb = new StringBuilder();
                for (var i = 0; i <= index; i++) sb.Append($"{accounts[i]}:");

                if (index == 0)
                    balanceReport.Accounts.Add(new AccountBalances(sb.ToString().SplitOnLast(':').First(), GetAccountBalance(line)));
                else
                    balanceReport.Accounts.Last().Accounts.Add(new AccountBalance(sb.ToString().SplitOnLast(':').First(), GetAccountBalance(line)));
            }

            var lines = nLedgerReport.Normalize().ToLines();

            foreach (var line in lines)
                if (line.StartsWith("--------------------"))
                {
                    balanceReport.Balance = lines.Last().SplitOnLast(' ').Last().ToDecimal();
                    break;
                }
                else
                {
                    PopulateBalanceReportLine(line);
                }

            return balanceReport;
        }

        private static decimal GetAccountBalance(string line)
        {
            return line.Substring(0, 22).Trim().Substring(1).ToDecimal();
        }
    }
}