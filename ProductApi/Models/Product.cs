using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApi.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0,double.MaxValue)]
        [Precision(18,2)]
        public decimal Price { get; set; }
        [Range(0,int.MaxValue)]
        public int StockAvailable { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
