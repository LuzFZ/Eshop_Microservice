using System.ComponentModel;
using BuildingBlocks.CQRS;
using MediatR;
using catalog.Api.Models;
namespace catalog.Api.products;

public record createPublicCommand(string Name, List<string> Category, string Description, string ImageFile, decimal price)
    : Icommand <createProductResult>;
public record createProductResult(Guid Id);
internal class createProductHandler : IcommandHandler<createPublicCommand, createProductResult>
{
    public async Task<createProductResult> Handle(createPublicCommand command, CancellationToken cancellationToken)
    {
        //busines logic to create  a product 
        //save to database 
        //return resultCreateProduct result

        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile

        };
        //TODO 
        //save to database 
        //return result

        return new createProductResult(Guid.NewGuid());

        
    }
}
