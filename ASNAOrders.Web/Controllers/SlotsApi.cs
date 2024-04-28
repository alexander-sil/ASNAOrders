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
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using ASNAOrders.Web.Attributes;
using ASNAOrders.Web.Models;
using ASNAOrders.Web.Filters;
using ASNAOrders.Web.Data;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;

namespace ASNAOrders.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>

    [ApiController]
    public class SlotsApiController : ControllerBase
    {
        /// <summary>
        /// Контекст БД контроллера слотов
        /// </summary>
        private ASNAOrdersDbContext Context { get; set; }

        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        public SlotsApiController(ASNAOrdersDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Слоты для использования на Главной для отображения ближайшего времени доставки
        /// </summary>
        /// <param name="nearestSlots"></param>
        /// <response code="200">Усредненные слоты</response>
        /// <response code="400">Ошибка в запросе, в ответе список ошибок</response>
        /// <response code="401">Не пройдена авторизация, в ответе список ошибок</response>
        /// <response code="404">Не найден ресурс, в ответе список ошибок</response>
        /// <response code="500">Внутренние ошибки сервера, в ответе список ошибок</response>
        [HttpPost]
        [Authorize(Policy = "ClientCredsReadWrite")]
        [Route("/places/nearest_delivery_times")]
        [Consumes("application/json")]
        [ValidateModelState]
        [SwaggerOperation("PartnerNearestSlotsGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<NearestSlotsResponseInner>), description: "Усредненные слоты", ContentTypes = ["application/json"])]
        [SwaggerResponse(statusCode: 400, type: typeof(List<ErrorListInner>), description: "Ошибка в запросе, в ответе список ошибок", ContentTypes = ["application/json"])]
        [SwaggerResponse(statusCode: 401, type: typeof(List<ErrorListInner>), description: "Не пройдена авторизация, в ответе список ошибок", ContentTypes = ["application/json"])]
        [SwaggerResponse(statusCode: 404, type: typeof(List<ErrorListInner>), description: "Не найден ресурс, в ответе список ошибок", ContentTypes = ["application/json"])]
        [SwaggerResponse(statusCode: 500, type: typeof(List<ErrorListInner>), description: "Внутренние ошибки сервера, в ответе список ошибок", ContentTypes = ["application/json"])]
        public virtual IActionResult PartnerNearestSlotsGet([FromBody] NearestSlots nearestSlots)
        {
            List<NearestSlotsResponseInner> slots = new List<NearestSlotsResponseInner>()
            {
                new NearestSlotsResponseInner()
                {
                    NearestTimes = new List<NearestSlotsResponseInnerNearestTimesInner>()
                    {
                        new NearestSlotsResponseInnerNearestTimesInner()
                        {
                            Id = new Random().Next(10000, 19999),
                            StartTime = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.fffK"),
                            EndTime = DateTime.Now.AddHours(6).ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.fffK")
                        }
                    }
                }
            };

            return new ContentResult()
            {
                ContentType = Properties.Resources.ApplicationJsonString,
                Content = JsonConvert.SerializeObject(slots),
                StatusCode = 200
            };
        }

        /// <summary>
        /// Слоты для использования в Корзине при оформлении заказа
        /// </summary>
        /// <param name="placeId">Внутренний уникальный идентификатор магазина в системе партнёра.</param>
        /// <param name="slotsCheckout"></param>
        /// <response code="200">Успешный запрос слотов</response>
        /// <response code="400">Ошибка в запросе, в ответе список ошибок</response>
        /// <response code="401">Не пройдена авторизация, в ответе список ошибок</response>
        /// <response code="404">Не найден ресурс, в ответе список ошибок</response>
        /// <response code="500">Внутренние ошибки сервера, в ответе список ошибок</response>
        [HttpPost]
        [Authorize(Policy = "ClientCredsReadWrite")]
        [Route("/places/{placeId}/delivery_times")]
        [Consumes("application/json")]
        [ValidateModelState]
        [SwaggerOperation("PartnerSlotsGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(SlotsCheckoutResponse), description: "Успешный запрос слотов", ContentTypes = ["application/json"])]
        [SwaggerResponse(statusCode: 400, type: typeof(List<ErrorListInner>), description: "Ошибка в запросе, в ответе список ошибок", ContentTypes = ["application/json"])]
        [SwaggerResponse(statusCode: 401, type: typeof(List<ErrorListInner>), description: "Не пройдена авторизация, в ответе список ошибок", ContentTypes = ["application/json"])]
        [SwaggerResponse(statusCode: 404, type: typeof(List<ErrorListInner>), description: "Не найден ресурс, в ответе список ошибок", ContentTypes = ["application/json"])]
        [SwaggerResponse(statusCode: 500, type: typeof(List<ErrorListInner>), description: "Внутренние ошибки сервера, в ответе список ошибок", ContentTypes = ["application/json"])]
        public virtual IActionResult PartnerSlotsGet([FromRoute(Name = "placeId")][Required] string placeId, [FromBody] SlotsCheckout slotsCheckout)
        {
            return new ContentResult()
            {
                ContentType = Properties.Resources.ApplicationJsonString,
                Content = JsonConvert.SerializeObject(new SlotsCheckoutResponse()
                {
                    DeliveryTimes = new List<SlotsCheckoutResponseDeliveryTimesInner>()
                    {
                        new SlotsCheckoutResponseDeliveryTimesInner()
                        {
                            Id = new Random().Next(20000, 29999),
                            StartTime = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.fffK"),
                            EndTime = DateTime.Now.AddHours(6).ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.fffK")
                        }
                    }
                }),
                StatusCode = 200
            };
        }
    }
}
