using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepositoryEf : BaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationContext _context;

        public ProductRepositoryEf(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public Product? GetByName(string name)
        {
            var productFound = _context.Products.FirstOrDefault(p => p.Name == name);
            
            if (productFound != null)
            {
                return productFound;
            }
            else
            {
                return null;
            }
        }
    }
}
