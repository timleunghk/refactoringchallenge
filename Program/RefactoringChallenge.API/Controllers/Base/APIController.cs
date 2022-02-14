using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RefactoringChallenge.API.Controllers.Base
{
    public class APIController : Controller
    {
        protected DataResult<T> Make<T>(T data)
        {
            return new DataResult<T>(data);
        }

        protected DataResult<T> Make<T>(T data, string error)
        {
            var result = new DataResult<T>(data);
            result.AddError(error);
            return result;
        }

        #region Uniform API response structure
        protected class DataResult<T>
        {
            public T Data { get; private set; }
            public List<string> Errors { get; private set; } = new List<string>();
            public bool IsSuccess => Errors.Count == 0;

            public DataResult(T data)
            {
                Data = data;
                Errors = Errors;
            }

            public DataResult<T> AddError(string error)
            {
                Errors.Add(error);
                return this;
            }
        }
        #endregion
    }
}
