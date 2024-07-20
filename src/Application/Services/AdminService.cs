using Application.Models.Requests;
using Application.Models;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;    //  PREGUNTAR LUEGO SI A LA HORA DE IMPLEMENTAR METODOS ESPECÍFICOS PARA CADA CLASE DEBO HACER LA INYECCION DEL REPO PARTICULAR DE CADA CLASE.
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public Admin Create(AdminCreateRequest adminCreateRequest)
        {
            var newObj = new Admin();
            
            newObj.Username = adminCreateRequest.Username;
            newObj.Password = adminCreateRequest.Password;
            newObj.Email = adminCreateRequest.Email;

            return _adminRepository.Add(newObj);
        }

        public void Update(int id, AdminUpdateRequest adminUpdateRequest)
        {
            var obj = _adminRepository.GetById(id);

            if (obj == null)
            {
                throw new NotFoundException(nameof(Admin), id);
            }

            if (adminUpdateRequest.Username != string.Empty) obj.Username = adminUpdateRequest.Username;
            if (adminUpdateRequest.Password != string.Empty) obj.Password = adminUpdateRequest.Password;
            if (adminUpdateRequest.Email != string.Empty) obj.Email = adminUpdateRequest.Email;


            _adminRepository.Update(obj);
        }

        public void Delete(int id)
        {
            var obj = _adminRepository.GetById(id);

            if (obj == null)
            {
                throw new NotFoundException(nameof(Client), id);
            }

            _adminRepository.Delete(obj);
        }

        public List<AdminDto> GetAll()
        {
            var list = _adminRepository.GetAll();

            return AdminDto.CreateList(list);
        }

        public List<Admin> GetAllFullData()
        {
            return _adminRepository.GetAll();
        }

        public AdminDto GetById(int id)
        {
            var obj = _adminRepository.GetById(id);

            var dto = AdminDto.Create(obj);

            return dto;
        }
    }
}
