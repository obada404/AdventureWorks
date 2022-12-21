using AdventureWorks.DTO;
using AdventureWorks.Models;

namespace AdventureWorks.Interface;

public interface ICustomerRepository
{
    public int Add(Customer customer);
    public CustomerRequest? Find(int customerId);
    public int Delete(int  customerId);
    public int Update(CustomerRequestUpdate requestUpdate);

    CustomerRequestUpdate? Login(CustomerLogin customerLogin);
    AddressRequest? FindAddress(int customerId);
}