using AdventureWorks.DTO;
using AdventureWorks.Interface;
using AdventureWorks.Models;
using AutoMapper;
using Azure.Identity;
using Microsoft.AspNetCore.Identity;

namespace AdventureWorks.Repository;

public class EFCustomerRepository:ICustomerRepository
{
    private readonly AdventureWorksLt2016Context _context;
    private readonly IMapper _mapper;

    public EFCustomerRepository(IMapper mapper) {
        _mapper = mapper;
    
        _context = new AdventureWorksLt2016Context() ;
    }
    public int Add(Customer customer)
    {
        _context.Customers.Add(customer);
        var hasher = new PasswordHasher <Customer> ();
        var hasedPassword = hasher.HashPassword(customer, customer.PasswordHash);
        customer.PasswordHash = hasedPassword;
        customer.PasswordSalt = "1";
        _context.SaveChanges();
        return customer.CustomerId;
    }

    public CustomerRequest Find(int CustomerId)
    {
        var tmp = _context.Customers.Find(CustomerId);
        var tmpMap =_mapper.Map<Customer, CustomerRequest>(tmp);
        _context.ChangeTracker.Clear();
        return tmpMap;

    }

   

    public int Delete(int CustomerId)
    {
        try
        {
            var customer = _context.Customers.Find(CustomerId);
            var ruslt = _context.Remove(customer);
            _context.SaveChanges();
            return 1;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public int Update(CustomerRequestUpdate Customer)
    {

        var customeer = _context.Customers.Find(Customer.CustomerId);
        if (customeer != null)
        {
           var ee= _context.Entry(customeer);
             ee.CurrentValues.SetValues(Customer);
           _context.SaveChanges();
           return 1;
        }
    
        return 0; 
        
    }

    public CustomerRequestUpdate Login(CustomerLogin customerLogin)
    {
        var result =_context.Customers.FirstOrDefault(x => x.CustomerId == customerLogin.CustomerId);
        var hasher = new PasswordHasher <Customer> ();
     var Verify=   hasher.VerifyHashedPassword(result, result.PasswordHash, customerLogin.Password);
     if (Verify == PasswordVerificationResult.Success)
     {
         return _mapper.Map<Customer, CustomerRequestUpdate>(result);
     }else
     {
         throw new AuthenticationFailedException("password not match ");
         return null;
     }
         
    }

    public AddressRequest FindAddress(int customerId)
    {
        var address = _context.CustomerAddresses.FirstOrDefault(x => x.CustomerId == customerId);
      
        var addressline= _context.Addresses.FirstOrDefault(x => x.AddressId == address.AddressId);
        return _mapper.Map<Address, AddressRequest>(addressline);
    }
}

