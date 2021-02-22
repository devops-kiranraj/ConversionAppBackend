namespace ConversionApp.Core.DomainModels
{
    public class ErrorDomainModel
    {
        public bool IsError { get; set; }
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }
}
