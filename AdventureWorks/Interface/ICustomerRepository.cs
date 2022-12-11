using AdventureWorks.DTO;
using AdventureWorks.Models;

namespace AdventureWorks.Interface;

public interface ICustomerRepository
{
    public int Add(Customer customer);
    public CustomerRequestUpdate find(int CustomerId);
    public Customer find(Customer Customer);
    public int Delete(int  CustomerId);
    public int Update(Customer Customer);

    CustomerRequestUpdate login(CustomerLogin customerLogin);
}