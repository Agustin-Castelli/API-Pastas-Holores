using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class AdminDto
    {
        public int Id { get; set; }
        public string? Username { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;

        public static AdminDto Create(Admin admin)
        {
            var dto = new AdminDto();
            dto.Id = admin.Id;
            dto.Username = admin.Username;
            dto.Email = admin.Email;

            return dto;
        }

        public static List<AdminDto> CreateList(IEnumerable<Admin> admins)
        {
            List<AdminDto> listDto = [];

            foreach (var a in admins)
            {
                listDto.Add(Create(a));
            }

            return listDto;
        }
    }
}
