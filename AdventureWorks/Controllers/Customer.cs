﻿using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Security;
using System.Security.Claims;
using System.Text;
using AdventureWorks.DTO;
using AdventureWorks.Models;
using AdventureWorks.Service;
using AdventureWorks.Validation;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyApp;

namespace AdventureWorks.Controllers;

public class Customer : Controller
{
    private readonly IConfiguration _config;
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;
    private  JwtManager _jwtManager;
    public Customer(ICustomerService customerService ,IConfiguration config ,IMapper mapper)
    {
        _customerService = customerService;
        _config = config;
        _mapper = mapper;
        _jwtManager = new JwtManager(_config["Jwt:Key"]);
    }
    [AllowAnonymous]
    [HttpPost]
    [Route("/Customer")]
    public ActionResult PostCustomer( [FromBody] CustomerSignup Customer)
    {
        var _CustomerValidatorSignup = new CustomerValidatorSignup();
        var resultval =  _CustomerValidatorSignup.Validate(Customer);
        if ( !resultval.IsValid)
        {
            var errors = resultval.Errors.Select(x => new { errors = x.ErrorMessage });
            return new JsonResult(errors){StatusCode = (int) HttpStatusCode.NotAcceptable};
        }

        var result = _customerService.addCustomer(Customer);
        return   new JsonResult( result ) {StatusCode = (int) HttpStatusCode.OK};
        
    }
    [AllowAnonymous]
    [HttpGet]
    [Route("/Customer")]
    public OkObjectResult loginCustomer([FromBody] CustomerLogin Customer)
    {
        var customer = _customerService.loginCustomer(Customer);;

        if (customer != null)
        {
            var jwt = _jwtManager.GenerateJwt(customer.CustomerId+"", customer.FirstName, customer.EmailAddress,"Customer");
            return Ok(jwt);
        }

        return null;
    }
    [HttpGet]
    [Route("/Customer/{CustomerId}")]
    public CustomerRequest findCustomer(int CustomerId)
    {
        return _customerService.findCustomer(CustomerId);
    }

    [HttpDelete]
    [Route("/Customer/{CustomerId}")]
    public int deleteCustomer(int CustomerId)
    {
        return _customerService.deleteCustomer(CustomerId);
    }

    //refactor CustomerRequestUpdate to CustomerRequest no need id with update we can get it from jwt
    [HttpPatch]
    //[Authorize(Roles ="Customer")]
    [Route("/Customer")]
    public ActionResult UpdateCustomer([FromBody] CustomerRequestUpdate customerRequestUpdate )
    {
        var principal = _jwtManager.VerifyJwt(Request.Headers["Authorization"]);
        if (principal == null)
        {
            return Unauthorized();
        }

        var userId = principal.FindFirst(JwtRegisteredClaimNames.Sid);
        var role = principal.FindFirst(ClaimTypes.Role).Value;
        var user = _customerService.findCustomer(int.Parse(userId.Value));
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
        return   new JsonResult( _customerService.updateCustomer(customerRequestUpdate,user.CustomerId) ) {StatusCode = (int) HttpStatusCode.OK};

    }
    // [HttpPost]
    // [Route("/Customer/createOrder")]
    // public ActionResult createOrder([FromBody] sales)
    //

    [HttpGet("/customerAddress")]
    public Address? GetAddress(int CustomerId)
    {
        return _customerService.findCustomerAddress(CustomerId);
    }
}