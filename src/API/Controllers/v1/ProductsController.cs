using FluentPOS.Application.Features.Products.Queries.GetProduct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseApiController<ProductsController>
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _mediator.Send(new GetProductQuery { Id = id, BypassCache = false });
            return Ok(product);
        }
    }
}
