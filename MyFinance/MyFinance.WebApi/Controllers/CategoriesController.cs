using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Application.Categories.Commands.CreateCategory;
using MyFinance.Application.Categories.Commands.DeleteCategory;
using MyFinance.Application.Categories.Commands.UpdateCategory;
using MyFinance.Application.Categories.Queries.GetCategoryById;
using MyFinance.Application.Categories.Queries.GetCategoryList;
using MyFinanceWebApi.Controllers.Abstractions;
using MyFinanceWebApi.ModelDtos.CategoryDto;

namespace MyFinanceWebApi.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : BaseController
    {
        private readonly IMapper _mapper;

        public CategoriesController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Gets the list of categories
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CategoryListVm>> GetAllAsync()
        {
            var query = new GetCategoryListQuery();
            var viewModel = await Mediator.Send(query);

            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the category by id
        /// </summary>
        [HttpGet("{id}")]
        [ActionName("GetByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryVm>> GetByIdAsync(int id)
        {
            var query = new GetCategoryByIdQuery
            {
                Id = id
            };
            var viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Creates the category
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryVm>> CreateAsync([FromBody] CreateCategoryDto category)
        {
            var command = _mapper.Map<CreateCategoryCommand>(category);
            var viewModel = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = viewModel.Id }, viewModel);
        }

        /// <summary>
        /// Updates the category by id
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateCategoryDto request)
        {
            var command = _mapper.Map<UpdateCategoryCommand>(request);
            command.Id = id;
            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Deletes the category by id
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var command = new DeleteCategoryCommand
            {
                Id = id
            };
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
