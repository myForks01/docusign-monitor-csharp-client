/* 
 * Monitor API
 *
 * An API for an integrator to access the features of DocuSign Monitor
 *
 * OpenAPI spec version: v2.0
 * Contact: devcenter@docusign.com
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
using SwaggerDateConverter = DocuSign.Monitor.Client.SwaggerDateConverter;

namespace DocuSign.Monitor.Model
{
    /// <summary>
    /// WebQuery
    /// </summary>
    [DataContract]
    public partial class WebQuery :  IEquatable<WebQuery>, IValidatableObject
    {
        public WebQuery()
        {
            // Empty Constructor
        }

        /// <summary>
        /// Defines QueryScope
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum QueryScopeEnum
        {
            
            /// <summary>
            /// Enum OrganizationId for value: OrganizationId
            /// </summary>
            [EnumMember(Value = "OrganizationId")]
            OrganizationId = 1
        }

        /// <summary>
        /// Gets or Sets QueryScope
        /// </summary>
        [DataMember(Name="queryScope", EmitDefaultValue=false)]
        public QueryScopeEnum? QueryScope { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="WebQuery" /> class.
        /// </summary>
        /// <param name="Filters">Filters.</param>
        /// <param name="Aggregations">Aggregations.</param>
        /// <param name="QueryScope">QueryScope.</param>
        /// <param name="QueryScopeId">QueryScopeId.</param>
        public WebQuery(List<Object> Filters = default(List<Object>), List<Object> Aggregations = default(List<Object>), QueryScopeEnum? QueryScope = default(QueryScopeEnum?), string QueryScopeId = default(string))
        {
            this.Filters = Filters;
            this.Aggregations = Aggregations;
            this.QueryScope = QueryScope;
            this.QueryScopeId = QueryScopeId;
        }
        
        /// <summary>
        /// Gets or Sets Filters
        /// </summary>
        [DataMember(Name="filters", EmitDefaultValue=false)]
        public List<Object> Filters { get; set; }
        /// <summary>
        /// Gets or Sets Aggregations
        /// </summary>
        [DataMember(Name="aggregations", EmitDefaultValue=false)]
        public List<Object> Aggregations { get; set; }
        /// <summary>
        /// Gets or Sets QueryScopeId
        /// </summary>
        [DataMember(Name="queryScopeId", EmitDefaultValue=false)]
        public string QueryScopeId { get; set; }
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class WebQuery {\n");
            sb.Append("  Filters: ").Append(Filters).Append("\n");
            sb.Append("  Aggregations: ").Append(Aggregations).Append("\n");
            sb.Append("  QueryScope: ").Append(QueryScope).Append("\n");
            sb.Append("  QueryScopeId: ").Append(QueryScopeId).Append("\n");
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
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as WebQuery);
        }

        /// <summary>
        /// Returns true if WebQuery instances are equal
        /// </summary>
        /// <param name="other">Instance of WebQuery to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(WebQuery other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Filters == other.Filters ||
                    this.Filters != null &&
                    this.Filters.SequenceEqual(other.Filters)
                ) && 
                (
                    this.Aggregations == other.Aggregations ||
                    this.Aggregations != null &&
                    this.Aggregations.SequenceEqual(other.Aggregations)
                ) && 
                (
                    this.QueryScope == other.QueryScope ||
                    this.QueryScope != null &&
                    this.QueryScope.Equals(other.QueryScope)
                ) && 
                (
                    this.QueryScopeId == other.QueryScopeId ||
                    this.QueryScopeId != null &&
                    this.QueryScopeId.Equals(other.QueryScopeId)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks etc, of course :)
                if (this.Filters != null)
                    hash = hash * 59 + this.Filters.GetHashCode();
                if (this.Aggregations != null)
                    hash = hash * 59 + this.Aggregations.GetHashCode();
                if (this.QueryScope != null)
                    hash = hash * 59 + this.QueryScope.GetHashCode();
                if (this.QueryScopeId != null)
                    hash = hash * 59 + this.QueryScopeId.GetHashCode();
                return hash;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        { 
            yield break;
        }
    }
}
