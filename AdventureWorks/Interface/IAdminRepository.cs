using AdventureWorks.DTO;
using AdventureWorks.Models;

namespace AdventureWorks.Interface;

public interface IAdminRepository
{
    public int Add(Admin admin);
    public AdminRequest? Find(int adminId);
    public int Delete(int  adminId);
    public int Update(AdminRequestUpdate adminRequestUpdate);
    AdminRequestUpdate Login(AdminLogin adminLogin);
}