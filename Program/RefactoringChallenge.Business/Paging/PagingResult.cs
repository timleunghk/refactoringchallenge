using System;
using System.Collections.Generic;
using System.Linq;

namespace RefactoringChallenge.Business.Paging
{
   public class PagingResult<T>
    {
        public IList<T> PageData { get; set; }
        public int Total { get; set; }
        public int PageSize { get; set; }

        public PagingResult<TDest> MapTo<TDest>(Func<T, TDest> mapper)
        {
            return new PagingResult<TDest>()
            {
                PageData = PageData.Select(d => mapper(d)).ToList(),
                PageSize = PageSize,
                Total = Total
            };
        }


    }
}
