using System.Net;
using Microsoft.AspNetCore.Mvc;
using AdventureWorks.DTO;
using AdventureWorks.Validation;

namespace AdventureWorks.Controllers;

public class Product : Controller
{
   
    private readonly IprudectService _prodectService;
    public Product(IprudectService prudectService )
    {
        _prodectService = prudectService;

    }
    [HttpPost]
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

      var result = _prodectService.addPrudect(product);
        return   new JsonResult( result ) {StatusCode = (int) HttpStatusCode.OK};
    }

    [HttpGet]
    [Route("/product/{productId}")]
    public productRequest findProduct(int productId)
    {
        return _prodectService.findProduct(productId);
    }

    [HttpDelete]
    [Route("/product/{productId}")]
    public int deleteProduct(int productId)
    {
        return _prodectService.deleteProduct(productId);
    }

    
    [HttpPatch]
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
        return   new JsonResult( _prodectService.updateProduct(productRequest) ) {StatusCode = (int) HttpStatusCode.OK};

    }

}