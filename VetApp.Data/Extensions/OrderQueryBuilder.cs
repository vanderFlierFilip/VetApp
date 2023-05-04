using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace VDMJasminka.Data.Extensions
{
    public static class OrderQueryBuilder
    {
        public static string? ToCamelCase(this string? str) => str is null
      ? null
      : new DefaultContractResolver() { NamingStrategy = new CamelCaseNamingStrategy() }.GetResolvedPropertyName(str);

        public static string? ToSnakeCase(this string? str) => str is null
            ? null
            : new DefaultContractResolver() { NamingStrategy = new SnakeCaseNamingStrategy() }.GetResolvedPropertyName(str);

        public static string CreateOrderQuery<T>(string orderByQueryString)
        {
            orderByQueryString ??= string.Empty;

            var orderParams = orderByQueryString?.Trim()?.Split(',');
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "desc" : "asc";

                orderQueryBuilder.Append($"{objectProperty.Name.ToSnakeCase()} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            return string.IsNullOrEmpty(orderQuery) ? "name asc" : orderQuery;
        }
    }
}