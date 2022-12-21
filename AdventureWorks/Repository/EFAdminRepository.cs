using AdventureWorks.DTO;
using AdventureWorks.Interface;
using AdventureWorks.Models;
using AutoMapper;
using Azure.Identity;
using Microsoft.AspNetCore.Identity;

namespace AdventureWorks.Repository;

public class EfAdminRepository:IAdminRepository
{
     private readonly AdventureWorksLt2016Context _context;
    private readonly IMapper _mapper;

    public EfAdminRepository(IMapper mapper) {
        _mapper = mapper;
    
        _context = new AdventureWorksLt2016Context() ;
    }
    public int Add(Admin admin)
    {
        _context.Admins.Add(admin);
        var hasher = new PasswordHasher <Admin> ();
        var hasedPassword = hasher.HashPassword(admin, admin.HashedPassword);
        admin.HashedPassword = hasedPassword;
        _context.SaveChanges();
        return admin.AdminId;
    }
    

    public AdminRequest? Find(int adminId)
    {
        var tmp = _context.Admins.Find(adminId);
        if (tmp != null)
        {
            var tmpMap =_mapper.Map<Admin, AdminRequest>(tmp);
            _context.ChangeTracker.Clear();
            return tmpMap;
        }

        return null;
    }
    

    public int Delete(int adminId)
    {
        try
        {
            var admin = _context.Admins.Find(adminId);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
            
            _context.SaveChanges();
            
        }
            return 1;
        }
        catch (Exception e)
        {
            return 0;
            
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

    public AdminRequestUpdate Login(AdminLogin adminLogin)
    {
        var result =_context.Admins.FirstOrDefault(x => x.AdminId == adminLogin.AdminId);
        var hasher = new PasswordHasher <Admin> ();
     var verify=   hasher.VerifyHashedPassword(result!, result!.HashedPassword, adminLogin.HashedPassword);
     if (verify == PasswordVerificationResult.Success)
     {
         return _mapper.Map<Admin, AdminRequestUpdate>(result);
     }else
     {
         throw new AuthenticationFailedException("password not match ");
     }
         
    }
}