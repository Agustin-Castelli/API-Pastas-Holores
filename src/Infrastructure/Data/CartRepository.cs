using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationContext _context;

        public CartRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Cart? GetCart(int clientId)
        {
            return _context.Set<Cart>().Include(x => x.Products).FirstOrDefault(c => c.ClientId == clientId);
        }

        public void CreateCartForClient(int clientId)
        {
            if (!_context.Set<Cart>().Any(c => c.ClientId == clientId))
            {
                Cart newCart = new Cart
                {
                    ClientId = clientId
                };

                _context.Set<Cart>().Add(newCart);
                _context.SaveChanges();
            }
        }

        public void Update() 
        {
            _context.SaveChanges();
        }

        public void CalculateTotalProductPrice(int clientId)
        {
            var clientCart = _context.Set<Cart>().Include(x => x.Products).FirstOrDefault(c => c.ClientId == clientId);

            if (clientCart != null)
            {
                float totalPrice = 0;

                foreach (var product in clientCart.Products)
                {
                    totalPrice += product.Price;
                }

                clientCart.TotalPrice = totalPrice;

                _context.SaveChanges();
            }
        }
    }
}
