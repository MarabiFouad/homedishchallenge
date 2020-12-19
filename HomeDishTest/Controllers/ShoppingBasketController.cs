﻿using System;
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
        private readonly IBasketService _homeDishApiService;

        public ShoppingBasketController(
            IBasketCalculationService basketCalculationService,
            IBasketService homeDishApiService,
            ILogger<ShoppingBasketController> log)
        {
            _basketCalculationService = basketCalculationService;
            _homeDishApiService = homeDishApiService;
            Log = log;
        }

        public ILogger<ShoppingBasketController> Log { get; }

        [HttpGet("/")]
        [HttpGet("/total", Name = nameof(ShoppingBasketController.GetMinimumGrandTotal))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMinimumGrandTotal(CancellationToken cancellationToken)
        {
            var basket = await _homeDishApiService.GetBasketAsync(cancellationToken);
            if (basket == null) return NotFound(new ApiError("Basket", "There is no basket"));

            return Ok(_basketCalculationService.MinimumGrandTotal(basket));

        }
    }
}
