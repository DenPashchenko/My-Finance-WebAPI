using MediatR;

namespace MyFinance.Application.Categories.Queries.GetCategoryList
{
    public class GetCategoryListQuery : IRequest<CategoryListVm>
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
