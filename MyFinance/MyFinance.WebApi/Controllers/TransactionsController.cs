using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Application.Transactions.Commands.CreateTransaction;
using MyFinance.Application.Transactions.Commands.DeleteTransaction;
using MyFinance.Application.Transactions.Commands.UpdateTransaction;
using MyFinance.Application.Transactions.Queries.GetTransactionById;
using MyFinance.Application.Transactions.Queries.GetTransactionList;
using MyFinanceWebApi.Controllers.Abstractions;
using MyFinanceWebApi.ModelDtos.FinancialTransactionDto;

namespace MyFinanceWebApi.Controllers
{
    [Route("api/[controller]")]
    public class TransactionsController : BaseController
    {
        private readonly IMapper _mapper;

        public TransactionsController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Gets the list of transactions
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TransactionListVm>> GetAllAsync()
        {
            var query = new GetTransactionListQuery();
            var viewModel = await Mediator.Send(query);

            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the transaction by id
        /// </summary>
        [HttpGet("{id}")]
        [ActionName("GetByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionVm>> GetByIdAsync(int id)
        {
            var query = new GetTransactionByIdQuery
            {
                Id = id
            };
            var viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Creates the transaction
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionVm>> CreateAsync([FromBody] CreateTransactionDto transaction)
        {
            var command = _mapper.Map<CreateTransactionCommand>(transaction);
            var viewModel = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = viewModel.CategoryId }, viewModel);
        }

        /// <summary>
        /// Updates the transaction by id
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateTransactionDto request)
        {
            var command = _mapper.Map<UpdateTransactionCommand>(request);
            command.Id = id;
            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Deletes the transaction by id
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var command = new DeleteTransactionCommand
            {
                Id = id
            };
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
