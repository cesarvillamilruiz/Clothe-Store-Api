using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ClotheStore.Repository.Helper
{
    public class QueryHelper
    {
        public static void GetValuesWithRegex(string query, out string operation, out string entityName)
        {
            operation = "";
            entityName = "";
            Regex re = new Regex(@"((\s*\/\*.*\*\/\s*?)*\s+TABLE((\s*\/\*.*\*\/\s*?)*\s+IF(\s*\/\*.*\*\/\s*?)*\s+EXISTS)?|UPDATE|DELETE|INSERT(\s*\/\*.*\*\/\s*?)*\s+INTO)(\s*\/\*.*\*\/\s*?)*\s+([^\s\/*;]+)", RegexOptions.Multiline);
            foreach (Match match in re.Matches(query))
            {
                operation = match.Groups[1].Value;
                entityName = match.Groups[8].Value.Replace("[", "").Replace("]", "");
            }
        }

        public static string GetFinalQuery(string query, string action, EntitySettings entity)
        {
            if (action == "INSERT")
            {
                var result = "";
                var partsOfQuery = entity.SP_Update.Split('@');
                if (partsOfQuery != null && partsOfQuery.Length > 0)
                {
                    result = partsOfQuery[0];
                    result += GetPartForInsert(query, partsOfQuery);
                }

                return string.IsNullOrEmpty(result) ? query : $"EXEC {result}"; return $"EXEC {query} ";
            }
            else if (action == "UPDATE")
            {
                var result = "";
                var partsOfQuery = entity.SP_Update.Split('@');
                if (partsOfQuery != null && partsOfQuery.Length > 0)
                {
                    result = partsOfQuery[0];
                    // Get the SET part of the query
                    result += GetPartForUpdateOrDelete(query, " SET ", "OUTPUT ", partsOfQuery);
                    // Get the WHERE part of the query
                    result += GetPartForUpdateOrDelete(query, "WHERE ", "", partsOfQuery);
                    result = result.TrimEnd(',', ' ');
                }

                return string.IsNullOrEmpty(result) ? query : $"EXEC {result}";
            }
            else if (action == "DELETE")
            {
                var result = "";
                var partsOfQuery = entity.SP_Delete.Split('@');
                if (partsOfQuery != null && partsOfQuery.Length > 0)
                {
                    result = partsOfQuery[0];
                    // Get the WHERE part of the query
                    result += GetPartForUpdateOrDelete(query, "WHERE ", "", partsOfQuery);
                    result = result.TrimEnd(',', ' ');
                }

                return string.IsNullOrEmpty(result) ? query : $"EXEC {result}";
            }
            else
            {
                return query;
            }
        }

        private static string GetPartForInsert(string query, string[] partsOfQuery)
        {
            var result = "";
            var queryParams = query.Substring(query.IndexOf("(") + 1, query.IndexOf(")") - query.IndexOf("(") - 1);
            var querySplit = queryParams.Split(',');
            for (int i = 1; i < partsOfQuery.Length; i++)
            {
                partsOfQuery[i] = partsOfQuery[i].Replace(",", "").Trim();
                var parameter = querySplit.FirstOrDefault(x => x.Contains(partsOfQuery[i]));
                if (parameter != null)
                {
                    var value = parameter.Trim();
                    result += $"@{partsOfQuery[i]} = {value}, ";
                }
            }
            return result;
        }

        private static string GetPartForUpdateOrDelete(string query, string startWord, string lastWord, string[] partsOfQuery)
        {
            var result = "";
            int startIndex = query.IndexOf(startWord);
            int lastIndex = query.IndexOf(lastWord);
            var queryParams = string.IsNullOrEmpty(lastWord) ? query.Substring(startIndex) : query.Substring(startIndex, lastIndex - startIndex);
            var querySplit = queryParams.Split(',');
            for (int i = 1; i < partsOfQuery.Length; i++)
            {
                partsOfQuery[i] = partsOfQuery[i].Replace(",", "").Trim();
                var parameter = querySplit.FirstOrDefault(x => x.Contains(partsOfQuery[i]));
                if (parameter != null)
                {
                    var value = parameter.Split('=')[1].Trim();
                    result += $"@{partsOfQuery[i]} = {value}, ";
                }
            }
            return result;
        }

        public static string GetParameters(SqlParameter[] parameters)
        {
            var result = "";
            foreach (var parameter in parameters)
            {
                result += $"{parameter.ParameterName} = {parameter.ParameterName}, ";
            }
            return result.TrimEnd(',', ' ');
        }
    }
}
