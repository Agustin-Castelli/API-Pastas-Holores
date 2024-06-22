using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string? Username { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Adress { get; set; } = string.Empty;

        public static ClientDto Create(Client client)
        {
            var dto = new ClientDto();
            dto.Id = client.Id;
            dto.Username = client.Username;
            dto.FirstName = client.FirstName;
            dto.LastName = client.LastName;
            dto.Adress = client.Adress;

            return dto;
        }

        public static List<ClientDto> CreateList(IEnumerable<Client> clients)
        {
            List<ClientDto> listDto = [];

            foreach (var c in clients) 
            {
                listDto.Add(Create(c));
            }

            return listDto;
        }
    }
}