using System.Collections.Generic;

namespace razor.Components.Models
{
    public class PagedResult<T>
    {
        public PagedResult()
        {
            count = 0;
            Result = new List<T>();
        }

        public long count;
        public List<T> Result { get; set; }
    }
}
