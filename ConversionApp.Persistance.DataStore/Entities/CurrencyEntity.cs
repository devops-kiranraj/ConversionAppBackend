using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ConversionApp.Persistance.DataStore.Entities
{
    [Table("Currency")]
    public class CurrencyEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
    }
}
