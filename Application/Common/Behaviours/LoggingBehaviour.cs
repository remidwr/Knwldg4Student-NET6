﻿using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var typeName = typeof(TRequest).Name;

            _logger.LogInformation("----- Handling command {CommandName} ({@Command})", typeName, request);
            var response = await next();
            _logger.LogInformation("----- Command {CommandName} handled - response: {@Response}", typeName, response);

            return response;
        }
    }
}