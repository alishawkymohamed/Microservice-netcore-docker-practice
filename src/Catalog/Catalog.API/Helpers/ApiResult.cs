using System.Collections.Generic;

namespace Catalog.API.Helpers
{
    public class ApiResult
    {
        public bool IsSucess { get; set; }
        public object Data { get; set; }
        public List<Error> Errors { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
