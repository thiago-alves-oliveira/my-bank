using MediatR;

namespace IOBBank.Core.Mediator;

public interface IMediatorInput<out TMediatorResult>
    : IRequest<TMediatorResult> where TMediatorResult : IMediatorResult
{

}