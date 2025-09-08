using Gridify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Extensions
{
    public static class GridifyExtensions
    {

        public static CustomResponse<IEnumerable<T>> ToCustomResponse<T>(this Paging<T> result, GridifyQuery query)
            => result.ToCustomResponse(query.Page, query.PageSize);
        public static CustomResponse<IEnumerable<T>> ToCustomResponse<T>(this Paging<T> result, int Page, int Size) 
        {

            if (result.Count == 0)
                return CustomResponse<IEnumerable<T>>.ErrorResponse<IEnumerable<T>>(404, "nothing found");

            var response = CustomResponse<IEnumerable<T>>.SuccessResponse(200, result.Data);
            response.Page = Page;
            response.PageSize = Size;
            response.Count = result.Count;

            return response;
        }

    }
}
