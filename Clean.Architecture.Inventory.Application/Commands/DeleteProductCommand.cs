using MediatR;

namespace Clean.Architecture.Inventory.Application.Commands
{
    public class DeleteProductCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
