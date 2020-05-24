using System.Threading.Tasks;
using MassTransit;
using NLedger.API.ServiceModel;
using ServiceStack;

namespace NLedger.API
{
    public class NLedgerRequestConsumer : IConsumer<NLedgerRequest>
    {
        private static readonly object lockObject = new object();

        public Task Consume(ConsumeContext<NLedgerRequest> context)
        {
            var queryResult = NLedgerProxy.FromLedgerFile(context.Message.LedgerFile).QueryLedger(context.Message.LedgerQuery);

            context.Respond(new NLedgerResponse
            {
                Result = queryResult.Exception.ToNullIfEmpty() ?? queryResult.Result
            });

            return Task.CompletedTask;
        }
    }
}