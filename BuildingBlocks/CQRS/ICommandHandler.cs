using MediatR;

namespace BuildingBlocks.CQRS;

public interface ICommandHandler<in TCommand>
    :ICommandHandler<TCommand, unit>
    where TCommand : ICommand<unit>
{

}

public class unit
{
}

public interface ICommandHandler<in TCommand,TResponse>
    : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where  TResponse : notnull
{

}