using System.Net;
using AdventureWorks.DTO;
using AdventureWorks.Filter;
using AdventureWorks.Service;
using AdventureWorks.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Controllers;

public class Admin : Controller
{
    private readonly IAdminService _adminService;
    private readonly JwtManager _jwtManager;

    public Admin(IAdminService adminService ,IConfiguration config)
    {
        _adminService = adminService;
        _jwtManager = new JwtManager(config["Jwt:Key"]);
    }
    
    
    
    [AllowAnonymous]
    [HttpPost]
    [Route("/Admin")]
    public ActionResult PostAdmin( [FromBody] AdminSignup admin)
    {
        var adminValidatorSignup = new AdminValidatorSignup();
        var validationResult =  adminValidatorSignup.Validate(admin);
        if ( !validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return new JsonResult(errors){StatusCode = (int) HttpStatusCode.NotAcceptable};
        }

        var result = _adminService.AddAdmin(admin);
        return   new JsonResult( result ) {StatusCode = (int) HttpStatusCode.OK};
        
    }
    [AllowAnonymous]
    [HttpGet]
    [Route("/Admin/login")]
    public OkObjectResult? LoginAdmin([FromBody] AdminLogin adminLogin)
    {
        var admin = _adminService.LoginAdmin(adminLogin);

        if (admin != null)
        {
            var jwt = _jwtManager.GenerateJwt(admin.AdminId+"", admin.AdminName!, admin.Email!,"Admin");
            return Ok(new Token(jwt));
        }

        return null;
    }
    
    [AllowAnonymous]
    [HttpGet]
    [TypeFilter(typeof(LogFilter))]
    [Route("/Admin/currentAdmin")]
    public ActionResult CurrentAdmin()
    {
        
        var user = _adminService.FindAdmin(int.Parse(Request.Headers["id"]!));
        return new JsonResult(user);
    }
    [HttpGet]
    [Route("/Admin/{adminId}")]
    public AdminRequest? FindAdmin(int adminId)
    {
        return _adminService.FindAdmin(adminId);
    }

    [HttpDelete]
    [Route("/Admin/{adminId}")]
    public int DeleteAdmin(int adminId)
    {
        return _adminService.DeleteAdmin(adminId);
    }

    [HttpPatch]
    [TypeFilter(typeof(LogFilter))]
    [Route("/Admin")]
    public ActionResult UpdateAdmin([FromBody] AdminRequestUpdate adminRequestUpdate )
    {
       
        var user = _adminService.FindAdmin(int.Parse(Request.Headers["id"]!));
        if (user == null )
        {
            return Forbid();
        }
        var validatorRequestUpdate = new AdminValidatorRequestUpdate();
        var resultval =  validatorRequestUpdate.Validate(adminRequestUpdate);
        if ( !resultval.IsValid)
        {
            var errors = resultval.Errors.Select(x => new { errors = x.ErrorMessage });
            return new JsonResult(errors);
        }
        return   new JsonResult( _adminService.UpdateAdmin(adminRequestUpdate,user.AdminId) ) {StatusCode = (int) HttpStatusCode.OK};

    }
}

