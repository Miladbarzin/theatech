using DataLayer.DBModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Repository;
using WebApi.Controllers.Client;
using Microsoft.AspNetCore.Authorization;
using DataLayer.ViewModels;
using WebApi.Models.ViewModels.AccountViewModel;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : Controller
    {
        private IProductServices _productServices;
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        [Route("Product/list")]
        public async Task<IEnumerable<ProductViewModel>> GetProductList()
        {
            return await _productServices.GetAllProductsAsync();
        }

        [HttpPost]
        [Route("Product/add")]
        public async Task<IActionResult> AddProduct(ProductViewModel _productViewModel)
        {
            if (string.IsNullOrEmpty(_productViewModel.ProductName))
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "400", Message = "Product Name is required!" });
            else if (_productViewModel.Price <= 0)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "400", Message = "Product Price is invalid!" });
            else
            {
                var result = await _productServices.AddProductAsync(_productViewModel);

                if(result != null)
                    return Ok(new Response { Status = "200", Message = "Product added successfully!" });
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "Internal ServerError!" });
            }
        }

        [HttpPost]
        [Route("Product/delete")]
        public async Task<IActionResult> DeleteProduct(int productID)
        {
            var result = await _productServices.DeleteProductAsync(productID);
            if(result == DataLayer.Enum.ProductEnum.NotExist)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "400", Message = "Product Not Exist!" });
            else if (result == DataLayer.Enum.ProductEnum.NotAllow)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "400", Message = "Product is in a Cart!" });
            else if (result == DataLayer.Enum.ProductEnum.InternalServerError)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "Internal ServerError!" });
            else
                return Ok(new Response { Status = "200", Message = "Product deleted successfully!" });

        }
    }
}
