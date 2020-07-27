using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class TicketFilterResult : IEquatable<TicketFilterResult>
    { 
        /// <summary>
        /// Gets or Sets TicketId
        /// </summary>
        [DataMember(Name="ticketId")]
        public int? TicketId { get; set; }

        /// <summary>
        /// Gets or Sets OperationDate
        /// </summary>
        [DataMember(Name="operationDate")]
        public string OperationDate { get; set; }

        /// <summary>
        /// Gets or Sets TicketTypeId
        /// </summary>
        [DataMember(Name="ticketTypeId")]
        public int? TicketTypeId { get; set; }

        /// <summary>
        /// Gets or Sets OperationTypeId
        /// </summary>
        [DataMember(Name="operationTypeId")]
        public int? OperationTypeId { get; set; }

        /// <summary>
        /// Gets or Sets StatusId
        /// </summary>
        [DataMember(Name="statusId")]
        public int? StatusId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TicketFilterResult {\n");
            sb.Append("  TicketId: ").Append(TicketId).Append("\n");
            sb.Append("  OperationDate: ").Append(OperationDate).Append("\n");
            sb.Append("  TicketTypeId: ").Append(TicketTypeId).Append("\n");
            sb.Append("  OperationTypeId: ").Append(OperationTypeId).Append("\n");
            sb.Append("  StatusId: ").Append(StatusId).Append("\n");
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
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((TicketFilterResult)obj);
        }

        /// <summary>
        /// Returns true if TicketFilterResult instances are equal
        /// </summary>
        /// <param name="other">Instance of TicketFilterResult to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TicketFilterResult other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    TicketId == other.TicketId ||
                    TicketId != null &&
                    TicketId.Equals(other.TicketId)
                ) && 
                (
                    OperationDate == other.OperationDate ||
                    OperationDate != null &&
                    OperationDate.Equals(other.OperationDate)
                ) && 
                (
                    TicketTypeId == other.TicketTypeId ||
                    TicketTypeId != null &&
                    TicketTypeId.Equals(other.TicketTypeId)
                ) && 
                (
                    OperationTypeId == other.OperationTypeId ||
                    OperationTypeId != null &&
                    OperationTypeId.Equals(other.OperationTypeId)
                ) && 
                (
                    StatusId == other.StatusId ||
                    StatusId != null &&
                    StatusId.Equals(other.StatusId)
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
                    if (TicketId != null)
                    hashCode = hashCode * 59 + TicketId.GetHashCode();
                    if (OperationDate != null)
                    hashCode = hashCode * 59 + OperationDate.GetHashCode();
                    if (TicketTypeId != null)
                    hashCode = hashCode * 59 + TicketTypeId.GetHashCode();
                    if (OperationTypeId != null)
                    hashCode = hashCode * 59 + OperationTypeId.GetHashCode();
                    if (StatusId != null)
                    hashCode = hashCode * 59 + StatusId.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(TicketFilterResult left, TicketFilterResult right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TicketFilterResult left, TicketFilterResult right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
