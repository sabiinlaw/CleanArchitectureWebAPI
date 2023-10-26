using AspNetRestApiContainer.Application.Parameters.Commands;
using AspNetRestApiContainer.Application.Parameters.Queries;
using AspNetRestApiContainer.Infrastructure.Shared.Services;
using AspNetRestApiContainer.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AspNetRestApiContainer.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// GET: api/controller
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetProductQuery filter)
        {
            var productsResponse = await _productService.GetProducts(filter);
            return Ok(productsResponse);
        }

        /// <summary>
        /// GET api/controller/
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var productResponse = await _productService.GetById(id);
            return Ok(productResponse);
        }

        /// <summary>
        /// POST api/controller
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] ProductCreate command)
        {
            var productResponse = await _productService.CreateAsync(command);
            return Ok(productResponse);
        }

        /// <summary>
        /// PUT api/controller/
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] ProductUpdate command)
        {
            var productResponse = await _productService.UpdateAsync(id, command);
            return Ok(productResponse);
        }

        /// <summary>
        /// DELETE api/controller/
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [Authorize(Policy = AuthorizationConsts.ManagerPolicy)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var productResponse = await _productService.DeleteAsync(id);
            return Ok(productResponse);
        }
    }
}