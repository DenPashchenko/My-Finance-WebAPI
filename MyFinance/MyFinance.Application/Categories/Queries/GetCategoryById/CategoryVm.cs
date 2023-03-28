using AutoMapper;
using MyFinance.Application.Common.Mappings;
using MyFinance.Domain;

namespace MyFinance.Application.Categories.Queries.GetCategoryById
{
    public class CategoryVm : IMapWith<Category>
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryVm>();
        }
    }
}
