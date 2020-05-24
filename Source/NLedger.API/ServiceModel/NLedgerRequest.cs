namespace NLedger.API.ServiceModel
{
    public class NLedgerRequest
    {
        public string LedgerFile  { get; set; }
        public string LedgerQuery { get; set; }
    }

    public class NLedgerResponse
    {
        public string Result { get; set; }
    }
}