using Microsoft.EntityFrameworkCore;
using MyFinance.Application.Categories.Commands.UpdateCategory;
using MyFinance.Application.Common.Exceptions;
using MyFinance.Tests.Common;

namespace MyFinance.Tests.Categories.CommandTests
{
    public class UpdateCategoryCommandHandlerTests : TestFixtureBase
    {
        [Fact]
        public async Task UpdateCategoryCommandHandler_ValidData_Success()
        {
            var handler = new UpdateCategoryCommandHandler(_context);
            var categoryName = "Updated name";
            int idToUpdate = 1;

            await handler.Handle(
                new UpdateCategoryCommand
                {
                    CategoryId = idToUpdate,
                    Name = categoryName,
                },
                CancellationToken.None);

            Assert.NotNull(await _context.Categories.SingleOrDefaultAsync(category => 
                category.Name == categoryName &&
                category.CategoryId == idToUpdate));
        }

        [Fact]
        public async Task UpdateCategoryCommandHandler_InvalidId_NotFoundException()
        {
            var handler = new UpdateCategoryCommandHandler(_context);
            var categoryName = "Updated name";
            int invalidId = 0;

            await Assert.ThrowsAsync<NotFoundException>(async () =>
               await handler.Handle(
                   new UpdateCategoryCommand
                   {
                       CategoryId = invalidId,
                       Name = categoryName
                   },
                   CancellationToken.None));
        }
    }
}
