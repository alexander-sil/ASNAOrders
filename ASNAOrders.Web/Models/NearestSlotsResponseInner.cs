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
    public partial class NearestSlotsResponseInner : IEquatable<NearestSlotsResponseInner>
    {
        /// <summary>
        /// Внутренний уникальный идентификатор магазина в системе партнёра.
        /// </summary>
        /// <value>Внутренний уникальный идентификатор магазина в системе партнёра.</value>
        [DataMember(Name="placeId", EmitDefaultValue=false)]
        public string PlaceId { get; set; }

        /// <summary>
        /// Gets or Sets NearestTimes
        /// </summary>
        [Required]
        [DataMember(Name="nearest_times", EmitDefaultValue=false)]
        public List<NearestSlotsResponseInnerNearestTimesInner> NearestTimes { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class NearestSlotsResponseInner {\n");
            sb.Append("  PlaceId: ").Append(PlaceId).Append("\n");
            sb.Append("  NearestTimes: ").Append(NearestTimes).Append("\n");
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
            return obj.GetType() == GetType() && Equals((NearestSlotsResponseInner)obj);
        }

        /// <summary>
        /// Returns true if NearestSlotsResponseInner instances are equal
        /// </summary>
        /// <param name="other">Instance of NearestSlotsResponseInner to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(NearestSlotsResponseInner other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    PlaceId == other.PlaceId ||
                    PlaceId != null &&
                    PlaceId.Equals(other.PlaceId)
                ) && 
                (
                    NearestTimes == other.NearestTimes ||
                    NearestTimes != null &&
                    other.NearestTimes != null &&
                    NearestTimes.SequenceEqual(other.NearestTimes)
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
                    if (PlaceId != null)
                    hashCode = hashCode * 59 + PlaceId.GetHashCode();
                    if (NearestTimes != null)
                    hashCode = hashCode * 59 + NearestTimes.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(NearestSlotsResponseInner left, NearestSlotsResponseInner right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(NearestSlotsResponseInner left, NearestSlotsResponseInner right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
