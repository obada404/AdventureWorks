using AdventureWorks.DTO;
using AdventureWorks.Models;

namespace AdventureWorks.Interface;

public interface ICustomerRepository
{
    public int Add(Customer customer);
    public CustomerRequest find(int CustomerId);
    public Customer find(Customer Customer);
    public int Delete(int  CustomerId);
    public int Update(CustomerRequestUpdate Customer);

    CustomerRequestUpdate login(CustomerLogin customerLogin);
    Address? findAddress(int customerId);
}