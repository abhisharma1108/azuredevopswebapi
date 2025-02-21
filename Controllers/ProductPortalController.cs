using Microsoft.AspNetCore.Mvc;
using azuredevopswebapi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace azuredevopswebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPortalController : ControllerBase
    {
        public static List<Product> products = new List<Product>()
        {
            new  Product(){productId=1001,productName="HP Laptop",productPrice=38440.99m,productDescription="HP 15s Intel Core I3 12th Gen Windows 11 Home Laptop" },
             new  Product(){productId=1002,productName="Asus VivoBook",productPrice=40400.99m,productDescription="Asus VivoBook Go 15 AMD Ryzen 5 7520U Laptop" },
              new  Product(){productId=1003,productName="Dell Inspiron",productPrice=39499.99m,productDescription="Dell Inspiron 15 3535 Notebook Laptop" }

        };

        // GET: api/<ProductPortalController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return products;
        }

        // GET api/<ProductPortalController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Product? product = products.FirstOrDefault(p => p.productId == id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST api/<ProductPortalController>
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            var maxProductId = products.Select(p => p.productId).DefaultIfEmpty(0).Max();

            if (maxProductId == 0)
            {
                product.productId = 1001;
            }
            else
            {
                product.productId = maxProductId + 1;

            }
            products.Add(product);
            return CreatedAtAction(nameof(Get), new { id = product.productId }, product);

        }

        // PUT api/<ProductPortalController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product updateProduct)
        {
            Product? product = products.FirstOrDefault(p => p.productId == id);
            if (updateProduct == null)
            {
                return BadRequest();
            }
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                product.productName = updateProduct.productName;
                product.productPrice = updateProduct.productPrice;
                product.productDescription = updateProduct.productDescription;


                return NoContent();

            }

        }

        // DELETE api/<ProductPortalController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product? product = products.FirstOrDefault(p => p.productId == id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                products.Remove(product);
                return NoContent();

            }

        }
    }
}
