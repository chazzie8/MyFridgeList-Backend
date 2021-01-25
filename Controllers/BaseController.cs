using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MyFridgeListWebapi.Core.Models;
using MyFridgeListWebapi.Extensions;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Controllers
{
    public abstract class BaseController<T> : ControllerBase
        where T : BaseController<T>
    {
        private IMediator _mediator;

        protected BaseController(IHttpContextAccessor contextAccessor)
        {
            UserId = contextAccessor.HttpContext.User.GetUserId();
            ContextAccessor = contextAccessor;
        }

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected Guid UserId { get; }
        protected IHttpContextAccessor ContextAccessor { get; }

        protected Response<TResponse> Success<TResponse>(TResponse data)
        {
            return BuildResponse(data, null);
        }

        protected Response<TResponse> Error<TResponse>(Error error)
        {
            HttpContext.Response.StatusCode = error.Code;

            return BuildResponse(default(TResponse), error);
        }

        protected Response<TResponse> ValidationError<TResponse>(IEnumerable<ValidationError> validationErrors)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            return BuildResponse(default(TResponse), null, validationErrors);
        }

        protected Response<TResponse> BuildResponse<TResponse>(TResponse data, Error error, IEnumerable<ValidationError> validationErrors = null)
        {
            return new Response<TResponse>
            {
                Success = error == null && (validationErrors == null || !validationErrors.Any()),
                Error = error,
                Data = data,
                ValidationErrors = validationErrors ?? new List<ValidationError>()
            };
        }

        protected Response<TResponse> NotAllowed<TResponse>(string message = null)
        {
            return Error<TResponse>(new Error
            {
                Code = StatusCodes.Status405MethodNotAllowed,
                Message = message ?? Resources.ErrorHttp405
            });
        }

        protected Response<TResponse> NotFound<TResponse>(string message = null)
        {
            return Error<TResponse>(new Error
            {
                Code = StatusCodes.Status404NotFound,
                Message = message ?? Resources.ErrorHttp404
            });
        }

        protected Response<TResponse> Forbidden<TResponse>(string message = null)
        {
            return Error<TResponse>(new Error
            {
                Code = StatusCodes.Status403Forbidden,
                Message = message ?? Resources.ErrorHttp403
            });
        }

        protected Response<TResponse> Unauthorized<TResponse>(string message = null)
        {
            return Error<TResponse>(new Error
            {
                Code = StatusCodes.Status401Unauthorized,
                Message = message ?? Resources.ErrorHttp401
            });
        }

        protected Response<TResponse> BadRequest<TResponse>(string message = null)
        {
            return Error<TResponse>(new Error
            {
                Code = StatusCodes.Status400BadRequest,
                Message = message ?? Resources.ErrorHttp400
            });
        }

        protected Response<TResponse> Unknown<TResponse>(string message = null)
        {
            return Error<TResponse>(new Error
            {
                Code = StatusCodes.Status500InternalServerError,
                Message = message ?? Resources.ErrorHttp500
            });
        }

        protected Response<TResponse> BadRequest<TResponse>(IEnumerable<ValidationError> validationErrors)
        {
            return BuildResponse(
                default(TResponse),
                new Error
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = Resources.ErrorHttp400
                },
                validationErrors);
        }
    }
}
