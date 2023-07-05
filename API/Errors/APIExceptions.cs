using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class APIExceptions : APIResponse
    {
        public APIExceptions(int statusCode, string message = null,String details=null) : base(statusCode, message)
        {
            Details=details;
        }
        public string Details { get; set; }
    }
}