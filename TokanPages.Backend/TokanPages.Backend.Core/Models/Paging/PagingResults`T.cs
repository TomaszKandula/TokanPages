using System.Collections.Generic;

namespace TokanPages.Backend.Core.Models.Paging
{
    public class PagingResults<T> where T : class
    {
        public PagingInfo PagingInfo { get; set; }

        public int TotalSize { get; set; }
        
        public ICollection<T> Results { get; set; }
    }
}