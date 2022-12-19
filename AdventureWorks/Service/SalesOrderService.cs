using AdventureWorks.DTO;
using AdventureWorks.Interface;
using AdventureWorks.Models;
using AutoMapper;

namespace AdventureWorks.Service;

public interface ISalesOrderService
{
    int AddOrder(SalesOrderRequest order);
    SalesOrderRequest FindOrder(int orderId);
    int UpdateOrder(SalesOrderRequestUpdate order);
    int DeleteOrder(int orderId);
    int Purchase(PurchaseRequestEnv<SalesOrderRequest, PurchaseRequest> request);
    List<SalesOrderHeader> AllOrders(int customerId);
    dynamic AllProductsCustomer(int customerId);
    int AddProductToOrder(int orderId, Orders orders);
}

public class SalesOrderService : ISalesOrderService
{
    
    private readonly ISalesOrderRepository _salesOrderRepository;
    private readonly IMapper _mapper;

    public SalesOrderService(ISalesOrderRepository salesOrderRepository,IMapper mapper) {
        _mapper = mapper;
        _salesOrderRepository = salesOrderRepository;
    }
    public int AddOrder(SalesOrderRequest order)
    {
        var orderMap = _mapper.Map<SalesOrderRequest,SalesOrderHeader>(order);

       return _salesOrderRepository.Add(orderMap);
    }

    public SalesOrderRequest FindOrder(int orderId)
    {
       return _salesOrderRepository.Find(orderId);
    }

    public int UpdateOrder(SalesOrderRequestUpdate order)
    {
       return _salesOrderRepository.Update(order);
    }

    public int DeleteOrder(int orderId)
    {
       return _salesOrderRepository.Delete(orderId);
    }

    public int Purchase(PurchaseRequestEnv<SalesOrderRequest, PurchaseRequest> request)
    {
        var listOfOrder = new List<SalesOrderDetail>();
        foreach (var orderRequest in request.PurchaseRequest.product)
        {
            listOfOrder.Add(_mapper.Map<OrderDetailRequest,SalesOrderDetail>(orderRequest));
        }

       
        return _salesOrderRepository.Purchase(_mapper.Map<SalesOrderRequest, SalesOrderHeader>(request.salesorder),
            listOfOrder);
    }

    public List<SalesOrderHeader> AllOrders(int customerId)
    {
       return _salesOrderRepository.GetAllOrders(customerId);
    }

    public  dynamic AllProductsCustomer(int customerId)
    {
         return _salesOrderRepository.GetAllProductsCustomer(customerId);
    }

    public int AddProductToOrder(int orderId, Orders orders)
    {
        var listOfOrder = new List<SalesOrderDetail>();
        foreach (var orderRequestMin in orders.product)
        {
            listOfOrder.Add(_mapper.Map<OrderDetailRequestmin,SalesOrderDetail>(orderRequestMin));
        }
        return _salesOrderRepository.AddProductToOrder(orderId, listOfOrder);
    }
}