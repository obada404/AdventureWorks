using AdventureWorks.Models;
namespace AdventureWorks.DTO;

public class PurchaseRequest
{
    public List<OrderDetailRequest> products { get; set; }
}

public class Orders
{
    public List<OrderDetailRequestmin> product { get; set; }
}