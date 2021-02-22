using ConversionApp.Core.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ConversionApp.WebAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ValidateDateFormatAttribute : ValidationAttribute
    {
        public string ValidFormat { get; set; } = MessageConstants.DATEFORMAT;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || validationContext == null)
                return ValidationResult.Success;

            string inputDate = Convert.ToString(value);

            if(!string.IsNullOrWhiteSpace(inputDate) && (!DateTime.TryParseExact(inputDate, ValidFormat, CultureInfo.InvariantCulture,DateTimeStyles.None, out DateTime outputDate)))
            {
                return new ValidationResult(MessageConstants.VALIDATION_MSG_DATE_NOTVALID, new List<string> { validationContext.MemberName});
            }

            return ValidationResult.Success;
        }
    }
}
