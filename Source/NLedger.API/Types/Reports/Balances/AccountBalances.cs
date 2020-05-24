using System.Collections.Generic;

namespace NLedger.API.Types.Reports.Balances
{
    public class AccountBalances
    {
        public AccountBalances(string account, decimal balance)
        {
            Account = account;
            Balance = balance;
        }

        public string  Account { get; set; }
        public decimal Balance { get; set; }

        public List<AccountBalance> Accounts { get; set; } = new List<AccountBalance>();
    }
}