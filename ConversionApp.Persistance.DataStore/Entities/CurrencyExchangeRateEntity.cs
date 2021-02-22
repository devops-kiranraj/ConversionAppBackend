using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ConversionApp.Persistance.DataStore.Entities
{
    [Table("CurrencyExchangeRate")]
    public class CurrencyExchangeRateEntity
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public string Curerncy { get; set; }
        public decimal ConversionRate { get; set; }
    }
}
