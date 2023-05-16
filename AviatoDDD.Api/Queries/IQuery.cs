using MediatR;

namespace AviatoDDD.Queries;

public interface IQuery<T>: IRequest<T>
{
    
}