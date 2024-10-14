using MediatR;

namespace IOBBank.Core.Mediator.Events;

public interface IEventHandler<in TEventInput> : INotificationHandler<TEventInput> where TEventInput : EventInput
{
}