using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class ProductCreateRequest
    {
        public string? Name { get; set; } = string.Empty;
        public float Price { get; set; }
        public string? Description { get; set; } = string.Empty;
        public string? Category { get; set; } = string.Empty ;
    }
}
