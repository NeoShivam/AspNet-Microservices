using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {

        readonly IDiscountRepository _repository;
        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{productName}",Name ="GetDiscount")]
        [ProducesResponseType(typeof(Coupon),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var coupon = await _repository.GetDiscount(productName);
            return Ok(coupon);
        }

        [HttpPost("CreateDiscount")]
        public async Task<ActionResult<bool>> CreateDiscount(Coupon coupon)
        {
            await _repository.CreateDiscount(coupon);
            return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
        }

        [HttpPut("UpdateDiscount")]
        public async Task<ActionResult<bool>> UpdateDiscount(Coupon coupon)
        {
            await _repository.UpdateDiscount(coupon);
            return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
        }

        [HttpDelete("DeleteDiscount")]
        public async Task<ActionResult<string>> DeleteDiscount(string productName)
        {
            var response = await _repository.DeleteDiscount(productName);
            if (!response)
                return $"Error in  deleting {productName} coupon";
            return $"{productName} deleted successfully";

        }
    }
}
