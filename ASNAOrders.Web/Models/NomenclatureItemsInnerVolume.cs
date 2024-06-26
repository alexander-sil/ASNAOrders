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
    /// Объем занимаемый товаром. Полезен для расчета доставки. Может существенно отличатся от номинального объема товара, например, подарочные наборы, крупные упаковки
    /// </summary>
    [DataContract]
    public partial class NomenclatureItemsInnerVolume : IEquatable<NomenclatureItemsInnerVolume>
    {
        /// <summary>
        /// Значение объема
        /// </summary>
        /// <value>Значение объема</value>
        /// <example>100</example>
        [Required]
        [DataMember(Name="value", EmitDefaultValue=true)]
        public int Value { get; set; }


        /// <summary>
        /// Единица измерения обьема
        /// </summary>
        /// <value>Единица измерения обьема</value>
        [TypeConverter(typeof(CustomEnumConverter<UnitEnum>))]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum UnitEnum
        {
            
            /// <summary>
            /// Enum CMQEnum for CMQ
            /// </summary>
            [EnumMember(Value = "CMQ")]
            CMQEnum = 1,
            
            /// <summary>
            /// Enum DMQEnum for DMQ
            /// </summary>
            [EnumMember(Value = "DMQ")]
            DMQEnum = 2
        }

        /// <summary>
        /// Единица измерения обьема
        /// </summary>
        /// <value>Единица измерения обьема</value>
        /// <example>DMQ</example>
        [Required]
        [DataMember(Name="unit", EmitDefaultValue=true)]
        public UnitEnum Unit { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class NomenclatureItemsInnerVolume {\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
            sb.Append("  Unit: ").Append(Unit).Append("\n");
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
            return obj.GetType() == GetType() && Equals((NomenclatureItemsInnerVolume)obj);
        }

        /// <summary>
        /// Returns true if NomenclatureItemsInnerVolume instances are equal
        /// </summary>
        /// <param name="other">Instance of NomenclatureItemsInnerVolume to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(NomenclatureItemsInnerVolume other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Value == other.Value ||
                    
                    Value.Equals(other.Value)
                ) && 
                (
                    Unit == other.Unit ||
                    
                    Unit.Equals(other.Unit)
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
                    
                    hashCode = hashCode * 59 + Value.GetHashCode();
                    
                    hashCode = hashCode * 59 + Unit.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(NomenclatureItemsInnerVolume left, NomenclatureItemsInnerVolume right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(NomenclatureItemsInnerVolume left, NomenclatureItemsInnerVolume right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
