using AviatoDDD.Commands;
using MediatR;

namespace AviatoDDD.Handlers;

public interface ICommandHandler<T> : INotificationHandler<T>
    where T : ICommand, INotification
{
}