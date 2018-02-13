using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIService.Models;

namespace WebAPIService.Controllers
{
    public class ProductsController : ApiController
    {
        Product[] products = new Product[]
        {
            new Product { Id = 1 , Name = "Чай Ахмат" , Category = "Бакалея" , Price = 100 },
            new Product { Id = 2 , Name = "Кукла Барби" , Category = "Игрушки" , Price = 1000 },
            new Product { Id = 3 , Name = "Дрель Интерскол" , Category = "Инструменты" , Price = 3000 }

        };

        public IEnumerable<Product> GetAllProducts()    //      http://localhost:3660/api/products
        {
            return products;
        }

        public IHttpActionResult GetProduct(int id)      //         http://localhost:3660/api/products/id
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

    }
}
