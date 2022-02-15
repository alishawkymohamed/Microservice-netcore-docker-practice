using Catalog.API.DTOs;
using Catalog.API.Helpers;
using Catalog.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            this._categoryService = categoryService;
            this._logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await this._categoryService.GetCategories();
            return Ok(new ApiResult
            {
                IsSucess = true,
                Data = categories
            });
        }

        [HttpGet("{id:length(24)}", Name = "GetCategory")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CategoryDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoryById(string id)
        {
            var category = await this._categoryService.GetCategoryById(id);
            if (category == null)
            {
                this._logger.LogError($"Product with id: {id} not found");
                return Ok(new ApiResult
                {
                    IsSucess = false,
                    Data = null,
                    Errors = new List<Error> { new Error
                    {
                        Code = ((int)HttpStatusCode.NotFound).ToString(),
                        Message = $"Product with id: {id} not found"
                    } }
                });
            }
            return Ok(new ApiResult
            {
                IsSucess = true,
                Data = category
            });
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateProduct([FromBody] CategoryUpsertDTO category)
        {
            await this._categoryService.CreateCategory(category);
            return Ok(new ApiResult
            {
                IsSucess = true
            });
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CategoryDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CategoryDTO>> UpdateCategory(string id, [FromBody] CategoryUpsertDTO category)
        {
            var cat = await this._categoryService.GetCategoryById(id);
            if (cat == null)
            {
                return Ok(new ApiResult
                {
                    IsSucess = false,
                    Data = null,
                    Errors = new List<Error> { new Error
                    {
                        Code = ((int)HttpStatusCode.NotFound).ToString(),
                        Message = $"Product with id: {id} not found"
                    } }
                });
            }
            await this._categoryService.UpdateCategory(id, category);
            var updated = await this._categoryService.GetCategoryById(id);
            return Ok(new ApiResult
            {
                IsSucess = true,
                Data = updated
            });
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteCategory(string id)
        {
            var cat = await this._categoryService.GetCategoryById(id);
            if (cat == null)
            {
                return Ok(new ApiResult
                {
                    IsSucess = false,
                    Data = null,
                    Errors = new List<Error> { new Error
                    {
                        Code = ((int)HttpStatusCode.NotFound).ToString(),
                        Message = $"Product with id: {id} not found"
                    } }
                });
            }
            try
            {
                await this._categoryService.DeleteCategory(id);
            }
            catch (BusinessException ex)
            {
                this._logger.LogError(ex, ex.Details);
                return BadRequest(new ApiResult
                {
                    IsSucess = false,
                    Data = null,
                    Errors = new List<Error> { new Error
                    {
                        Code = ex.Code,
                        Message = ex.Details,
                    } }
                });
            }
            await this._categoryService.DeleteCategory(id);
            return Ok(new ApiResult
            {
                IsSucess = true
            });
        }
    }
}
