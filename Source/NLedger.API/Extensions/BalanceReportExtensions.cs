using System;
using System.Collections.Generic;
using System.Globalization;
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
            var lines = nLedgerReport.GetLines().ToList();

            // Parse Lines
            foreach (var line in lines.Take(lines.Count - 2))
                PopulateBalanceReportLine(line);

            // Set Balance
            balanceReport.Balance = lines.Last().SplitOnLast(' ').Last().ToDecimal();

            // Return Balance Report
            return balanceReport;
        }

        private static decimal GetAccountBalance(string line)
        {
            var parts = line.Trim().Split("  ", StringSplitOptions.RemoveEmptyEntries);

            var value = parts.First();

            var amount = char.IsDigit(value.Last()) ? value : value.SplitOnLast(" ").First();

            return decimal.Parse(amount, NumberStyles.Any);
        }
    }
}