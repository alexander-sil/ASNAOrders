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
    public partial class NomenclatureItemsInnerImagesInner : IEquatable<NomenclatureItemsInnerImagesInner>
    {
        /// <summary>
        /// SHA1-хэш от содержимого файла изображения. Рассчитывается партнером, служит признаком уникальности. В случае если он меняется, Яндекс Еда перезагружает картинку
        /// </summary>
        /// <value>SHA1-хэш от содержимого файла изображения. Рассчитывается партнером, служит признаком уникальности. В случае если он меняется, Яндекс Еда перезагружает картинку</value>
        /// <example>2fd4e1c6 7a2d28fc ed849ee1 bb76e739 1b93eb12</example>
        [Required]
        [DataMember(Name="hash", EmitDefaultValue=false)]
        public string Hash { get; set; }

        /// <summary>
        /// Ссылка на изображение товара для скачивания
        /// </summary>
        /// <value>Ссылка на изображение товара для скачивания</value>
        /// <example>https://ya.ru/images/image_1.jpg</example>
        [Required]
        [DataMember(Name="url", EmitDefaultValue=false)]
        public string Url { get; set; }

        /// <summary>
        /// Порядок сортировки от меньшего к большему
        /// </summary>
        /// <value>Порядок сортировки от меньшего к большему</value>
        /// <example>0</example>
        [DataMember(Name="order", EmitDefaultValue=true)]
        public int Order { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class NomenclatureItemsInnerImagesInner {\n");
            sb.Append("  Hash: ").Append(Hash).Append("\n");
            sb.Append("  Url: ").Append(Url).Append("\n");
            sb.Append("  Order: ").Append(Order).Append("\n");
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
            return obj.GetType() == GetType() && Equals((NomenclatureItemsInnerImagesInner)obj);
        }

        /// <summary>
        /// Returns true if NomenclatureItemsInnerImagesInner instances are equal
        /// </summary>
        /// <param name="other">Instance of NomenclatureItemsInnerImagesInner to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(NomenclatureItemsInnerImagesInner other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Hash == other.Hash ||
                    Hash != null &&
                    Hash.Equals(other.Hash)
                ) && 
                (
                    Url == other.Url ||
                    Url != null &&
                    Url.Equals(other.Url)
                ) && 
                (
                    Order == other.Order ||
                    
                    Order.Equals(other.Order)
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
                    if (Hash != null)
                    hashCode = hashCode * 59 + Hash.GetHashCode();
                    if (Url != null)
                    hashCode = hashCode * 59 + Url.GetHashCode();
                    
                    hashCode = hashCode * 59 + Order.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(NomenclatureItemsInnerImagesInner left, NomenclatureItemsInnerImagesInner right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(NomenclatureItemsInnerImagesInner left, NomenclatureItemsInnerImagesInner right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
