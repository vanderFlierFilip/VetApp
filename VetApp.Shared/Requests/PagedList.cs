using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VDMJasminka.Shared.Requests
{
    public class PagedList<T>
    {
        public MetaData MetaData { get; set; }
        public List<T> Items { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            Items = items;

        }

        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.ToList();
               

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
