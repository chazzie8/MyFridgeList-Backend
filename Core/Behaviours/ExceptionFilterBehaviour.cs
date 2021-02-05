using System;
using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyFridgeListWebapi.Core.Exceptions;
using MyFridgeListWebapi.Core.Models;
using MyFridgeListWebapi.Properties;
namespace MyFridgeListWebapi.Core.Behaviours
{
    public sealed class ExceptionFilterBehaviour : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException)
            {
                HandleValidationException(context, context.Exception as ValidationException);
                return;
            }
            if (context.Exception is BadRequestException)
            {
                HandleBadRequestException(context, context.Exception as BadRequestException);
                return;
            }
            if (context.Exception is UnauthorizedException)
            {
                HandleUnauthorizedException(context, context.Exception as UnauthorizedException);
                return;
            }
            if (context.Exception is NotFoundException)
            {
                HandleNotFoundException(context, context.Exception as NotFoundException);
                return;
            }
            HandleException(context, context.Exception);
        }
        private void HandleValidationException(ExceptionContext context, ValidationException exception)
        {
            context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new JsonResult(new Response<object>
            {
                Success = false,
                ValidationErrors = exception.ValidationErrors,
                Error = new Error
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = !string.IsNullOrEmpty(exception.Message) ? exception.Message : Resources.ErrorHttp400
                },
            });
        }
        private void HandleBadRequestException(ExceptionContext context, BadRequestException exception)
        {
            context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new JsonResult(new Response<object>
            {
                Success = false,
                Error = new Error
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = !string.IsNullOrEmpty(exception.Message) ? exception.Message : Resources.ErrorHttp400
                },
                ValidationErrors = exception.ValidationErrors
            });
        }
        private void HandleUnauthorizedException(ExceptionContext context, UnauthorizedException exception)
        {
            context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
            context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Result = new JsonResult(new Response<object>
            {
                Success = false,
                Error = new Error
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Message = !string.IsNullOrEmpty(exception.Message) ? exception.Message : Resources.ErrorHttp401
                },
                ValidationErrors = new List<ValidationError>()
            });
        }
        private void HandleNotFoundException(ExceptionContext context, NotFoundException exception)
        {
            context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
            context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Result = new JsonResult(new Response<object>
            {
                Success = false,
                Error = new Error
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = !string.IsNullOrEmpty(exception.Message) ? exception.Message : Resources.ErrorHttp404
                },
                ValidationErrors = new List<ValidationError>()
            });
        }
        private void HandleException(ExceptionContext context, Exception exception)
        {
            context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new JsonResult(new Response<object>
            {
                Success = false,
                Error = new Error
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "An unexpected error occured. Please try again later."
                },
                ValidationErrors = new List<ValidationError>()
            });
        }
    }
}