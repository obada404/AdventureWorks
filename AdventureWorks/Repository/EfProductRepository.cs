
using AdventureWorks.DTO;
using AdventureWorks.Interface;
using AdventureWorks.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Repository;

public class EfProductRepository:IproductRepository
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

    public productRequest find(int productId)
    {
         var tmp =_context.Products.Find(productId);

         return _mapper.Map<Product, productRequest>(tmp);
    }

    public Product find(Product product)
    {
        throw new NotImplementedException();
    }

    public int Delete(int productId)
    {
        try
        {
            var product = _context.Products.Find(productId);
            var ruslt = _context.Remove(product);
            _context.SaveChanges();
            return 1;
        }
        catch (ArgumentNullException e)
        {
            return 0;
        }
        
    
    }
    

    public int Update(Product product)
    {
        try
        {
            var result  =_context.Products.Update(product);
            result.Entity.ModifiedDate = DateTime.Now;
            _context.SaveChanges();
            return 1;
        }
        catch (Exception e )
        {
            throw e;
        }
       
        
    }


}