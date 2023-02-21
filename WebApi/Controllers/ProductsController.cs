using System.Collections.Generic;
using Dal;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private Dal.Dal dal = Dal.Dal.Instance;
        [HttpGet("getproducts")]
        public IActionResult GetProduct([FromQuery] int FieldToSort)
        {

            List<Product> products = dal.GetProducts(FieldToSort);
            return new ObjectResult(products);
        }
        [HttpPost("insertproduct")]
        public IActionResult InsertProduct([FromBody] Details details)
        {

            bool result = dal.InsertProduct(details.Name, details.Description);
            return new ObjectResult(result);
        }
        [HttpGet("deleteproduct")]
        public IActionResult DeleteProduct([FromQuery] int code)
        {

            bool result = dal.DeleteProduct(code);
            return new ObjectResult(result);
        }
        [HttpPost("editproduct")]
        public IActionResult EditProduct([FromBody] Details details)
        {

            bool result = dal.EditProduct((int)details.Code, details.Name, details.Description);
            return new ObjectResult(result);
        }
        [HttpPost("insertimg")]

        public IActionResult InsertIMG([FromBody] Img img)
        {

            bool result = dal.InsertIMG(img.Code, img.Router);
            return new ObjectResult(result);
        }

        [HttpPost("editimg")]

        public IActionResult EditIMG([FromBody] Img img)
        {

            bool result = dal.EditIMG(img.Code, img.Router);
            return new ObjectResult(result);
        }
        [HttpGet("deleteimg")]
        public IActionResult DeleteIMG([FromQuery] int code)
        {

            bool result = dal.DeleteIMG(code);
            return new ObjectResult(result);
        }
    }
    public class Details
    {
        public int? Code { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

    }
    public class Img
    {
        public int Code { get; set; }

        public string Router { get; set; }

    }
}
