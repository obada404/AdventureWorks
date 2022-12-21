using AdventureWorks.Models;
using AdventureWorks.DTO;
using AdventureWorks.Interface;
using AutoMapper;

namespace AdventureWorks.Service;
public interface ICustomerService
{
    int AddCustomer(CustomerSignup customer);
    CustomerRequest? FindCustomer(int customerId);
    CustomerRequestUpdate? LoginCustomer(CustomerLogin customerLogin);
    int UpdateCustomer(CustomerRequestUpdate customer);
    int DeleteCustomer(int customerId);
    AddressRequest? FindCustomerAddress(int customerId);
}

public class CustomerService:ICustomerService
{
    
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    public CustomerService(ICustomerRepository customerRepository,IMapper mapper ) {
        _mapper = mapper;
        _customerRepository = customerRepository;
    }
    public int AddCustomer(CustomerSignup customer)
    {
        var customerMap = _mapper.Map<CustomerSignup,Customer>(customer);
        var result =  _customerRepository.Add(customerMap);
        return result;
    }

    public CustomerRequest? FindCustomer(int customerId)
    {
        return _customerRepository.Find(customerId);
    }

    public CustomerRequestUpdate? LoginCustomer(CustomerLogin customerLogin)
    {
        
        return _customerRepository.Login(customerLogin);
    }

    public int UpdateCustomer(CustomerRequestUpdate customer)
    {
        return _customerRepository.Update(customer);
    }

    public int DeleteCustomer(int customerId)
    {
        return _customerRepository.Delete(customerId);
    }

    public AddressRequest? FindCustomerAddress(int customerId)
    {
       return _customerRepository.FindAddress(customerId);
    }
}