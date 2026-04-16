using ASNAOrders.Web.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace ASNAOrders.Web.LogicServices
{
    /// <summary>
    /// Handler to implement correct 500 Internal Server Error error handling.
    /// </summary>
    public class CustomExceptionHandler : IExceptionHandler
    {
        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            if (context.Exception is KeyNotFoundException && context.ExceptionContext.Response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                context.Result = (IHttpActionResult)new JsonResult
                (
                    new List<ErrorListV1Inner>()
                    {
                        new ErrorListV1Inner()
                        {
                            Code = 404,
                            Description = context.Exception.Message
                        }
                    }, 
                    new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                    }

                );
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken cancellationToken)
        {

            if (context.Response.StatusCode == StatusCodes.Status500InternalServerError)
            {
                if (ex is BadHttpRequestException)
                {
                    return false;
                }

                if (ex is KeyNotFoundException)
                {
                    var response = new List<ErrorListV1Inner>()
                    {
                        new ErrorListV1Inner()
                        {
                            Code = 404,
                            Description = ex.Message
                        }
                    };

                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsJsonAsync
                    (
                        response,
                        new JsonSerializerOptions()
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                        },
                        cancellationToken
                    );
                }

                var responseAlt = new List<ErrorListV1Inner>()
                    {
                        new ErrorListV1Inner()
                        {
                            Code = 500,
                            Description = ex.Message
                        }
                    };

                await context.Response.WriteAsJsonAsync
                (
                    responseAlt,
                    new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                    },
                    cancellationToken
                );
            }

            return true;
        } 
    }
}
