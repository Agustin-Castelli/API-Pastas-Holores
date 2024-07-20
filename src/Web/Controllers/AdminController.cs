using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] AdminCreateRequest adminCreateRequest)
        {
            var newObj = _adminService.Create(adminCreateRequest);

            return Ok(newObj);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute]int id, [FromBody]AdminUpdateRequest adminUpdateRequest)
        {
            try
            {
                _adminService.Update(id, adminUpdateRequest);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            try
            {
                _adminService.Delete(id);
                return NoContent();
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public ActionResult<List<AdminDto>> GetAll()
        {
            return _adminService.GetAll();
        }

        [HttpGet("[action]")]
        public ActionResult<List<Admin>> GetAllFullData() 
        { 
            return _adminService.GetAllFullData();
        }

        [HttpGet("{id}")]
        public ActionResult<AdminDto> GetById([FromRoute] int id)
        {
            try
            {
                return _adminService.GetById(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
