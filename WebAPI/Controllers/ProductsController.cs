using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.WebAPI;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        #region Get
        
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            IActionResult result = ApiHelper.CheckRequestResult(_productService.GetAll());
            return result;
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            IActionResult result = ApiHelper.CheckRequestResult(_productService.GetById(id));
            return result;
        }

        [HttpGet("getallbycategoryid")]
        public IActionResult GetAllByCategoryId(int id)
        {
            IActionResult result = ApiHelper.CheckRequestResult(_productService.GetAllByCategoryId(id));
            return result;
        }

        [HttpGet("getallbyunitprice")]
        public IActionResult GetAllByUnitPrice(decimal min, decimal max)
        {
            IActionResult result = ApiHelper.CheckRequestResult(_productService.GetAllByUnitPrice(min,max));
            return result;
        }

        #endregion

        #region Post

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            IActionResult result = ApiHelper.CheckRequestResult(_productService.Add(product));
            return result;
        }

        [HttpPost("update")]
        public IActionResult Update(Product product)
        {
            IActionResult result = ApiHelper.CheckRequestResult(_productService.Update(product));
            return result;
        }

        [HttpPost("delete")]
        public IActionResult Delete(Product product)
        {
            IActionResult result = ApiHelper.CheckRequestResult(_productService.Delete(product));
            return result;
        }

        [HttpPost("addtransactionaltest")]
        public IActionResult AddTransactionalTest(Product[] products)
        {
            IActionResult result =
                ApiHelper.CheckRequestResult(_productService.AddTransactionalTest(products[0], products[1]));
            return result;
        }

        #endregion

        #region GetDto

        [HttpGet("getproductdetails")]
        public IActionResult GetProductDetails()
        {
            IActionResult result = ApiHelper.CheckRequestResult(_productService.GetProductDetails());
            return result;
        }

        [HttpGet("getproductdetail")]
        public IActionResult GetProductDetail(int id)
        {
            IActionResult result = ApiHelper.CheckRequestResult(_productService.GetProductDetail(id));
            return result;
        }

        [HttpGet("getproductdetailsbystock")]
        public IActionResult GetProductDetailsByStock(short stockLimit)
        {
            IActionResult result = ApiHelper.CheckRequestResult(_productService.GetProductDetailsByStock(stockLimit));
            return result;
        }

        #endregion
    }
}