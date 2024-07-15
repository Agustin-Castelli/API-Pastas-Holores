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
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> _productRepository;

        public ProductService(IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public Product Create(ProductCreateRequest productCreateRequest)
        {
            var newObj = new Product();

            newObj.Name = productCreateRequest.Name;
            newObj.Price = productCreateRequest.Price;
            newObj.Description = productCreateRequest.Description;
            newObj.Category = productCreateRequest.Category;

            return _productRepository.Add(newObj);
        }

        public void Delete(int id)
        {
            var obj = _productRepository.GetById(id);

            if(obj == null) 
            {
                throw new NotFoundException(nameof(Client), id);
            }

            _productRepository.Delete(obj);
        }

        public void Update(int id, ProductUpdateRequest productUpdateRequest) 
        {
            var obj = _productRepository.GetById(id);

            if (obj == null)
            {
                throw new NotFoundException(nameof(Client), id);
            }

            if (productUpdateRequest.Name != string.Empty) obj.Name = productUpdateRequest.Name;
            if (productUpdateRequest.Price != 0) obj.Price = productUpdateRequest.Price;
            if (productUpdateRequest.Description != string.Empty) obj.Description = productUpdateRequest.Description;
            if (productUpdateRequest.Category != string.Empty) obj.Category = productUpdateRequest.Category;

            _productRepository.Update(obj);
        }

        public List<ProductDto> GetAll()
        {
            var list = _productRepository.GetAll();

            return ProductDto.CreateList(list);
        }

        public List<Product> GetAllFullData()
        {
            return _productRepository.GetAll();
        }

        public ProductDto GetById(int id)
        {
            var obj = _productRepository.GetById(id);

            var dto = ProductDto.Create(obj);

            return dto;
        }
    }
}
