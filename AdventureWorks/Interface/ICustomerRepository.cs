using AdventureWorks.DTO;
using AdventureWorks.Models;

namespace AdventureWorks.Interface;

public interface ICustomerRepository
{
    public int Add(Customer customer);
    public CustomerRequest Find(int customerId);
    public Customer Find(Customer customer);
    public int Delete(int  customerId);
    public int Update(CustomerRequestUpdate customer);

    CustomerRequestUpdate Login(CustomerLogin customerLogin);
    Address? FindAddress(int customerId);
}