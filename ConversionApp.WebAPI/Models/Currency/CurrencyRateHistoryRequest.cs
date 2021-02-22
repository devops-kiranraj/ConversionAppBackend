using ConversionApp.Core.Constants;
using ConversionApp.WebAPI.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConversionApp.WebAPI.Models.Currency
{
    public class CurrencyRateHistoryRequest
    {
        [Required(ErrorMessage = MessageConstants.VALIDATION_MSG_REQUIRED)]
        public string SourceCurrency { get; set; }
        
        public string TargetCurrency { get; set; }        

        [ValidateDateFormat]
        public string DateFrom { get; set; }

        [ValidateDateFormat]
        public string DateTo { get; set; }
    }
}
