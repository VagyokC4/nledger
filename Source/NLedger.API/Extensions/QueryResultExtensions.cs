using System;
using NLedger.API.Types;
using NLedger.API.Types.Balances;
using ServiceStack;

namespace NLedger.API.Extensions
{
    public static class QueryResultExtensions
    {
        /// <summary>
        ///     Attempt to Json deserialize as <see cref="QueryResult{T}" />
        /// </summary>
        /// <param name="queryResult"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static QueryResult<T> As<T>(this QueryResult<string> queryResult) where T : new()
        {
            var result = new QueryResult<T>().PopulateWith(queryResult);

            try
            {
                result.Result = queryResult.Result.FromJson<T>();
            }
            catch (Exception e)
            {
                result.Exception = e.ToString();
            }

            return result;
        }

        /// <summary>
        ///     Deserialize from <see cref="QueryResult" /> to <see cref="BalanceReport" />
        /// </summary>
        /// <param name="queryResult"></param>
        /// <returns>BalanceReport</returns>
        public static BalanceReport AsBalanceReport(this QueryResult<string> queryResult)
        {
            return BalanceReport.FromLedgerResponse(queryResult.Result);
        }
    }
}