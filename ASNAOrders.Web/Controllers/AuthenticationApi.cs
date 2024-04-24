/*
 * API для интеграции сервиса Яндекс.Еда
 *
 * Описание API для работы с сервисом Яндекс.Еда по моделям \"Доставка Яндекс Едой\",  \"Доставка партнером\" и \"Самовывоз\". Все методы описанные ниже должны быть реализованы на стороне партнера в процессе интеграции. Т.е. сервис Яндекс.Еда выступает в роли клиента, а Вам необходимо реализовать серверную часть. Взаимодействие происходит на основании pull-модели, т.е. сервис Яндекс Еда как клиент запрашивает данные либо может создавать/обновлять данные если это необходимо.  # Authentication  <!- - ReDoc-Inject: <security-definitions> - ->
 *
 * The version of the OpenAPI document: 1.0-oas3
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using ASNAOrders.Web.Attributes;
using ASNAOrders.Web.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using ASNAOrders.Web.LogicServices;
using System.Reflection;
using System.Security.Principal;
using System.IO;
using ASNAOrders.Web.Filters;
using ASNAOrders.Web.ConfigServiceExtensions;

namespace ASNAOrders.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class AuthenticationApiController : ControllerBase
    {
        /// <summary>
        /// Аутентификация в системе
        /// </summary>
        /// <remarks>Авторизация для всех методов используется одинаковая. Партнер реализует в своей системе авторизацию формата OAuth2.0. Для получения токена партнер передает host, client_id и client_secret сотрудникам Яндекса. Полученные данные используются для получения токена в запросе  POST /security/oauth/token. Далее с полученным токеном Яндекс выполняет все запросы: получение товаров и остатков, работа с заказами и другие. Токен запрашивается перед каждым запросом. Данные значения заполняются Яндексом всегда одинаково grant_type &#x3D; client_credentials, scope &#x3D; read write и передаются в теле c application/x-www-form-urlencoded. Далее токен передается в заголовке каждого запроса как Authorization : Bearer token</remarks>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="grantType"></param>
        /// <param name="scope"></param>
        /// <response code="200">Успешная аутентификация</response>
        /// <response code="400">Ошибка в запросе, в ответе список ошибок</response>
        /// <response code="500">Внутренние ошибки сервера, в ответе список ошибок</response>
        [HttpPost]
        [Route("/security/oauth/token")]
        [Consumes("application/x-www-form-urlencoded")]
        [ValidateModelState]
        [SwaggerOperation("PartnerAuthPost")]
        [SwaggerResponse(statusCode: 200, type: typeof(AuthenticationResponse), description: "Успешная аутентификация", ContentTypes = ["application/json"])]
        [SwaggerResponse(statusCode: 400, type: typeof(List<ErrorListV1Inner>), description: "Ошибка в запросе, в ответе список ошибок", ContentTypes = ["application/vnd.eda.picker.errors.v1+json"])]
        [SwaggerResponse(statusCode: 500, type: typeof(List<ErrorListV1Inner>), description: "Внутренние ошибки сервера, в ответе список ошибок", ContentTypes = ["application/vnd.eda.picker.errors.v1+json"])]
        public virtual IActionResult PartnerAuthPost([FromForm(Name = "client_id")][Required()] string clientId, [FromForm(Name = "client_secret")][Required()] string clientSecret, [FromForm(Name = "grant_type")][Required()] string grantType, [FromForm(Name = "scope")][Required()] string scope)
        {

            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret) || string.IsNullOrEmpty(grantType) || string.IsNullOrEmpty(scope))
            {
                return BadRequest("Username and/or password not specified.");
            }

            string hash = Convert.ToHexString(SHA256.Create().ComputeHash(Convert.FromBase64String($"{clientSecret}")));
            string etalonHash = SecretGeneratorService.GetClientSecretHash(StaticConfig.ClientSecretFilenameSetToAuto ? "client-secret-hash" : StaticConfig.ClientSecretFilename);

            if (hash == etalonHash && clientId == SecretGeneratorService.GetClientId())
            {
                var secretKey = new SymmetricSecurityKey(SecretGeneratorService.GetIssuerSigningKey(StaticConfig.SigningKeyFileSetToAuto ? "skey" : StaticConfig.SigningKeyFile));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(type: "grant_type", grantType),
                        new Claim(type: "scope", scope)
                    }),
                    Expires = DateTime.UtcNow.AddDays(2),
                    SigningCredentials = new SigningCredentials
                    (secretKey,
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);
                return new ObjectResult(new AuthenticationResponse { AccessToken = stringToken });
            }

            throw new BadHttpRequestException("Missing or invalid access credentials.", 400);
        }
    }
}
