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
    public partial class YandexOrderCreateItemsInnerModificationsInner : IEquatable<YandexOrderCreateItemsInnerModificationsInner>
    {
        /// <summary>
        /// Внутренний уникальный идентификатор модификатора в системе партнера
        /// </summary>
        /// <value>Внутренний уникальный идентификатор модификатора в системе партнера</value>
        /// <example>937c57f6-4508-4858-be7f-20691a16fbb0</example>
        [Required]
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }

        /// <summary>
        /// Наименование модификатора
        /// </summary>
        /// <value>Наименование модификатора</value>
        /// <example>Нарезать</example>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// Количество в заказе
        /// </summary>
        /// <value>Количество в заказе</value>
        /// <example>1</example>
        [Required]
        [DataMember(Name="quantity", EmitDefaultValue=true)]
        public int Quantity { get; set; }

        /// <summary>
        /// Цена модификатора
        /// </summary>
        /// <value>Цена модификатора</value>
        /// <example>39</example>
        [Required]
        [DataMember(Name="price", EmitDefaultValue=true)]
        public double Price { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class YandexOrderCreateItemsInnerModificationsInner {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Quantity: ").Append(Quantity).Append("\n");
            sb.Append("  Price: ").Append(Price).Append("\n");
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
            return obj.GetType() == GetType() && Equals((YandexOrderCreateItemsInnerModificationsInner)obj);
        }

        /// <summary>
        /// Returns true if YandexOrderCreateItemsInnerModificationsInner instances are equal
        /// </summary>
        /// <param name="other">Instance of YandexOrderCreateItemsInnerModificationsInner to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(YandexOrderCreateItemsInnerModificationsInner other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Id == other.Id ||
                    Id != null &&
                    Id.Equals(other.Id)
                ) && 
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.Equals(other.Name)
                ) && 
                (
                    Quantity == other.Quantity ||
                    
                    Quantity.Equals(other.Quantity)
                ) && 
                (
                    Price == other.Price ||
                    
                    Price.Equals(other.Price)
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
                    if (Id != null)
                    hashCode = hashCode * 59 + Id.GetHashCode();
                    if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                    
                    hashCode = hashCode * 59 + Quantity.GetHashCode();
                    
                    hashCode = hashCode * 59 + Price.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(YandexOrderCreateItemsInnerModificationsInner left, YandexOrderCreateItemsInnerModificationsInner right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(YandexOrderCreateItemsInnerModificationsInner left, YandexOrderCreateItemsInnerModificationsInner right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
