using MediatR;
using Microsoft.Extensions.Logging;
using Exceptions = System.Exception;


namespace CleanArchitecture.Application.Behaviours
{
    public class UnhandledExeptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>  where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExeptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exceptions ex)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogError(ex, "Aplication Request: Sucedio una excepcion para el request {Name} {@Request}", requestName, request);
                throw;
            }
        }
    }
}
