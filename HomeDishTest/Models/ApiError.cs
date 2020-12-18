using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeDishTest.Models
{
    public class ApiError
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public ApiError(string name,string message)
        {
            Name = name;
            Message = message;
        }
    }
}
