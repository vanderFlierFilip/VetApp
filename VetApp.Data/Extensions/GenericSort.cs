using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;

namespace VDMJasminka.Data.Extensions
{
    public static class GenericSort
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> values, string parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters))
            {
                return values;
            }

            var queryString = OrderQueryBuilder.CreateOrderQuery<T>(parameters);

            return values.OrderBy(queryString);
        }
    }
}