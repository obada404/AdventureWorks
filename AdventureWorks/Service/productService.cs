using AdventureWorks.DTO;
using AdventureWorks.Interface;
using AdventureWorks.Models;
using AutoMapper;

namespace AdventureWorks.Service;

public interface IProductService
{
    int AddProduct(productRequest product);
    productRequest? FindProduct(int productId);
    int UpdateProduct(productRequestUpdate product);
    int DeleteProduct(int productId);
    List<Product> GetAll(int number);
    VGetAllCategory? GetProductCategory(int productId);
    VProductAndDescription? GetProductDescription(int productId);
}

public class ProductService :IProductService
{

    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository,IMapper mapper) {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public  int AddProduct(productRequest product)
    {
        
        var map = _mapper.Map<productRequest,Product>(product);
        return  _productRepository.Add(map);

    }
    
    public productRequest? FindProduct(int productId)
    {
        return _productRepository.Find(productId);
    }

    public int UpdateProduct(productRequestUpdate product)
    { 
        return _productRepository.Update(product);
    }

    public int DeleteProduct(int productId)
    {
        return _productRepository.Delete(productId) ;
    }

    public List<Product> GetAll(int number)
    {
        return _productRepository.GetAll(number);
    }

    public VGetAllCategory? GetProductCategory(int productId)
    {
        return _productRepository.GetCategory(productId);
    }

    public VProductAndDescription? GetProductDescription(int productId)
    {
        return _productRepository.ProductDescription(productId);
    }
}