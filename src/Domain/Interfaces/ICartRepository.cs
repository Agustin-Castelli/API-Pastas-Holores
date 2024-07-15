using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICartRepository
    {
        public void CreateCartForClient(int clientId);
        public void Update();
        public Cart? GetCart(int clientId);
        public void CalculateTotalProductPrice(int clientId);
    }
}
