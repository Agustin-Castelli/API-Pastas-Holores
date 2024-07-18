using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class CartDto
    {
        public ICollection<Product>? Products { get; set; }
        public float TotalPrice { get; set; }
        public CartStatus Status { get; set; }
        public string? PaymentMethod { get; set; }

        public static CartDto? Create(Cart cart)
        {
            if (cart == null)
            {
                return null; 
            }

            var dto = new CartDto();

            dto.Products = cart.Products;
            dto.TotalPrice = cart.TotalPrice;
            dto.Status = cart.Status;
            dto.PaymentMethod = cart.PaymentMethod;

            return dto;
        }
    }
}
