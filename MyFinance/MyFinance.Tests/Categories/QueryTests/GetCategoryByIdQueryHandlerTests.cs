using AutoMapper;
using MyFinance.Application.Categories.Queries.GetCategoryById;
using MyFinance.Application.Common.Exceptions;
using MyFinance.Persistence;
using MyFinance.Tests.Common;

namespace MyFinance.Tests.Categories.QueryTests
{
    [Collection("QueryCollection")]
    public class GetCategoryByIdQueryHandlerTests
    {
        private DataDbContext _context;
        private IMapper _mapper;

        public GetCategoryByIdQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.context;
            _mapper = fixture._mapper;
        }

        [Fact]
        public async Task GetCategoryByIdQueryHandler_ValidId_Success()
        {
            var handler = new GetCategoryByIdQueryHandler(_context, _mapper);
            int id = 1;

            var result = await handler.Handle(
                new GetCategoryByIdQuery
                {
                    CategoryId = id
                },
                CancellationToken.None);

            Assert.IsType<CategoryVm>(result);
            Assert.Equal(id, result.CategoryId);
            Assert.Equal("Category1", result.Name);
        }

        [Fact]
        public async Task GetCategoryByIdQueryHandler_InvalidId_NotFoundException()
        {
            var handler = new GetCategoryByIdQueryHandler(_context, _mapper);
            int invalidId = 0;

            await Assert.ThrowsAsync<NotFoundException>(async () =>
               await handler.Handle(
                   new GetCategoryByIdQuery
                   {
                       CategoryId = invalidId
                   },
                   CancellationToken.None));
        }
    }
}
