using Application.Models.Requests;
using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        Admin Create(AdminCreateRequest adminCreateRequest);
        void Delete(int id);
        List<AdminDto> GetAll();
        List<Admin> GetAllFullData();
        AdminDto GetById(int id);
        void Update(int id, AdminUpdateRequest adminUpdateRequest);
    }
}
