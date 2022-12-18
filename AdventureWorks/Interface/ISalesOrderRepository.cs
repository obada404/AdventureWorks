using AdventureWorks.DTO;
using AdventureWorks.Models;

namespace AdventureWorks.Interface;

public interface ISalesOrderRepository
{

    public int Add(SalesOrderHeader order);
    public SalesOrderRequest find(int orderid);
    public SalesOrderHeader find(SalesOrderHeader order);
    public int Delete(int  orderid);
    public int Update(SalesOrderRequestUpdate orderRequest);
    public int purchas(SalesOrderHeader salesOrderHeader, List<SalesOrderDetail> salesOrderDetails);

    List<SalesOrderHeader> GetAllOrders(int customerId);
    dynamic getallproductscustomer(int customerId);
    int addProductToOrder(int orderId,  List<SalesOrderDetail> purchaseRequest);
}