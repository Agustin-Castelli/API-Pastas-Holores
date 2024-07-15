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
            return _context.Products.Find(new object[] { name });
        }
    }
}
