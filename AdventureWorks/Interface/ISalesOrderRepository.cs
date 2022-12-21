using AdventureWorks.DTO;
using AdventureWorks.Models;

namespace AdventureWorks.Interface;

public interface ISalesOrderRepository
{

    public int Add(SalesOrderHeader order);
    public SalesOrderRequest Find(int orderId);
    public int Delete(int  orderId);
    public int Update(SalesOrderRequestUpdate orderRequest);
    public int Purchase(int OrderHeaderId, List<SalesOrderDetail> salesOrderDetails);

    List<SalesOrderHeader> GetAllOrders(int customerId);
    dynamic GetAllProductsCustomer(int customerId);
    int AddProductToOrder(int orderId,  List<SalesOrderDetail> purchaseRequest);
    List<SalesOrderDetail> GetOrderDetails(int orderHeaderId);
}