using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService; 

        public ClientController (IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ClientCreateRequest clientCreateRequest)
        {
            var newObj = _clientService.Create(clientCreateRequest);

            return Ok(newObj);
        }

        [HttpPut]
        public IActionResult Update([FromRoute] int id, [FromBody] ClientUpdateRequest clientUpdateRequest)
        {
            try
            {
                _clientService.Update(id, clientUpdateRequest);
                return NoContent();
            }
            
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _clientService.Delete(id);
                return NoContent();
            }

            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<Client>> GetAllFulData()
        {
            return _clientService.GetAllFullData();
        }

        [HttpGet("[action]")]
        public ActionResult<List<ClientDto>> GetAll()
        {
            return _clientService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<ClientDto> GetById([FromRoute] int id)
        {
            try
            {
                return _clientService.GetById(id);
            }

            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}