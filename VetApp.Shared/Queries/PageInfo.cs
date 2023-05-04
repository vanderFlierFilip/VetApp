using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace VDMJasminka.Shared.Queries
{
    /// <summary>Object containing information about paging.</summary>
    public class PageInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageInfo"/> class.
        /// </summary>
        public PageInfo() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="PageInfo"/> class with given values for 
        /// page index, page size, and items count. 
        /// </summary>
        /// <param name="pageIndex">The page index.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="itemsCount">The total count of items.</param>
        /// <remarks>
        /// <para>
        /// The <c>PageInfo</c> class is used to store information about the current page,
        /// the total number of pages and the total number of items in the result set.
        /// </para>
        /// </remarks>
        public PageInfo(int pageIndex, int pageSize, int itemsCount)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = IsSinglePage() ? 1 : (int)Math.Ceiling(itemsCount / (double)PageSize);
            HasNextPage = PageIndex < (int)Math.Ceiling(itemsCount / (double)PageSize);
        }
        /// <summary>Number of total pages.</summary>
        public int TotalPages { get; private set; }
        /// <summary>If the current page has a nest page.</summary>
        public bool HasNextPage { get; private set; }
        /// <summary>If the current page has a previous page.</summary>
        public bool HasPreviousPage { get; private set; }

        /// <summary>The 0-based page index.</summary>
        public int PageIndex { get; set; }

        /// <summary>The number of items in a page.</summary>
        public int PageSize { get; set; } = 20;

        /// <summary>Gets the value indicating whether the page info represents the request for a single page.</summary>
        public bool IsSinglePage() => this.PageIndex == 0 && this.PageSize == -1;

       
    }
}
