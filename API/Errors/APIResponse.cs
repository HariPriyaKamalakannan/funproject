using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class APIResponse
    {    public int StatusCode{get;set;}
        public string Message { get; set; }
        public APIResponse(int statusCode, string message=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageforStatusCode(statusCode);
        }

        private string GetDefaultMessageforStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400=>"A bad request",
                401=>"Authorized,you are not",
                404=>"Resource was not found",
                500=>"Server Error",
                _=>null
            };
        }
    }
}