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
        public async Task<IActionResult> GetAll()
        {
            IActionResult result = ApiHelper.CheckRequestResult(await _productService.GetAll());
            return result;
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            IActionResult result = ApiHelper.CheckRequestResult(await _productService.GetById(id));
            return result;
        }

        [HttpGet("getallbycategoryid")]
        public async Task<IActionResult> GetAllByCategoryId(int id)
        {
            IActionResult result = ApiHelper.CheckRequestResult(await _productService.GetAllByCategoryId(id));
            return result;
        }

        [HttpGet("getallbyunitprice")]
        public async Task<IActionResult> GetAllByUnitPrice(decimal min, decimal max)
        {
            IActionResult result = ApiHelper.CheckRequestResult(await _productService.GetAllByUnitPrice(min,max));
            return result;
        }

        #endregion

        #region Post

        [HttpPost("add")]
        public async Task<IActionResult> Add(Product product)
        {
            IActionResult result = ApiHelper.CheckRequestResult(await _productService.Add(product));
            return result;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(Product product)
        {
            IActionResult result = ApiHelper.CheckRequestResult(await _productService.Update(product));
            return result;
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(Product product)
        {
            IActionResult result = ApiHelper.CheckRequestResult(await _productService.Delete(product));
            return result;
        }

        [HttpPost("addtransactionaltest")]
        public async Task<IActionResult> AddTransactionalTest(Product[] products)
        {
            IActionResult result =
                ApiHelper.CheckRequestResult(await _productService.AddTransactionalTest(products[0], products[1]));
            return result;
        }

        #endregion

        #region GetDto

        [HttpGet("getproductdetails")]
        public async Task<IActionResult> GetProductDetails()
        {
            IActionResult result = ApiHelper.CheckRequestResult(await _productService.GetProductDetails());
            return result;
        }

        [HttpGet("getproductdetail")]
        public async Task<IActionResult> GetProductDetail(int id)
        {
            IActionResult result = ApiHelper.CheckRequestResult(await _productService.GetProductDetail(id));
            return result;
        }

        [HttpGet("getproductdetailsbystock")]
        public async Task<IActionResult> GetProductDetailsByStock(short stockLimit)
        {
            IActionResult result = ApiHelper.CheckRequestResult(await _productService.GetProductDetailsByStock(stockLimit));
            return result;
        }

        #endregion
    }
}