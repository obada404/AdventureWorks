using AdventureWorks.Models;
using AdventureWorks.DTO;
using AdventureWorks.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AdventureWorks.Service;
public interface ICustomerService
{
    int addCustomer(CustomerSignup customer);
    CustomerRequest findCustomer(int CustomerId);
    CustomerRequestUpdate loginCustomer(CustomerLogin customerLogin);
    int updateCustomer(CustomerRequestUpdate Customer, int customerId);
    int deleteCustomer(int CustomerId);
    Address? findCustomerAddress(int customerId);
}

public class CustomerService:ICustomerService
{
    
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
  //  appUser.PasswordHash = hasher.HashPassword(appUser, "Default8!");
    public CustomerService(ICustomerRepository customerRepository,IMapper mapper  ,Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Identity.PasswordHasherOptions>? optionsAccessor = default     ) {
        _mapper = mapper;
        _customerRepository = customerRepository;
    }
    public int addCustomer(CustomerSignup customer)
    {

        var customermap = _mapper.Map<CustomerSignup,Customer>(customer);
        var result =  _customerRepository.Add(customermap);
        return result;
    }

    public CustomerRequest findCustomer(int CustomerId)
    {
        return _customerRepository.find(CustomerId);
    }

    public CustomerRequestUpdate loginCustomer(CustomerLogin customerLogin)
    {
        
        return _customerRepository.login(customerLogin);
    }

    public int updateCustomer(CustomerRequestUpdate Customer, int customerId)
    {
        return _customerRepository.Update(Customer);
    }

    public int deleteCustomer(int CustomerId)
    {
        return _customerRepository.Delete(CustomerId);
    }

    public Address? findCustomerAddress(int customerId)
    {
       return _customerRepository.findAddress(customerId);
    }
}