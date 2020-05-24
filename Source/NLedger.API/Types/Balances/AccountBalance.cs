namespace NLedger.API.Types.Balances
{
    public class AccountBalance
    {
        public AccountBalance(string account, decimal balance)
        {
            Account = account;
            Balance = balance;
        }

        public string  Account { get; set; }
        public decimal Balance { get; set; }
    }
}