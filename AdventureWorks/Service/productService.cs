using AdventureWorks;
using AdventureWorks.Models;
using AdventureWorks.DTO;
using AdventureWorks.Interface;
using AutoMapper;


public interface IprudectService
{
    int addPrudect(productRequest product);
    productRequest findProduct(int productId);
    int updateProduct(productRequestUpdate product);
    int deleteProduct(int productId);
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
        var productmap = _mapper.Map<productRequestUpdate,Product>(product);
        return _productRepository.Update(productmap);
    }

    public int deleteProduct(int productId)
    {
        return _productRepository.Delete(productId) ;
    }


}