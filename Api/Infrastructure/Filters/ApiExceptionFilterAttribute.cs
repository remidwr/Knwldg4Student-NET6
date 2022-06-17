﻿namespace Api.Infrastructure.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ApiExceptionFilterAttribute> _logger;
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilterAttribute(IWebHostEnvironment env, ILogger<ApiExceptionFilterAttribute> logger)
        {
            _env = env;
            _logger = logger;
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(KnwldgDomainException), HandleKnwldgDomainException },
                { typeof(ArgumentException), HandleArgumentException },
                { typeof(ArgumentNullException), HandleArgumentNullException },
                { typeof(ArgumentOutOfRangeException), HandleArgumentOutOfRangeException },
            };
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            HandleException(context);
            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            var type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var json = new JsonErrorResponse
            {
                Messages = new[] { "An error occur.Try it again." }
            };

            if (_env.IsDevelopment())
            {
                json.DeveloperMessage = context.Exception;
            }

            context.Result = new InternalServerErrorObjectResult(json);
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationException;

            var problemDetails = new ValidationProblemDetails(exception.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Instance = context.HttpContext.Request.Path,
            };

            problemDetails.Errors.Add("DomainValidations", new string[] { context.Exception.Message.ToString() });

            context.Result = new BadRequestObjectResult(problemDetails);

            context.ExceptionHandled = true;
        }

        private static void HandleInvalidModelStateException(ExceptionContext context)
        {
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Instance = context.HttpContext.Request.Path,
            };

            context.Result = new BadRequestObjectResult(problemDetails);

            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException;

            var problemDetails = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specified resource was not found.",
                Detail = exception?.Message,
                Instance = context.HttpContext.Request.Path,
            };

            context.Result = new NotFoundObjectResult(problemDetails);

            context.ExceptionHandled = true;
        }

        private void HandleKnwldgDomainException(ExceptionContext context)
        {
            var exception = context.Exception as KnwldgDomainException;

            var problemDetails = new ValidationProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "An error occurred while processing your request.",
                Status = StatusCodes.Status400BadRequest,
                Detail = exception?.Message,
                Instance = context.HttpContext.Request.Path,
            };

            context.Result = new BadRequestObjectResult(problemDetails);

            context.ExceptionHandled = true;
        }

        private void HandleArgumentException(ExceptionContext context)
        {
            var exception = context.Exception as ArgumentException;

            var problemDetails = new ValidationProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "The argument is incorrect.",
                Status = StatusCodes.Status400BadRequest,
                Detail = exception?.Message,
                Instance = context.HttpContext.Request.Path,
            };

            context.Result = new BadRequestObjectResult(problemDetails);

            context.ExceptionHandled = true;
        }

        private void HandleArgumentNullException(ExceptionContext context)
        {
            var exception = context.Exception as ArgumentNullException;

            var problemDetails = new ValidationProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "The argument is null.",
                Status = StatusCodes.Status400BadRequest,
                Detail = exception?.Message,
                Instance = context.HttpContext.Request.Path,
            };

            context.Result = new BadRequestObjectResult(problemDetails);

            context.ExceptionHandled = true;
        }

        private void HandleArgumentOutOfRangeException(ExceptionContext context)
        {
            var exception = context.Exception as ArgumentOutOfRangeException;

            var problemDetails = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "The argument exceeds the possible values.",
                Detail = exception?.Message,
                Instance = context.HttpContext.Request.Path,
            };

            context.Result = new BadRequestObjectResult(problemDetails);

            context.ExceptionHandled = true;
        }

        private class JsonErrorResponse
        {
            public string[] Messages { get; set; }

            public object DeveloperMessage { get; set; }
        }
    }
}