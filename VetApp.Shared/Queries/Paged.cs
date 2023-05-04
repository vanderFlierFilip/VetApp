using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace VDMJasminka.Shared.Queries
{
    public class Paged<T>
    {
        /// <summary>Information about the requested page.</summary>
        public PageInfo Paging { get; set; }

        /// <summary>The list of items for the given page.</summary>
        public T[] Items { get; set; }
    }
}
