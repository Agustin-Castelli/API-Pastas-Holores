using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IBaseRepository<Client> _clientRepository;

        public ClientService(IBaseRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Client Create(ClientCreateRequest clientCreateRequest)
        {
            var newObj = new Client();
            newObj.Username = clientCreateRequest.Username;
            newObj.Password = clientCreateRequest.Password;
            newObj.Email = clientCreateRequest.Email;
            newObj.FirstName = clientCreateRequest.FirstName;
            newObj.LastName = clientCreateRequest.LastName;
            newObj.Adress = clientCreateRequest.Adress;

            return _clientRepository.Add(newObj);
        }

        public void Update(int id, ClientUpdateRequest clientUpdateRequest) 
        {
            var obj = _clientRepository.GetById(id);

            if (obj == null)
            {
                throw new NotFoundException(nameof(Client), id);
            }

            if (clientUpdateRequest.Username != string.Empty) obj.Username = clientUpdateRequest.Username;
            if (clientUpdateRequest.Password != string.Empty) obj.Password = clientUpdateRequest.Password;
            if (clientUpdateRequest.Email != string.Empty) obj.Email = clientUpdateRequest.Email;
            if (clientUpdateRequest.FirstName != string.Empty) obj.FirstName = clientUpdateRequest.FirstName;
            if (clientUpdateRequest.LastName != string.Empty) obj.LastName = clientUpdateRequest.LastName;
            if (clientUpdateRequest.Adress != string.Empty) obj.Adress = clientUpdateRequest.Adress;

            _clientRepository.Update(obj);
        }

        public void Delete(int id)
        {
            var obj = _clientRepository.GetById(id);

            if (obj == null)
            {
                throw new NotFoundException(nameof(Client), id);
            }

            _clientRepository.Delete(obj);
        }

        public List<ClientDto> GetAll() 
        { 
            var list = _clientRepository.GetAll();

            return ClientDto.CreateList(list);
        }

        public List<Client> GetAllFullData()
        {
            return _clientRepository.GetAll();
        }

        public ClientDto GetById(int id) 
        {
            var obj = _clientRepository.GetById(id);

            var dto = ClientDto.Create(obj);

            return dto;
        }
    }
}
