using AdventureWorks.DTO;
using AdventureWorks.Interface;
using AdventureWorks.Models;
using AutoMapper;

namespace AdventureWorks.Service;

public interface IAdminService
{
    int AddAdmin(AdminSignup adminSignup);
    AdminRequest FindAdmin(int adminId);
    AdminRequestUpdate LoginAdmin(AdminLogin adminLogin);
    int UpdateAdmin(AdminRequestUpdate admin, int adminId);
    int DeleteAdmin(int adminId);
}
public class AdminService:IAdminService
{
    
    private readonly IAdminRepository _adminRepository;
    private readonly IMapper _mapper;
    //  appUser.PasswordHash = hasher.HashPassword(appUser, "Default8!");
    public AdminService(IAdminRepository adminRepository,IMapper mapper ) {
        _mapper = mapper;
        _adminRepository = adminRepository;
    }

    public int AddAdmin(AdminSignup adminSignup)
    {
        var admin = _mapper.Map<AdminSignup, Admin>(adminSignup);
       return _adminRepository.Add(admin);
    }

    public AdminRequest FindAdmin(int adminId)
    {
        return _adminRepository.find(adminId);
    }

    public AdminRequestUpdate LoginAdmin(AdminLogin adminLogin)
    {
        return _adminRepository.login(adminLogin);
    }

    public int UpdateAdmin(AdminRequestUpdate admin, int adminId)
    {
        return _adminRepository.Update(admin);
    }

    public int DeleteAdmin(int adminId)
    {
        return _adminRepository.Delete(adminId);
    }
}

