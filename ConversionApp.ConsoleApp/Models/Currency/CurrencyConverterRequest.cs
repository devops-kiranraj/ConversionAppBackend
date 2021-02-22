using ConversionApp.ConsoleApp.Attributes;
using ConversionApp.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace ConversionApp.ConsoleApp.Models.Currency
{
    public class CurrencyConverterRequest
    {
        [Required(ErrorMessage = MessageConstants.VALIDATION_MSG_REQUIRED)]
        public string SourceCurrency { get; set; }

        [Required(ErrorMessage = MessageConstants.VALIDATION_MSG_REQUIRED)]
        public string TargetCurrency { get; set; }

        [Required(ErrorMessage = MessageConstants.VALIDATION_MSG_REQUIRED)]
        [RegularExpression(MessageConstants.AMOUNT_REGEX, ErrorMessage = MessageConstants.VALIDATION_MSG_CURRENCYAMOUNT_NOTVALID)]
        public string Amount { get; set; }

        [ValidateDateFormat]
        public string Date { get; set; }
    }
}
