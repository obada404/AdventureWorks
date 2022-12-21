using AdventureWorks.DTO;
using AdventureWorks.Models;

namespace AdventureWorks.Interface;

public interface IProductRepository
{
    public int Add(Product product);
    public productRequest? Find(int productId);
    public int Delete(int  productId);
    public int Update(productRequestUpdate productRequest);
    List<Product> GetAll(int number);
    VGetAllCategory? GetCategory(int productId);
    VProductAndDescription? ProductDescription(int productId);
}