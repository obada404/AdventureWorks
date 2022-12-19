using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using AdventureWorks.DTO;
using AdventureWorks.Models;
using AdventureWorks.Service;
using AdventureWorks.Validation;
using Microsoft.IdentityModel.JsonWebTokens;
using MyApp;

namespace AdventureWorks.Controllers;

public class Product : Controller
{
    private  JwtManager _jwtManager;

    private readonly IProductService _prodectService;
    public Product(IProductService productService,IConfiguration config  )
    {
        _prodectService = productService;
        _jwtManager = new JwtManager(config["Jwt:Key"]);

    }
    [HttpPost]
    [Route("/product")]
    public ActionResult PostProduct( [FromBody] productRequest product)
    {
        var authResult  = Authorizationfunc();
        if (authResult != null)
            return authResult;
        ProductValidatorRequest validatorRequestUpdate = new ProductValidatorRequest();
      var resultval =  validatorRequestUpdate.Validate(product);
      if ( !resultval.IsValid)
      {
          var errors = resultval.Errors.Select(x => new { errors = x.ErrorMessage });
          return new JsonResult(errors){StatusCode = (int) HttpStatusCode.NotAcceptable};
      }

      var result = _prodectService.AddProduct(product);
        return   new JsonResult( result ) {StatusCode = (int) HttpStatusCode.OK};
    }

    [HttpGet]
    [Route("/product/{productId}")]
    public productRequest findProduct(int productId)
    {
        return _prodectService.FindProduct(productId);
    }

    [HttpDelete]
    [Route("/product/{productId}")]
    public ActionResult deleteProduct(int productId)
    {
        var authResult  = Authorizationfunc();
        if (authResult != null)
            return authResult;
        return new JsonResult( _prodectService.DeleteProduct(productId));
    }

    
    [HttpPatch]
    [Route("/product")]
    public ActionResult UpdateProduct( [FromBody] productRequestUpdate productRequest)
    {
        var authResult  = Authorizationfunc();
        if (authResult != null)
            return authResult;
        ProductValidatorRequestUpdate validatorRequestUpdate = new ProductValidatorRequestUpdate();
        var resultval =  validatorRequestUpdate.Validate(productRequest);
        if ( !resultval.IsValid)
        {
            var errors = resultval.Errors.Select(x => new { errors = x.ErrorMessage });
            return new JsonResult(errors);
        }
        return   new JsonResult( _prodectService.UpdateProduct(productRequest) ) {StatusCode = (int) HttpStatusCode.OK};
        
    }
    [HttpGet]
    [Route("/products")]
    public ActionResult GetAll()
    {
        var authResult  = Authorizationfunc();
        if (authResult != null)
            return authResult;
        return new JsonResult(_prodectService.GetAll());
    }
    [HttpGet]
    [Route("/ProductCategory")]
    public VGetAllCategory? GetProductCategory(int ProductId)
    {
        return _prodectService.GetProductCategory(ProductId);
    }
    [HttpGet]
    [Route("/ProductModel")]
    public VProductAndDescription? GetProductDescription(int ProductId)
    {
        return _prodectService.GetProductDescription(ProductId);
    }
    public ActionResult Authorizationfunc()
    {
        var principal = _jwtManager.VerifyJwt(Request.Headers["Authorization"]);
        if (principal == null)
        {
            return Unauthorized();
        }

        var userId = principal.FindFirst(JwtRegisteredClaimNames.Sid);
        var role = principal.FindFirst(ClaimTypes.Role).Value;
        if (role != "Admin")
        {
            return Forbid();
        }

        return null;
    }
}