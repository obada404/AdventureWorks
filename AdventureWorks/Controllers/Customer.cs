using System.Net;
using AdventureWorks.DTO;
using AdventureWorks.Filter;
using AdventureWorks.Models;
using AdventureWorks.Service;
using AdventureWorks.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp;

namespace AdventureWorks.Controllers;

public class Customer : Controller
{
    private readonly ICustomerService _customerService;
    private readonly JwtManager _jwtManager;
    public Customer(ICustomerService customerService ,IConfiguration config )
    {
        _customerService = customerService;
        _jwtManager = new JwtManager(config["Jwt:Key"]);
    }
    [AllowAnonymous]
    [HttpPost]
    [Route("/Customer")]
    public ActionResult PostCustomer( [FromBody] CustomerSignup customer)
    {
        var customerValidatorSignup = new CustomerValidatorSignup();
        var validationResult =  customerValidatorSignup.Validate(customer);
        if ( !validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return new JsonResult(errors){StatusCode = (int) HttpStatusCode.NotAcceptable};
        }

        var result = _customerService.AddCustomer(customer);
        return   new JsonResult( result ) {StatusCode = (int) HttpStatusCode.OK};
        
    }
    [AllowAnonymous]
    [HttpGet]
    [Route("/Customerlogin")]
    public OkObjectResult LoginCustomer([FromBody] CustomerLogin customerLogin)
    {
        var customer = _customerService.LoginCustomer(customerLogin);

        if (customer != null || customer?.EmailAddress != null)
        {
            var jwt = _jwtManager.GenerateJwt(customer.CustomerId+"", customer.FirstName, customer.EmailAddress!,"Customer");
            return Ok(jwt);
        }

        return null;
    }
    [HttpGet]
    [TypeFilter(typeof(LogFilter))]
    [Route("/Customer/{customerId}")]
    public ActionResult FindCustomer(int customerId)
    {
        return new JsonResult(_customerService.FindCustomer(customerId));
    }

    [HttpDelete]
    [TypeFilter(typeof(LogFilter))]
    [Route("/Customer/{customerId}")]
    public ActionResult DeleteCustomer(int customerId)
    {
       
        return new JsonResult(_customerService.DeleteCustomer(customerId));
    }

    [HttpPatch]
    [TypeFilter(typeof(LogFilter))]
    [Route("/Customer")]
    public ActionResult UpdateCustomer([FromBody] CustomerRequestUpdate customerRequestUpdate )
    {
     
        var role =Request.Headers["role"] ;
        var user = _customerService.FindCustomer(int.Parse(Request.Headers["id"]!));
        if (user == null ||role != "Customer")
        {
            return Forbid();
        }
        var validatorRequestUpdate = new CustomerValidatorRequestUpdate();
        var resultval =  validatorRequestUpdate.Validate(customerRequestUpdate);
        if ( !resultval.IsValid)
        {
            var errors = resultval.Errors.Select(x => new { errors = x.ErrorMessage });
            return new JsonResult(errors);
        }
        return   new JsonResult( _customerService.UpdateCustomer(customerRequestUpdate) ) {StatusCode = (int) HttpStatusCode.OK};

    }
    [TypeFilter(typeof(LogFilter))]
    [HttpGet("/customerAddress")]
    public AddressRequest GetAddress(int customerId)
    {
        return _customerService.FindCustomerAddress(customerId);
    }
}