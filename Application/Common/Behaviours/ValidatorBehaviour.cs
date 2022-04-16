using Domain.Exceptions;

using Microsoft.Extensions.Logging;

//using ValidationException = Application.Common.Exceptions.ValidationException;

using ValidationException = FluentValidation.ValidationException;

namespace Application.Common.Behaviours
{
    public class ValidatorBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ValidatorBehaviour<TRequest, TResponse>> _logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidatorBehaviour(ILogger<ValidatorBehaviour<TRequest, TResponse>> logger,
            IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var typeName = typeof(TRequest).Name;

                _logger.LogInformation("----- Validating command {CommandType}", typeName);

                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators
                                                                .Select(v =>
                                                                    v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                                    .SelectMany(r => r.Errors)
                                    .Where(f => f != null)
                                    .ToList();

                if (failures.Any())
                {
                    _logger.LogWarning("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}", typeName, request, failures);

                    throw new KnwldgDomainException(
                        $"Command Validation Errors for type {typeof(TRequest).Name}",
                        new ValidationException("Validation exception", failures));

                    //throw new ValidationException(failures);
                }
            }
            return await next();
        }
    }
}