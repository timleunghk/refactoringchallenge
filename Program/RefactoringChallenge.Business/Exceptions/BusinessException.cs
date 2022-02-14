using System;

namespace RefactoringChallenge.Business.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string businessMessage)
            : base(businessMessage)
        {

        }
    }
}
