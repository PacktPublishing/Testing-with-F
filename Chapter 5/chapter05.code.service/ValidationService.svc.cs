using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;

namespace chapter05.code.service
{
    public class ValidationService : IValidationService
    {
        const string EmailValidation = "EmailValidation";
        const string EmailExpression = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$";

        public ValidationResult ValidateEmail(string input)
        {
            var result = new ValidationResult(EmailValidation);

            try
            {
                var expression = new Regex(EmailExpression, RegexOptions.IgnoreCase);

                if (expression.IsMatch(input))
                {
                    result.Status = ValidationStatus.Valid;
                }
                else
                {
                    result.Status = ValidationStatus.Invalid;
                }
            }
            catch (Exception ex)
            {
                result.Status = ValidationStatus.Error;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
