using AdventureWorks.DTO;
using AdventureWorks.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Interface;

public interface IproductRepository
{
    public int Add(Product product);
    public productRequest find(int productId);
    public Product find(Product product);
    public int Delete(int  productId);
    public int Update(productRequestUpdate productRequest);

    // public List<UserOld> GetUsersGeneric(Func<UserOld,bool> pred);  
    List<Product> GetAll();
    VGetAllCategory? getCategory(int productId);
    VProductAndDescription? ProductDescription(int productId);
}