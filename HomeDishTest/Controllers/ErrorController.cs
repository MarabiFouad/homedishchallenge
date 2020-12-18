using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeDishTest.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeDishTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {

            //check type of exception if is custome type return other wel formated type.
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (context.Error is ApiException)
            {
                return Problem(detail: context.Error?.Message, title: "An error occurded");
            }
            return Problem();

        }
    }
}
