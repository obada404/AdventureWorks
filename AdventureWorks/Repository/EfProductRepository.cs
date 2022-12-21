using AdventureWorks.DTO;
using AdventureWorks.Interface;
using AdventureWorks.Models;
using AutoMapper;

namespace AdventureWorks.Repository;

public class EfProductRepository:IProductRepository
{
    private readonly AdventureWorksLt2016Context _context;
    private readonly IMapper _mapper;

    public EfProductRepository(IMapper mapper) {
        _mapper = mapper;
    
        _context = new AdventureWorksLt2016Context() ;
    }
    public  int Add(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return product.ProductId;
    }

    public productRequest? Find(int productId)
    {
        var product =_context.Products.Find(productId);

        if (product != null) return _mapper.Map<Product, productRequest>(product);
        return null;
    }
    
    public int Delete(int productId)
    {
        try
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                _context.Products.Remove(product);

                _context.SaveChanges();
                return 1;
            }
        }
        catch (ArgumentNullException e)
        {
            return 0;
        }

        return 0;
    }


    public int Update(productRequestUpdate productRequest)
    {
        var product = _context.Products.Find(productRequest.ProductId);
        if (product != null)
        {
            var ee= _context.Entry(product);
            ee.CurrentValues.SetValues(productRequest);
            _context.SaveChanges();
            return 1;
        }
    
        return 0; 
        
    }

    public List<Product> GetAll(int number)
    {
       return _context.Products.Take(number).ToList();
    }

    public VGetAllCategory? GetCategory(int productId)
    {
        var product = _context.Products.Find(productId);
        if(product == null)
            return null;
        return  _context.VGetAllCategories.FirstOrDefault(x => x.ProductCategoryId == product.ProductCategoryId);
    }

    public VProductAndDescription? ProductDescription(int productId)
    {
       return _context.VProductAndDescriptions.FirstOrDefault(x => x.ProductId == productId);
    }
}