using Maqta.API.DBContexts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Maqta.API.Models
{
    public class Product: IEntity
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1,1000)]
        public double Price { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set ; }
        public string CreatedBy { get ; set; }
        public DateTime? UpdatedDate { get ; set; }
        public string UpdatedBy { get ; set; }
    }
}
