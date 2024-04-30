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
    /// UserPermissionsDataModel
    /// </summary>
    [DataContract]
    public partial class UserPermissionsDataModel : IEquatable<UserPermissionsDataModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserPermissionsDataModel" /> class.
        /// </summary>
        /// <param name="id">id.</param>
        /// <param name="_operator">_operator.</param>
        /// <param name="optionsViewEdit">optionsViewEdit.</param>
        /// <param name="optionsView">optionsView.</param>
        public UserPermissionsDataModel(int? id = default, bool? _operator = default, bool? optionsViewEdit = default, bool? optionsView = default)
        {
            Id = id;
            _Operator = _operator;
            OptionsViewEdit = optionsViewEdit;
            OptionsView = optionsView;
        }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or Sets _Operator
        /// </summary>
        [DataMember(Name = "operator", EmitDefaultValue = false)]
        public bool? _Operator { get; set; }

        /// <summary>
        /// Gets or Sets OptionsViewEdit
        /// </summary>
        [DataMember(Name = "optionsViewEdit", EmitDefaultValue = false)]
        public bool? OptionsViewEdit { get; set; }

        /// <summary>
        /// Gets or Sets OptionsView
        /// </summary>
        [DataMember(Name = "optionsView", EmitDefaultValue = false)]
        public bool? OptionsView { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class UserPermissionsDataModel {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  _Operator: ").Append(_Operator).Append("\n");
            sb.Append("  OptionsViewEdit: ").Append(OptionsViewEdit).Append("\n");
            sb.Append("  OptionsView: ").Append(OptionsView).Append("\n");
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
            return Equals(input as UserPermissionsDataModel);
        }

        /// <summary>
        /// Returns true if UserPermissionsDataModel instances are equal
        /// </summary>
        /// <param name="input">Instance of UserPermissionsDataModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(UserPermissionsDataModel input)
        {
            if (input == null)
                return false;

            return
                (
                    Id == input.Id ||
                    Id != null &&
                    Id.Equals(input.Id)
                ) &&
                (
                    _Operator == input._Operator ||
                    _Operator != null &&
                    _Operator.Equals(input._Operator)
                ) &&
                (
                    OptionsViewEdit == input.OptionsViewEdit ||
                    OptionsViewEdit != null &&
                    OptionsViewEdit.Equals(input.OptionsViewEdit)
                ) &&
                (
                    OptionsView == input.OptionsView ||
                    OptionsView != null &&
                    OptionsView.Equals(input.OptionsView)
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
                if (Id != null)
                    hashCode = hashCode * 59 + Id.GetHashCode();
                if (_Operator != null)
                    hashCode = hashCode * 59 + _Operator.GetHashCode();
                if (OptionsViewEdit != null)
                    hashCode = hashCode * 59 + OptionsViewEdit.GetHashCode();
                if (OptionsView != null)
                    hashCode = hashCode * 59 + OptionsView.GetHashCode();
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
