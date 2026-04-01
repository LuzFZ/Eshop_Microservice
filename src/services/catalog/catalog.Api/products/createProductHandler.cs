using catalog.Api.Models;
using MediatR;

namespace catalog.Api.products;

// Command que MediatR puede enrutar
public record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : IRequest<CreateProductResult>;

// Resultado
public record CreateProductResult(Guid Id);

// Handler que MediatR puede encontrar
internal class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        return Task.FromResult(new CreateProductResult(Guid.NewGuid()));
    }
}