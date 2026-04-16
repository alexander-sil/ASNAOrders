using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using ASNAOrders.Web.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Org.BouncyCastle.Asn1.Ocsp;

namespace ASNAOrders.Web.LogicServices
{
    /// <summary>
    /// Handler to implement correct 400 Bad Request error handling.
    /// </summary>
    public class BadRequestErrorHandler : IExceptionHandler
    {
        public class ContentTypeResult : IHttpActionResult
        {
            private readonly string _content;
            private readonly string _contentType;
            private readonly HttpStatusCode _statusCode;

            public ContentTypeResult(string content, string contentType, HttpStatusCode statusCode = HttpStatusCode.OK)
            {
                _content = content;
                _contentType = contentType;
                _statusCode = statusCode;
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage(_statusCode);
                response.Content = new StringContent(_content);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(_contentType);

                return Task.FromResult(response);
            }
        }

        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            string ctype =
                (((context.ExceptionContext.Request.Method.Equals(System.Net.Http.HttpMethod.Post)) && context.ExceptionContext.Request.RequestUri.ToString().Contains("order"))
                || context.ExceptionContext.Request.RequestUri.ToString().Contains("nomenclature")
                || context.ExceptionContext.Request.RequestUri.ToString().Contains("security/oauth"))
                ? "application/vnd.eda.picker.errors.v1+json"
                : "application/json";

            if (context.Exception is BadHttpRequestException || context.ExceptionContext.Response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var response = new List<ErrorListV1Inner>()
                {
                    new ErrorListV1Inner()
                    {
                        Code = 400,
                        Description = context.Exception.Message
                    }
                };

                context.ExceptionContext.Response.StatusCode = (System.Net.HttpStatusCode)StatusCodes.Status400BadRequest;
                context.Result = new ContentTypeResult(JsonSerializer.Serialize(response, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower }), ctype, HttpStatusCode.BadRequest);

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

            string ctype =
                (((context.Request.Method == "POST") && context.Request.Path.ToString().Contains("order"))
                || context.Request.Path.ToString().Contains("nomenclature")
                || context.Request.Path.ToString().Contains("security/oauth"))
                ? "application/vnd.eda.picker.errors.v1+json"
                : "application/json";

            if (ex is BadHttpRequestException || context.Response.StatusCode == StatusCodes.Status400BadRequest)
            {
                var response = new List<ErrorListV1Inner>()
                {
                    new ErrorListV1Inner()
                    {
                        Code = 400,
                        Description = ex.Message
                    }
                };

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync
                (
                    response,
                    new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                    },
                    ctype,
                    cancellationToken
                );

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
