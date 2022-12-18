using AdventureWorks;
using AdventureWorks.Models;
using AdventureWorks.DTO;
using AdventureWorks.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


public interface IprudectService
{
    int addPrudect(productRequest product);
    productRequest findProduct(int productId);
    int updateProduct(productRequestUpdate product);
    int deleteProduct(int productId);
    List<Product> GetAll();
    VGetAllCategory? GetProductCategory(int productId);
    VProductAndDescription? GetProductDescription(int productId);
}

public class productService :IprudectService
{

    private readonly IproductRepository _productRepository;
    private readonly IMapper _mapper;

    public productService(IproductRepository productRepository,IMapper mapper) {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public  int addPrudect(productRequest product)
    {
        
        var productmap = _mapper.Map<productRequest,Product>(product);
        return  _productRepository.Add(productmap);

    }
    
    public productRequest findProduct(int productId)
    {
        return _productRepository.find(productId);
    }

    public int updateProduct(productRequestUpdate product)
    { 
        return _productRepository.Update(product);
    }

    public int deleteProduct(int productId)
    {
        return _productRepository.Delete(productId) ;
    }

    public List<Product> GetAll()
    {
       return _productRepository.GetAll();
    }

    public VGetAllCategory? GetProductCategory(int productId)
    {
       return _productRepository.getCategory(productId);
    }

    public VProductAndDescription? GetProductDescription(int productId)
    {
        return _productRepository.ProductDescription(productId);
    }
}