using System.Threading.Tasks;
using MassTransit;
using NLedger.API.ServiceModel;
using ServiceStack;

namespace NLedger.API
{
    public class NLedgerRequestConsumer : IConsumer<NLedgerRequest>
    {
        public async Task Consume(ConsumeContext<NLedgerRequest> context)
        {
            var queryResult = await NLedgerProxy.FromLedgerFile(context.Message.LedgerFile).QueryLedger(context.Message.LedgerQuery).ConfigureAwait(false);

            await context.RespondAsync(queryResult.ConvertTo<NLedgerResponse>());
        }
    }
}