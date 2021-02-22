using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ConversionApp.Persistance.DataStore.Entities
{
    [Table("CurrencyForexRate")]
    public class CurrencyForexRateEntity
    {
        [Key]
        public int Id { get; set; }
        public int CurrencyId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public decimal ConversionRate { get; set; }

        [ForeignKey("CurrencyId")]
        public CurrencyEntity Currency { get; set; }
    }
}
