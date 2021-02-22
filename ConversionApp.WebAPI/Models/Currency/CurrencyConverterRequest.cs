using ConversionApp.Core.Constants;
using ConversionApp.WebAPI.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ConversionApp.WebAPI.Models.Currency
{
    public class CurrencyConverterRequest
    {
        [Required(ErrorMessage = MessageConstants.VALIDATION_MSG_REQUIRED)]
        public string SourceCurrency { get; set; }

        [Required(ErrorMessage = MessageConstants.VALIDATION_MSG_REQUIRED)]
        public string TargetCurrency { get; set; }

        [Required(ErrorMessage = MessageConstants.VALIDATION_MSG_REQUIRED)]
        [RegularExpression(MessageConstants.AMOUNT_REGEX, ErrorMessage = MessageConstants.VALIDATION_MSG_CURRENCYAMOUNT_NOTVALID)]
        public decimal Amount { get; set; }

        [ValidateDateFormat]
        public string Date { get; set; }
    }
}
