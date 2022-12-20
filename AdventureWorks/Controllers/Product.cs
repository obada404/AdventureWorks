using System.Net;
using Microsoft.AspNetCore.Mvc;
using AdventureWorks.DTO;
using AdventureWorks.Filter;
using AdventureWorks.Models;
using AdventureWorks.Service;
using AdventureWorks.Validation;

namespace AdventureWorks.Controllers;

public class Product : Controller
{

    private readonly IProductService _productService;
    public Product(IProductService productService)
    {
        _productService = productService;

    }
    [HttpPost]
    [TypeFilter(typeof(LogFilter))]
    [Route("/product")]
    public ActionResult PostProduct( [FromBody] productRequest product)
    {
        ProductValidatorRequest validatorRequestUpdate = new ProductValidatorRequest();
      var resultval =  validatorRequestUpdate.Validate(product);
      if ( !resultval.IsValid)
      {
          var errors = resultval.Errors.Select(x => new { errors = x.ErrorMessage });
          return new JsonResult(errors){StatusCode = (int) HttpStatusCode.NotAcceptable};
      }

      var result = _productService.AddProduct(product);
        return   new JsonResult( result ) {StatusCode = (int) HttpStatusCode.OK};
    }

    [HttpGet]
    [Route("/product/{productId}")]
    public productRequest FindProduct(int productId)
    {
        return _productService.FindProduct(productId);
    }

    [HttpDelete]
    [TypeFilter(typeof(LogFilter))]
    [Route("/product/{productId}")]
    public ActionResult DeleteProduct(int productId)
    {
        return new JsonResult( _productService.DeleteProduct(productId) == 1?"product deleted " : " no product with this id" );
    }

    
    [HttpPatch]
    [TypeFilter(typeof(LogFilter))]
    [Route("/product")]
    public ActionResult UpdateProduct( [FromBody] productRequestUpdate productRequest)
    {
        
        ProductValidatorRequestUpdate validatorRequestUpdate = new ProductValidatorRequestUpdate();
        var resultval =  validatorRequestUpdate.Validate(productRequest);
        if ( !resultval.IsValid)
        {
            var errors = resultval.Errors.Select(x => new { errors = x.ErrorMessage });
            return new JsonResult(errors);
        }
        return   new JsonResult( _productService.UpdateProduct(productRequest) ) {StatusCode = (int) HttpStatusCode.OK};
        
    }
    [HttpGet]
    [Route("/products")]
    public ActionResult GetAll(int number)
    {
        
        return new JsonResult(_productService.GetAll(number));
    }
    [HttpGet]
    [Route("/ProductCategory")]
    public VGetAllCategory? GetProductCategory(int productId)
    {
        return _productService.GetProductCategory(productId);
    }
    [HttpGet]
    [Route("/ProductModel")]
    public VProductAndDescription? GetProductDescription(int productId)
    {
        return _productService.GetProductDescription(productId);
    }
  
}