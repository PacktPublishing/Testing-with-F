using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace chapter05.code.service
{
    [ServiceContract]
    public interface IValidationService
    {
        [OperationContract]
        ValidationResult ValidateEmail(string input);
    }

    [DataContract]
    public class ValidationResult
    {
        public ValidationResult()
        {
        }

        public ValidationResult(string identifier)
        {
            Identifier = identifier;
        }

        [DataMember]
        public string Identifier { get; set; }

        [DataMember]
        public ValidationStatus Status { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }
    }

    [DataContract]
    public enum ValidationStatus
    {
        [EnumMember]
        None = 0,

        [EnumMember]
        Valid = 1,

        [EnumMember]
        Invalid = 2,

        [EnumMember]
        Error = 3,
    }

}
