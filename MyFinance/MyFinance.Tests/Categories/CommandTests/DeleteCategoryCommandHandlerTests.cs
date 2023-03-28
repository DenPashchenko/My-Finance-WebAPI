using Microsoft.EntityFrameworkCore;
using MyFinance.Application.Categories.Commands.DeleteCategory;
using MyFinance.Application.Common.Exceptions;
using MyFinance.Tests.Common;

namespace MyFinance.Tests.Categories.CommandTests
{
    public class DeleteCategoryCommandHandlerTests : TestFixtureBase
    {
        [Fact]
        public async Task DeleteCategoryCommandHandler_ValidData_Success()
        {
            var handler = new DeleteCategoryCommandHandler(_context);
            int idToDelete = 3;

            await handler.Handle(
                new DeleteCategoryCommand
                {
                    CategoryId = idToDelete
                },
                CancellationToken.None);

            Assert.Null(await _context.Categories.SingleOrDefaultAsync(category =>
                category.CategoryId == idToDelete));
        }

        [Fact]
        public async Task DeleteCategoryCommandHandler_InvalidId_NotFoundException()
        {
            var handler = new DeleteCategoryCommandHandler(_context);
            int invalidId = 0;

            await Assert.ThrowsAsync<NotFoundException>(async () =>
               await handler.Handle(
                   new DeleteCategoryCommand
                   {
                       CategoryId = invalidId
                   },
                   CancellationToken.None));
        }
    }
}
