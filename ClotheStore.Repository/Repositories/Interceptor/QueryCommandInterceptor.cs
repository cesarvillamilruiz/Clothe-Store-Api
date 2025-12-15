using ClotheStore.Repository.Helper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClotheStore.Repository.Repositories.Interceptor
{
    public class QueryCommandInterceptor : DbCommandInterceptor
    {
        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            return base.ReaderExecuting(command, eventData, result);
        }

        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result,
            CancellationToken cancellationToken = default)
        {
            if (command.CommandText.Contains("[VendorTest]") && command.CommandText.ToLower().Contains("update"))
            {
                var queries = command.CommandText.Split(";");
                foreach (var query in queries)
                {
                    string operation = "", entityName = "";
                    QueryHelper.GetValuesWithRegex(query, out operation, out entityName);
                    if (string.IsNullOrEmpty(entityName)) continue;

                    EntitiesHelper entities = new EntitiesHelper();
                    var entity = entities.Entities.FirstOrDefault(x => x.EntityName == entityName);
                    if (entity != null && !string.IsNullOrEmpty(entity.EntityName))
                    {
                        queries[Array.IndexOf(queries, query)] = QueryHelper.GetFinalQuery(query, operation, entity);
                    }
                }
                command.CommandText = string.Join(";", queries);
            }

            return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
        }


        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            return base.ReaderExecuted(command, eventData, result);
        }

        public override ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = default)
        {
            return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
        }
    }
}
