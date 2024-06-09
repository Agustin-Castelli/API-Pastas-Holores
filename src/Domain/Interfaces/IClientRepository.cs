using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IClientRepository
    {
        Client? GetById(int id);

        List<Client> GetAll();

        Client Add(Client client);
        Client AddClient(Client client);

        void Update(Client client);

        void Delete(Client client);

        void SaveChanges();
    }
}
