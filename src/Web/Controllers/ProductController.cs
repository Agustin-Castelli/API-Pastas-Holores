using Application.Interfaces;
using Application.Models.Requests;
using Application.Models;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductCreateRequest productCreateRequest)
        {
            var newObj = _productService.Create(productCreateRequest);

            return Ok(newObj);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ProductUpdateRequest productUpdateRequest)
        {
            try
            {
                _productService.Update(id, productUpdateRequest);
                return NoContent();
            }

            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _productService.Delete(id);
                return NoContent();
            }

            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public ActionResult<List<Product>> GetAllFulData()
        {
            return _productService.GetAllFullData();
        }

        [HttpGet("[action]")]
        public ActionResult<List<ProductDto>> GetAll()
        {
            return _productService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetById([FromRoute] int id)
        {
            try
            {
                return _productService.GetById(id);
            }

            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
