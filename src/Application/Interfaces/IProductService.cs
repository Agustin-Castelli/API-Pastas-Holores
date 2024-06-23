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
    public interface IProductService
    {
        Product Create(ProductCreateRequest productCreateRequest);
        void Delete(int id);
        List<ProductDto> GetAll();
        List<Product> GetAllFullData();
        ProductDto GetById(int id);
        void Update(int id, ProductUpdateRequest productUpdateRequest);
    }
}
