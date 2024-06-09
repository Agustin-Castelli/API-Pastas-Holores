using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ClientRepositoryEf : IClientRepository
    {
        private readonly ApplicationContext _context;

        public ClientRepositoryEf(ApplicationContext context)
        {
            _context = context;
        }

        static int LastIdAssigned = 0;

        public Client? GetById(int id)
        {
            return _context.Clients.FirstOrDefault(c => c.Id == id);
        }

        public List<Client> GetAll()
        {
            return _context.Clients.ToList();
        }

        public Client Add(Client client)
        {
            client.Id = LastIdAssigned++;
            _context.Clients.Add(client);
            _context.SaveChanges();
            return client;
        }

        public void Update(Client client)
        {
            _context.SaveChanges();
        }

        public void Delete(Client client)
        {
            _context.Remove(client);
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
