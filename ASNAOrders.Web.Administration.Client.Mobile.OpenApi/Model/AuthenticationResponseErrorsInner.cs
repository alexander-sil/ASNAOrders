/* 
 * ASNAOrders.Web.Administration.Server
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = ASNAOrders.Web.Administration.Client.OpenApi.Client.SwaggerDateConverter;
namespace ASNAOrders.Web.Administration.Client.OpenApi.Model
{
    /// <summary>
    /// AuthenticationResponseErrorsInner
    /// </summary>
    [DataContract]
    public partial class AuthenticationResponseErrorsInner : IEquatable<AuthenticationResponseErrorsInner>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationResponseErrorsInner" /> class.
        /// </summary>
        /// <param name="code">code.</param>
        /// <param name="message">message.</param>
        public AuthenticationResponseErrorsInner(string code = default, string message = default)
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// Gets or Sets Code
        /// </summary>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public string Code { get; set; }

        /// <summary>
        /// Gets or Sets Message
        /// </summary>
        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string Message { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AuthenticationResponseErrorsInner {\n");
            sb.Append("  Code: ").Append(Code).Append("\n");
            sb.Append("  Message: ").Append(Message).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return Equals(input as AuthenticationResponseErrorsInner);
        }

        /// <summary>
        /// Returns true if AuthenticationResponseErrorsInner instances are equal
        /// </summary>
        /// <param name="input">Instance of AuthenticationResponseErrorsInner to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AuthenticationResponseErrorsInner input)
        {
            if (input == null)
                return false;

            return
                (
                    Code == input.Code ||
                    Code != null &&
                    Code.Equals(input.Code)
                ) &&
                (
                    Message == input.Message ||
                    Message != null &&
                    Message.Equals(input.Message)
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
                int hashCode = 41;
                if (Code != null)
                    hashCode = hashCode * 59 + Code.GetHashCode();
                if (Message != null)
                    hashCode = hashCode * 59 + Message.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
