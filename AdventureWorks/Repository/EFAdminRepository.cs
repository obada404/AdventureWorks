using AdventureWorks.DTO;
using AdventureWorks.Interface;
using AdventureWorks.Models;
using AutoMapper;
using Azure.Identity;
using Microsoft.AspNetCore.Identity;

namespace AdventureWorks.Repository;

public class EFAdminRepository:IAdminRepository
{
     private readonly AdventureWorksLt2016Context _context;
    private readonly IMapper _mapper;

    public EFAdminRepository(IMapper mapper) {
        _mapper = mapper;
    
        _context = new AdventureWorksLt2016Context() ;
    }
    public int Add(Admin Admin)
    {
        _context.Admins.Add(Admin);
        var hasher = new PasswordHasher <Admin> ();
        var hasedPassword = hasher.HashPassword(Admin, Admin.HashedPassword);
        Admin.HashedPassword = hasedPassword;
        _context.SaveChanges();
        return Admin.AdminId;
    }
    

    public AdminRequest find(int AdminId)
    {
        var tmp = _context.Admins.Find(AdminId);
        var tmpMap =_mapper.Map<Admin, AdminRequest>(tmp);
        _context.ChangeTracker.Clear();
        return tmpMap;

    }

    public Admin find(Admin Admin)
    {
        throw new NotImplementedException();
    }

    public int Delete(int AdminId)
    {
        try
        {
            var Admin = _context.Admins.Find(AdminId);
            var ruslt = _context.Remove(Admin);
            _context.SaveChanges();
            return 1;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public int Update(AdminRequestUpdate adminRequestUpdate)
    {
        var hasher = new PasswordHasher <Admin> ();
    
        var admin = _context.Admins.Find(adminRequestUpdate.AdminId);
        if (admin != null)
        {
           var ee= _context.Entry(admin);
             ee.CurrentValues.SetValues(adminRequestUpdate);
             var hasedPassword = hasher.HashPassword(admin, adminRequestUpdate.HashedPassword);
             admin.HashedPassword = hasedPassword;
             _context.SaveChanges();
           return 1;
        }
    
        return 0; 
        
    }

    public AdminRequestUpdate login(AdminLogin AdminLogin)
    {
        var result =_context.Admins.FirstOrDefault(x => x.AdminId == AdminLogin.AdminId);
        var hasher = new PasswordHasher <Admin> ();
     var Verify=   hasher.VerifyHashedPassword(result, result.HashedPassword, AdminLogin.HashedPassword);
     if (Verify == PasswordVerificationResult.Success)
     {
         return _mapper.Map<Admin, AdminRequestUpdate>(result);
     }else
     {
         throw new AuthenticationFailedException("password not match ");
         return null;
     }
         
    }
}