using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult Create([FromBody] ClientCreateRequest clientCreateRequest)
        {
            var newObj = _clientService.Create(clientCreateRequest);

            return Ok(newObj);
        }


        [HttpPut("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
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

        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
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

        [HttpGet("[action]")]
        [Authorize(Policy = "RequireAdminRole")]
        public ActionResult<List<Client>> GetAllFulData()
        {
            return _clientService.GetAllFullData();
        }

        [HttpGet("[action]")]
        [Authorize(Policy = "RequireAdminRole")]
        public ActionResult<List<ClientDto>> GetAll()
        {
            return _clientService.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
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




        //              ↓ ↓ ↓ ↓  ENDPOINTS ESPECÍFICOS ↓ ↓ ↓ ↓ 




        [HttpGet("[action]/{clientId}")]
        public ActionResult<CartDto> GetCart([FromRoute] int clientId)
        {
            try
            {
                var cart = _clientService.GetCart(clientId);
                return cart;
                
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("[action]/{clientId}")]
        public IActionResult AddCartProducts([FromRoute] int clientId, [FromQuery] string productName)
        {
            try
            {
                _clientService.AddCartProducts(clientId, productName);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("[action]/{clientId}")]
        public IActionResult DeleteCartProducts([FromRoute] int clientId, [FromQuery] string productName)
        {
            try
            {
                _clientService.DeleteCartProducts(clientId, productName);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("[action]/{clientId}")]
        public IActionResult CompletePurchase([FromRoute] int clientId, [FromQuery] string paymentMethod)
        {
            try
            {
                _clientService.CompletePurchase(clientId, paymentMethod);
                return NoContent();
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}