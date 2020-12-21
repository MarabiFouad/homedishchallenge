using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HomeDishTest.Models;
using HomeDishTest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeDishTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingBasketController : ControllerBase
    {
        private readonly IBasketCalculationService _basketCalculationService;

        public ShoppingBasketController(
            IBasketCalculationService basketCalculationService)
        {
            _basketCalculationService = basketCalculationService;
        }


        [HttpPost("/")]
        [HttpPost("/total", Name = nameof(ShoppingBasketController.GetMinimumGrandTotal))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetMinimumGrandTotal([FromBody] Basket basket, CancellationToken cancellationToken)
        {
            if (basket == null) return BadRequest(new ApiError("Basket", "There is no basket"));

            return Ok(_basketCalculationService.MinimumGrandTotal(basket));

        }
    }
}
