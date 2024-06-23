using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ProductDto
    {
        public string Name { get; set; } = string.Empty;
        public float Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        public static ProductDto Create(Product product)
        {
            var dto = new ProductDto();
            dto.Name = product.Name;
            dto.Price = product.Price;
            dto.Description = product.Description;
            dto.Category = product.Category;

            return dto;
        }

        public static List<ProductDto> CreateList(IEnumerable<Product> products)
        {
            List<ProductDto> listDto = [];

            foreach (var product in products)
            {
                listDto.Add(Create(product));
            }

            return listDto;
        }
    }
}
