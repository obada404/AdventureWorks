using AdventureWorks.DTO;
using AdventureWorks.Models;

namespace AdventureWorks.Interface;

public interface IproductRepository
{
    public int Add(Product product);
    public productRequest find(int productId);
    public Product find(Product product);
    public int Delete(int  productId);
    public int Update(Product product);

    // public List<UserOld> GetUsersGeneric(Func<UserOld,bool> pred);  
}