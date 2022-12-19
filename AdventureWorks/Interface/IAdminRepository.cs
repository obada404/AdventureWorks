using AdventureWorks.DTO;
using AdventureWorks.Models;

namespace AdventureWorks.Interface;

public interface IAdminRepository
{
    public int Add(Admin Admin);
    public AdminRequest find(int AdminId);
    public int Delete(int  AdminId);
    public int Update(AdminRequestUpdate adminRequestUpdate);
    AdminRequestUpdate login(AdminLogin AdminLogin);
}