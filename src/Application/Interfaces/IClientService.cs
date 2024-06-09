using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClientService
    {
        Client Create(ClientCreateRequest clientCreateRequest);
        void Delete(int id);
        List<ClientDto> GetAll();
        List<Client> GetAllFullData();
        ClientDto GetById(int id);
        void Update(int id, ClientUpdateRequest clientUpdateRequest);
    }
}
