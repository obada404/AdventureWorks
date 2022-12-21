using AdventureWorks.DTO;
using AdventureWorks.Interface;
using AdventureWorks.Models;
using AutoMapper;
using Azure.Identity;
using Microsoft.AspNetCore.Identity;

namespace AdventureWorks.Repository;

public class EfCustomerRepository:ICustomerRepository
{
    private readonly AdventureWorksLt2016Context _context;
    private readonly IMapper _mapper;

    public EfCustomerRepository(IMapper mapper) {
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

    public CustomerRequest? Find(int customerId)
    {
        var customer = _context.Customers.Find(customerId);
        if (customer != null)
        {
            var tmpMap =_mapper.Map<Customer, CustomerRequest>(customer);
            _context.ChangeTracker.Clear();
            return tmpMap;
        }

        return null;
    }

   

    public int Delete(int customerId)
    {
        try
        {
            var customer = _context.Customers.Find(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                return 1;
            }

            return 0;
        }
        catch (Exception e)
        {
            return 0;
        }
    }

    public int Update(CustomerRequestUpdate requestUpdate)
    {

        var customer = _context.Customers.Find(requestUpdate.CustomerId);
        if (customer != null)
        {
           var ee= _context.Entry(customer);
            ee.CurrentValues.SetValues(requestUpdate);
           _context.SaveChanges();
           return 1;
        }
    
        return 0; 
        
    }

    public CustomerRequestUpdate? Login(CustomerLogin customerLogin)
    {
        var result =_context.Customers.FirstOrDefault(x => x.CustomerId == customerLogin.CustomerId);
        var hasher = new PasswordHasher <Customer> ();
        if (result != null)
        {
            var passwordVerificationResult=   hasher.VerifyHashedPassword(result, result.PasswordHash, customerLogin.Password);
            if (passwordVerificationResult == PasswordVerificationResult.Success)
            {
                return _mapper.Map<Customer, CustomerRequestUpdate>(result);
            }else
            {
                throw new AuthenticationFailedException("password not match ");
            }
        }

        return null;
    }

    public AddressRequest? FindAddress(int customerId)
    {
        var address = _context.CustomerAddresses.FirstOrDefault(x => x.CustomerId == customerId);
      
        var addressLine= _context.Addresses.FirstOrDefault(x => address != null && x.AddressId == address.AddressId);
        if (addressLine != null) return _mapper.Map<Address, AddressRequest>(addressLine);
        return null;
    }
}

