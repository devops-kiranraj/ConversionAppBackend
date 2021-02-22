using System;
using System.Collections.Generic;
using System.Text;

namespace ConversionApp.Core.Constants
{
    public static class MessageConstants
    {
        public const string VALIDATION_MSG_REQUIRED = "This value is required";
        public const string VALIDATION_MSG_CURRENCY_NOTFOUND = "Currency value not found for conversion";
        public const string VALIDATION_MSG_CURRENCYAMOUNT_NOTVALID = "Currency Amount not valid";
        public const string VALIDATION_MSG_DATE_NOTVALID = "Date is not valid";

        public const string STATUSCODE_CURRENCY_NOTFOUND = "CURRENCY_NOTFOUND";
        public const string STATUSCODE_ERRORMSG_GENERIC = "GENERIC_ERROR";
        public const string STATUSMESSAGE_ERRORMSG_GENERIC = "Some error happened in the system.";

        public const string FIXER_APIKEY = "FixerApiKey";
        public const string FIXER_BASEURL = "FixerBaseUrl";

        public const string FIXER_EXCHANGERATE_ACTION = "latest";

        public const string DATEFORMAT = "yyyy-MM-dd";
        public const string AMOUNT_REGEX = @"^\d*(\.\d{0,2})?$";
    }
}
