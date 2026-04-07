using MediatR;

namespace BuildingBlocks.CQRS; // Corregí el error de dedo "BuildingBocks" a "BuildingBlocks"

// 2. Definición del QueryHandler (El que procesa)
public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : notnull
{
}