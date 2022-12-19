using System.Net;
using System.Security.Claims;
using AdventureWorks.Service;
using AdventureWorks.DTO;
using AdventureWorks.Models;
using AdventureWorks.Validation;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using MyApp;

namespace AdventureWorks.Controllers;

public class SalesOrder : Controller
{
    private readonly IConfiguration _config;
    private readonly ISalesOrderService _salesOrderService;
    private readonly IMapper _mapper;
    private JwtManager _jwtManager;

    public SalesOrder(ISalesOrderService salesOrderService, IConfiguration config, IMapper mapper)
    {
        _salesOrderService = salesOrderService;
        _config = config;
        _mapper = mapper;
        _jwtManager = new JwtManager(_config["Jwt:Key"]);
    }

    [HttpPost]
    [Route("/SalesOrder")]
    public ActionResult Addorder([FromBody] SalesOrderRequest salesOrderRequest)
    {
        OrderValidatorRequest orderValidatorRequest = new OrderValidatorRequest();
        var resultval = orderValidatorRequest.Validate(salesOrderRequest);
        if (!resultval.IsValid)
        {
            var errors = resultval.Errors.Select(x => new { errors = x.ErrorMessage });
            return new JsonResult(errors);
        }

        return new JsonResult(_salesOrderService.AddOrder(salesOrderRequest))
            { StatusCode = (int)HttpStatusCode.OK };

    }
    

    [HttpGet]
    [Route("/SalesOrder/{OrderId}")]
    public SalesOrderRequest findOrder(int OrderId)
    {
        return _salesOrderService.FindOrder(OrderId);
    }

    [HttpDelete]
    [Route("/Order/{OrderId}")]
    public int deleteProduct(int productId)
    {
        return _salesOrderService.DeleteOrder(productId);
    }


    [HttpPatch]
    [Route("/Order")]
    public ActionResult Updateorder([FromBody] SalesOrderRequestUpdate salesOrderRequest)
    {
        OrderValidatorRequestUpdate orderValidatorRequestUpdate = new OrderValidatorRequestUpdate();
        var resultval = orderValidatorRequestUpdate.Validate(salesOrderRequest);
        if (!resultval.IsValid)
        {
            var errors = resultval.Errors.Select(x => new { errors = x.ErrorMessage });
            return new JsonResult(errors);
        }

        return new JsonResult(_salesOrderService.UpdateOrder(salesOrderRequest))
            { StatusCode = (int)HttpStatusCode.OK };

    }

    [HttpPost]
    [Route("/order/purchase/{customerId}")]
    public ActionResult purchase([FromBody] PurchaseRequestEnv<SalesOrderRequest,PurchaseRequest> Request )
    {
        PurchaseRequestValidator purchaseRequestValidator = new PurchaseRequestValidator();
        var resultval = purchaseRequestValidator.Validate(Request.PurchaseRequest);
        if (!resultval.IsValid)
        {
            var errors = resultval.Errors.Select(x => new { errors = x.ErrorMessage });
            return new JsonResult(errors);
        }

       var order = _salesOrderService.Purchase(Request);
        
        
        return new JsonResult(order);
    }
    
    [HttpGet]
    [Route("/order/{customerId}")]
    public List<SalesOrderHeader> GitOrdersForCustomer(int customerId)
    {
        return _salesOrderService.AllOrders(customerId);
    }
    [HttpGet]
    [Route("/allProductsforcustomer/{customerId}")]
    public dynamic GitallProductsforcustomer(int customerId)
    {
        var authResult  = Authorizationfunc();
        if (authResult != null)
            return authResult;
          return _salesOrderService.AllProductsCustomer(customerId);
    }
    [HttpGet]
    [Route("/addProductToOrder/{orderId}")]
    public int addProductToOrder([FromBody] Orders purchaseRequest, int orderId)
    {
        return _salesOrderService.AddProductToOrder(orderId,purchaseRequest);
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
