using AviatoDDD.Queries;
using MediatR;

namespace AviatoDDD.Handlers;

public interface IQueryHandler<Q, R> : IRequestHandler<Q, R>
    where Q : IQuery<R>
{
}