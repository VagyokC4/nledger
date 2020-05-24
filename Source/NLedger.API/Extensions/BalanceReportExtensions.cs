using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLedger.API.Types.Balances;
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
                // Parse name
                var account     = line.Substring(22);
                var accountTrim = account.Trim();

                // Calculate Index
                var distance = account.Length - accountTrim.Length;
                var index    = distance / 2;

                if (index == 0)
                {
                    // Add Root Account Name
                    accounts = new List<string> {accountTrim};
                }
                else
                {
                    if (accounts.Count <= index)
                        // Add Account Name
                        accounts.Add(accountTrim);
                    else
                        // Replace Account Name
                        accounts[index] = accountTrim;
                }

                // Build Account Name
                var sb = new StringBuilder();
                for (var i = 0; i <= index; i++) sb.Append($"{accounts[i]}:");

                if (index == 0)
                    // Add to Root Account
                    balanceReport.Accounts.Add(new AccountBalances(sb.ToString().SplitOnLast(':').First(), GetAccountBalance(line)));
                else
                    // Add to Sub Account
                    balanceReport.Accounts.Last().Accounts.Add(new AccountBalance(sb.ToString().SplitOnLast(':').First(), GetAccountBalance(line)));
            }

            // Normalize ToLines
            var lines = nLedgerReport.Normalize().ToLines();

            // Parse Lines
            foreach (var line in lines)
                if (line.StartsWith("--------------------"))
                {
                    // Second to last line, we can record balance and exit early
                    balanceReport.Balance = lines.Last().SplitOnLast(' ').Last().ToDecimal();
                    break;
                }
                else
                {
                    // Add Current Line
                    PopulateBalanceReportLine(line);
                }

            // Return Balance Report
            return balanceReport;
        }

        private static decimal GetAccountBalance(string line)
        {
            return line.Substring(0, 22).Trim().Substring(1).ToDecimal();
        }
    }
}