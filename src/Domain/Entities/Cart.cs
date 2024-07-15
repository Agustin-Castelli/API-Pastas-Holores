using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public ICollection<Product>? Products { get; set; } = new List<Product>();
        public float TotalPrice { get; set; }
        public CartStatus Status { get; set; }
        public string? PaymentMethod { get; set; }
        public Client? Client { get; set; }

        public Cart()
        {
            Products = new List<Product>();
            TotalPrice = 0;
            Status = CartStatus.Pending;
            PaymentMethod = string.Empty;
        }
    }
}
