using System.Net;
using AdventureWorks.Service;
using AdventureWorks.DTO;
using AdventureWorks.Filter;
using AdventureWorks.Models;
using AdventureWorks.Validation;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Controllers;

public class SalesOrder : Controller
{
    private readonly ISalesOrderService _salesOrderService;

    public SalesOrder(ISalesOrderService salesOrderService)
    {
        _salesOrderService = salesOrderService;
    }

    [HttpPost]
    [Route("/SalesOrder")]
    public ActionResult AddOrder([FromBody] SalesOrderRequest salesOrderRequest)
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
    [Route("/SalesOrder/{orderId}")]
    public SalesOrderRequest FindOrder(int orderId)
    {
        return _salesOrderService.FindOrder(orderId);
    }

    [HttpDelete]
    [Route("/Order/{orderId}")]
    public int DeleteProduct(int orderId)
    {
        return _salesOrderService.DeleteOrder(orderId);
    }


    [HttpPatch]
    [Route("/Order")]
    public ActionResult UpdateOrder([FromBody] SalesOrderRequestUpdate salesOrderRequest)
    {
        OrderValidatorRequestUpdate orderValidatorRequestUpdate = new OrderValidatorRequestUpdate();
        var validationResult = orderValidatorRequestUpdate.Validate(salesOrderRequest);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return new JsonResult(errors);
        }
        return new JsonResult(_salesOrderService.UpdateOrder(salesOrderRequest))
            { StatusCode = (int)HttpStatusCode.OK };

    }

    [HttpPost]
    [Route("/order/purchase")]
    public ActionResult Purchase([FromBody] PurchaseRequest request ,int orderHeaderId)
    {
        PurchaseRequestValidator purchaseRequestValidator = new PurchaseRequestValidator();
        var validationResult = purchaseRequestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return new JsonResult(errors);
        }
        var order = _salesOrderService.Purchase(orderHeaderId,request);
        
        return new JsonResult(order);
    }
    
    [HttpGet]
    [Route("/ordersHeader")]
    public List<SalesOrderHeader> GetOrdersForCustomer(int customerId)
    {
        return _salesOrderService.AllOrders(customerId);
    }
    
    [HttpGet]
    [Route("/orderDetails")]
    public List<SalesOrderDetail> GetDetailsForOrderHeaders(int orderHeaderId)
    {
        return _salesOrderService.DetailsForOrderHeaders(orderHeaderId);
    }
    
    [HttpGet]
    [TypeFilter(typeof(LogFilter))]
    [Route("/AllProductsForCustomer/{customerId}")]
    public dynamic GetAllProductsForCustomer(int customerId)
    {
        return _salesOrderService.AllProductsCustomer(customerId);
    }
    
    [HttpPost]
    [Route("/addProductToOrder/{orderId}")]
    public int AddProductToOrder([FromBody] Orders purchaseRequest, int orderId)
    {
        return _salesOrderService.AddProductToOrder(orderId,purchaseRequest);
    }


}
