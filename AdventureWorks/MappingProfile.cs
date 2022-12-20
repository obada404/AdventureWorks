using AdventureWorks.DTO;
using AdventureWorks.Models;
using AutoMapper;

namespace AdventureWorks;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Product, productRequest>(); // One Way
        CreateMap<Product, productRequest>().ReverseMap(); // Reverse
        
        CreateMap<Product, productRequestUpdate>(); // One Way
        CreateMap<Product, productRequestUpdate>().ReverseMap(); // Reverse
        
        
        
        CreateMap<Customer, CustomerRequest>(); // One Way
        CreateMap<Customer, CustomerRequest>().ReverseMap(); // Reverse
        
        CreateMap<Customer, CustomerRequestUpdate>(); // One Way
        CreateMap<Customer, CustomerRequestUpdate>().ReverseMap(); // Reverse

        CreateMap<Customer, CustomerLogin>(); // One Way
        CreateMap<Customer, CustomerLogin>().ReverseMap(); // Reverse
        
        CreateMap<Customer, CustomerSignup>(); // One Way
        CreateMap<Customer, CustomerSignup>().ReverseMap(); // Reverse
        
        
        CreateMap<SalesOrderHeader, SalesOrderRequest>(); // One Way
        CreateMap<SalesOrderHeader, SalesOrderRequest>().ReverseMap(); // Reverse
        
            
        CreateMap<SalesOrderDetail, OrderDetailRequest>(); // One Way
        CreateMap<SalesOrderDetail, OrderDetailRequest>().ReverseMap(); // Reverse
        
        CreateMap<SalesOrderDetail, OrderDetailRequestmin>(); // One Way
        CreateMap<SalesOrderDetail, OrderDetailRequestmin>().ReverseMap(); // Reverse

        CreateMap<Admin, AdminSignup>(); // One Way
        CreateMap<Admin, AdminSignup>().ReverseMap(); // Reverse
        CreateMap<Admin, AdminRequestUpdate>(); // One Way
        CreateMap<Admin, AdminRequestUpdate>().ReverseMap(); // Reverse
        CreateMap<Admin, AdminLogin>(); // One Way
        CreateMap<Admin, AdminLogin>().ReverseMap(); // Reverse
        CreateMap<Admin, AdminRequest>(); // One Way
        CreateMap<Admin, AdminRequest>().ReverseMap(); // Reverse
        
        CreateMap<Address, AddressRequest>(); // One Way
        CreateMap<Address, AddressRequest>().ReverseMap(); // Reverse

    }
}