using Clean.Architecture.Inventory.Domain.Entities;
using MediatR;

namespace Clean.Architecture.Inventory.Application.Queries
{
    public class GetCategoryProductByIdQuery : IRequest<CategoryProduct>
    {
        public int Id { get; set; }
    }
}
