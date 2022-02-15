using Catalog.API.DTOs;
using Catalog.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductService service, ILogger<CatalogController> logger)
        {
            this._service = service ?? throw new ArgumentNullException(nameof(service));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await this._service.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProductDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductById(string id)
        {
            var product = await this._service.GetProductById(id);
            if (product == null)
            {
                this._logger.LogError($"Product with id: {id} not found");
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("[action]/{name}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProductDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductDTO>> GetProductByName(string name)
        {
            var product = await this._service.GetProductByName(name);
            if (product == null)
            {
                this._logger.LogError($"Product with id: {name} not found");
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("[action]/{categoryId:length(24)}")]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByCategory(string categoryId)
        {
            var products = await this._service.GetProductsByCategory(categoryId);
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductDTO>> CreateProduct(ProductUpsertDTO product)
        {
            var createdProduct = await this._service.CreateProduct(product);
            return CreatedAtAction("GetProductById", new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(typeof(ProductDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductDTO>> UpdateProduct(string id, ProductUpsertDTO product)
        {
            var updatedProduct = await this._service.UpdateProduct(id, product);
            return Ok(updatedProduct);
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductDTO>> DeleteProduct(string id)
        {
            var product = await this._service.GetProductById(id);
            if (product == null)
            {
                this._logger.LogError($"Product with id: {id} Not found");
                return NotFound();
            }
            await this._service.DeleteProduct(id);
            return Ok();
        }

    }
}
