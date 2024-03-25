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
using ASNAOrders.Web.Converters;

namespace ASNAOrders.Web.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class MarketplaceOrderCreatePaymentInfo : IEquatable<MarketplaceOrderCreatePaymentInfo>
    {

        /// <summary>
        /// Информация о типе оплаты
        /// </summary>
        /// <value>Информация о типе оплаты</value>
        [TypeConverter(typeof(CustomEnumConverter<PaymentTypeEnum>))]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum PaymentTypeEnum
        {
            
            /// <summary>
            /// Enum CARDEnum for CARD
            /// </summary>
            [EnumMember(Value = "CARD")]
            CARDEnum = 1,
            
            /// <summary>
            /// Enum CASHEnum for CASH
            /// </summary>
            [EnumMember(Value = "CASH")]
            CASHEnum = 2
        }

        /// <summary>
        /// Информация о типе оплаты
        /// </summary>
        /// <value>Информация о типе оплаты</value>
        [Required]
        [DataMember(Name="paymentType", EmitDefaultValue=true)]
        public PaymentTypeEnum PaymentType { get; set; }

        /// <summary>
        /// Полная сумма стоимости товаров в заказе
        /// </summary>
        /// <value>Полная сумма стоимости товаров в заказе</value>
        /// <example>1570</example>
        [Required]
        [DataMember(Name="itemsCost", EmitDefaultValue=true)]
        public double ItemsCost { get; set; }

        /// <summary>
        /// Стоимость доставки
        /// </summary>
        /// <value>Стоимость доставки</value>
        /// <example>179</example>
        [Required]
        [DataMember(Name="deliveryFee", EmitDefaultValue=true)]
        public double DeliveryFee { get; set; }

        /// <summary>
        /// Общая стоимость заказа
        /// </summary>
        /// <value>Общая стоимость заказа</value>
        /// <example>1749</example>
        [DataMember(Name="total", EmitDefaultValue=true)]
        public double Total { get; set; }

        /// <summary>
        /// Сумма, с которой потребуется сдача. Другими словами, это сумма которой клиент планирует расплатиться. Обычно оплата клиентом происходит картой на стороне Яндекса и партнерам данное поле передается пустым.
        /// </summary>
        /// <value>Сумма, с которой потребуется сдача. Другими словами, это сумма которой клиент планирует расплатиться. Обычно оплата клиентом происходит картой на стороне Яндекса и партнерам данное поле передается пустым.</value>
        /// <example>0</example>
        [Required]
        [DataMember(Name="change", EmitDefaultValue=true)]
        public double Change { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class MarketplaceOrderCreatePaymentInfo {\n");
            sb.Append("  PaymentType: ").Append(PaymentType).Append("\n");
            sb.Append("  ItemsCost: ").Append(ItemsCost).Append("\n");
            sb.Append("  DeliveryFee: ").Append(DeliveryFee).Append("\n");
            sb.Append("  Total: ").Append(Total).Append("\n");
            sb.Append("  Change: ").Append(Change).Append("\n");
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
            return obj.GetType() == GetType() && Equals((MarketplaceOrderCreatePaymentInfo)obj);
        }

        /// <summary>
        /// Returns true if MarketplaceOrderCreatePaymentInfo instances are equal
        /// </summary>
        /// <param name="other">Instance of MarketplaceOrderCreatePaymentInfo to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(MarketplaceOrderCreatePaymentInfo other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    PaymentType == other.PaymentType ||
                    
                    PaymentType.Equals(other.PaymentType)
                ) && 
                (
                    ItemsCost == other.ItemsCost ||
                    
                    ItemsCost.Equals(other.ItemsCost)
                ) && 
                (
                    DeliveryFee == other.DeliveryFee ||
                    
                    DeliveryFee.Equals(other.DeliveryFee)
                ) && 
                (
                    Total == other.Total ||
                    
                    Total.Equals(other.Total)
                ) && 
                (
                    Change == other.Change ||
                    
                    Change.Equals(other.Change)
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
                    
                    hashCode = hashCode * 59 + PaymentType.GetHashCode();
                    
                    hashCode = hashCode * 59 + ItemsCost.GetHashCode();
                    
                    hashCode = hashCode * 59 + DeliveryFee.GetHashCode();
                    
                    hashCode = hashCode * 59 + Total.GetHashCode();
                    
                    hashCode = hashCode * 59 + Change.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(MarketplaceOrderCreatePaymentInfo left, MarketplaceOrderCreatePaymentInfo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MarketplaceOrderCreatePaymentInfo left, MarketplaceOrderCreatePaymentInfo right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
