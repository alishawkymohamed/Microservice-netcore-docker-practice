using System;

namespace Catalog.API.Helpers
{
    public class BusinessException : Exception
    {
        public BusinessException(string code, string details)
        {
            this.Code = code;
            this.Details = details;
        }
        public string Code { get; private set; }
        public string Details { get;private set; }
    }
}
