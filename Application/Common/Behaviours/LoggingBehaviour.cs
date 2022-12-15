using MediatR;

namespace GLPIService.Application.Common.Behaviours {

    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse> {

        private readonly ILogger _logger;

        public LoggingBehaviour(ILogger<TRequest> logger) {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) {
            var requestName = typeof(TRequest).Name;

            _logger.LogInformation("GLPIService Request: {Name} {@Request}", requestName, request);

            var response = await next();

            _logger.LogDebug("GLPIService Response: {Name} {@Request} {@Response}", requestName, request, response);

            return response;
        }

    }
}
