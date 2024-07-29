using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProductTask.Base.ErrorHandling;
using ProductTask.Shared.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace ProductTask.Utill
{


    public class HandleExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (NotFoundException e)
            {
                await HandleExceptionAsync(context, (int)HttpStatusCode.NotFound, e.Message);
            }

            catch (InternalServerException e)
            {
                await HandleExceptionAsync(context, (int)HttpStatusCode.InternalServerError, e.Message);
            }
            catch (BadRequestException e)
            {
                await HandleExceptionAsync(context, (int)HttpStatusCode.BadRequest, e.Message);
            }
            catch (ValidationException e)
            {
                await HandleExceptionAsync(context, (int)HttpStatusCode.BadRequest, e.Message);
            }
            catch (UnAuthorizedException e)
            {
                await HandleExceptionAsync(context, (int)HttpStatusCode.Unauthorized, e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("********************ERROR********************");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                await HandleExceptionAsync(context, (int)HttpStatusCode.InternalServerError, ErrorKey.InternalServerError.ToString());
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, int httpStatusCode, string message)
        {

            if (!context.Response.HasStarted)
            {
                var result = JsonConvert.SerializeObject(new
                {
                    //errorMessage = _errorResource[message].Value,
                    errorMessage = message,
                });
                context.Response.StatusCode = httpStatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }
        }
    }
}