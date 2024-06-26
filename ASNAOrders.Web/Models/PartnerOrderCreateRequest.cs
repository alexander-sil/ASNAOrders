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
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using JsonSubTypes;
using Swashbuckle.AspNetCore.Annotations;
using ASNAOrders.Web.Converters;

namespace ASNAOrders.Web.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [JsonConverter(typeof(JsonSubtypes), "Discriminator")]
    [SwaggerDiscriminator("Discriminator")]
    [JsonSubtypes.KnownSubType(typeof(MarketplaceOrderCreate), "marketplace")]
    [SwaggerSubType(typeof(MarketplaceOrderCreate), DiscriminatorValue =  "marketplace")]
    [JsonSubtypes.KnownSubType(typeof(PickupOrderV1), "pickup")]
    [SwaggerSubType(typeof(PickupOrderV1), DiscriminatorValue =  "pickup")]
    [JsonSubtypes.KnownSubType(typeof(YandexOrderCreate), "yandex")]
    [SwaggerSubType(typeof(YandexOrderCreate), DiscriminatorValue =  "yandex")]
    public partial class PartnerOrderCreateRequest : IEquatable<PartnerOrderCreateRequest>
    {

        /// <summary>
        /// Идентификатор платформы, DC - Delivery Club, YE - Yandex Eda
        /// </summary>
        /// <value>Идентификатор платформы, DC - Delivery Club, YE - Yandex Eda</value>
        [TypeConverter(typeof(CustomEnumConverter<PlatformEnum>))]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum PlatformEnum
        {
            
            /// <summary>
            /// Enum YEEnum for YE
            /// </summary>
            [EnumMember(Value = "YE")]
            YEEnum = 1,
            
            /// <summary>
            /// Enum DCEnum for DC
            /// </summary>
            [EnumMember(Value = "DC")]
            DCEnum = 2
        }

        /// <summary>
        /// Идентификатор платформы, DC - Delivery Club, YE - Yandex Eda
        /// </summary>
        /// <value>Идентификатор платформы, DC - Delivery Club, YE - Yandex Eda</value>
        /// <example>YE</example>
        [DataMember(Name="platform", EmitDefaultValue=true)]
        public PlatformEnum Platform { get; set; }

        /// <summary>
        /// Дискриминатор схемы. Для заказов с самовывозом равен \&quot;pickup\&quot;
        /// </summary>
        /// <value>Дискриминатор схемы. Для заказов с самовывозом равен \&quot;pickup\&quot;</value>
        /// <example>pickup</example>
        [Required]
        [DataMember(Name="discriminator", EmitDefaultValue=false)]
        public string Discriminator { get; set; }

        /// <summary>
        /// Сквозной идентификатор заказа на стороне Яндекс Еды в формате DDDDDD-DDDDDDD
        /// </summary>
        /// <value>Сквозной идентификатор заказа на стороне Яндекс Еды в формате DDDDDD-DDDDDDD</value>
        /// <example>230130-1234567</example>
        [Required]
        [DataMember(Name="eatsId", EmitDefaultValue=false)]
        public string EatsId { get; set; }

        /// <summary>
        /// Внутренний уникальный идентификатор магазина в системе партнёра. Формат свободный, рекомендуется UUID4
        /// </summary>
        /// <value>Внутренний уникальный идентификатор магазина в системе партнёра. Формат свободный, рекомендуется UUID4</value>
        /// <example>937c57f6-4508-4858-be7f-20691a16fbb0</example>
        [DataMember(Name="restaurantId", EmitDefaultValue=false)]
        public string RestaurantId { get; set; }

        /// <summary>
        /// Gets or Sets DeliveryInfo
        /// </summary>
        [Required]
        [DataMember(Name="deliveryInfo", EmitDefaultValue=false)]
        public PickupOrderV1DeliveryInfo DeliveryInfo { get; set; }

        /// <summary>
        /// Gets or Sets PaymentInfo
        /// </summary>
        [Required]
        [DataMember(Name="paymentInfo", EmitDefaultValue=false)]
        public PickupOrderV1PaymentInfo PaymentInfo { get; set; }

        /// <summary>
        /// Gets or Sets Items
        /// </summary>
        [Required]
        [DataMember(Name="items", EmitDefaultValue=false)]
        public List<PickupOrderV1ItemsInner> Items { get; set; }

        /// <summary>
        /// Параметр не поддерживается в интеграции магазинов и передается пустым
        /// </summary>
        /// <value>Параметр не поддерживается в интеграции магазинов и передается пустым</value>
        /// <example>0</example>
        [DataMember(Name="persons", EmitDefaultValue=true)]
        public int Persons { get; set; }

        /// <summary>
        /// Дополнительная информация о заказе
        /// </summary>
        /// <value>Дополнительная информация о заказе</value>
        /// <example>Дополнительная информация о заказе: ...</example>
        [Required]
        [DataMember(Name="comment", EmitDefaultValue=false)]
        public string Comment { get; set; }

        /// <summary>
        /// Параметр не поддерживается в интеграции магазинов и передается пустым
        /// </summary>
        /// <value>Параметр не поддерживается в интеграции магазинов и передается пустым</value>
        [Required]
        [DataMember(Name="promos", EmitDefaultValue=false)]
        public List<PickupOrderV1PromosInner> Promos { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PartnerOrderCreateRequest {\n");
            sb.Append("  Platform: ").Append(Platform).Append("\n");
            sb.Append("  Discriminator: ").Append(Discriminator).Append("\n");
            sb.Append("  EatsId: ").Append(EatsId).Append("\n");
            sb.Append("  RestaurantId: ").Append(RestaurantId).Append("\n");
            sb.Append("  DeliveryInfo: ").Append(DeliveryInfo).Append("\n");
            sb.Append("  PaymentInfo: ").Append(PaymentInfo).Append("\n");
            sb.Append("  Items: ").Append(Items).Append("\n");
            sb.Append("  Persons: ").Append(Persons).Append("\n");
            sb.Append("  Comment: ").Append(Comment).Append("\n");
            sb.Append("  Promos: ").Append(Promos).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((PartnerOrderCreateRequest)obj);
        }

        /// <summary>
        /// Returns true if PartnerOrderCreateRequest instances are equal
        /// </summary>
        /// <param name="other">Instance of PartnerOrderCreateRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PartnerOrderCreateRequest other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Platform == other.Platform ||
                    
                    Platform.Equals(other.Platform)
                ) && 
                (
                    Discriminator == other.Discriminator ||
                    Discriminator != null &&
                    Discriminator.Equals(other.Discriminator)
                ) && 
                (
                    EatsId == other.EatsId ||
                    EatsId != null &&
                    EatsId.Equals(other.EatsId)
                ) && 
                (
                    RestaurantId == other.RestaurantId ||
                    RestaurantId != null &&
                    RestaurantId.Equals(other.RestaurantId)
                ) && 
                (
                    DeliveryInfo == other.DeliveryInfo ||
                    DeliveryInfo != null &&
                    DeliveryInfo.Equals(other.DeliveryInfo)
                ) && 
                (
                    PaymentInfo == other.PaymentInfo ||
                    PaymentInfo != null &&
                    PaymentInfo.Equals(other.PaymentInfo)
                ) && 
                (
                    Items == other.Items ||
                    Items != null &&
                    other.Items != null &&
                    Items.SequenceEqual(other.Items)
                ) && 
                (
                    Persons == other.Persons ||
                    
                    Persons.Equals(other.Persons)
                ) && 
                (
                    Comment == other.Comment ||
                    Comment != null &&
                    Comment.Equals(other.Comment)
                ) && 
                (
                    Promos == other.Promos ||
                    Promos != null &&
                    other.Promos != null &&
                    Promos.SequenceEqual(other.Promos)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    
                    hashCode = hashCode * 59 + Platform.GetHashCode();
                    if (Discriminator != null)
                    hashCode = hashCode * 59 + Discriminator.GetHashCode();
                    if (EatsId != null)
                    hashCode = hashCode * 59 + EatsId.GetHashCode();
                    if (RestaurantId != null)
                    hashCode = hashCode * 59 + RestaurantId.GetHashCode();
                    if (DeliveryInfo != null)
                    hashCode = hashCode * 59 + DeliveryInfo.GetHashCode();
                    if (PaymentInfo != null)
                    hashCode = hashCode * 59 + PaymentInfo.GetHashCode();
                    if (Items != null)
                    hashCode = hashCode * 59 + Items.GetHashCode();
                    
                    hashCode = hashCode * 59 + Persons.GetHashCode();
                    if (Comment != null)
                    hashCode = hashCode * 59 + Comment.GetHashCode();
                    if (Promos != null)
                    hashCode = hashCode * 59 + Promos.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(PartnerOrderCreateRequest left, PartnerOrderCreateRequest right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PartnerOrderCreateRequest left, PartnerOrderCreateRequest right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
