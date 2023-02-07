using MediatR;

namespace CleanArchitecture.Application.Behaviours
{
    public interface IValidationBehaviour<TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken);
    }
}