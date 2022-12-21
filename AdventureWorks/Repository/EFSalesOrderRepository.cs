using AdventureWorks.DTO;
using AdventureWorks.Interface;
using AdventureWorks.Models;
using AutoMapper;

namespace AdventureWorks.Repository;

public class SalesOrderRepository:ISalesOrderRepository
{
    private readonly AdventureWorksLt2016Context _context;
    private readonly IMapper _mapper;

    public SalesOrderRepository(IMapper mapper) {
        _mapper = mapper;
    
        _context = new AdventureWorksLt2016Context() ;
    }
    public int Add(SalesOrderHeader order)
    {

        var customer = _context.Customers.Find(order.CustomerId);
       if(customer ==null)
           throw new Exception("no customer with this Id");
        
        var customerAddress = _context.CustomerAddresses.FirstOrDefault(x=> x.CustomerId == order.CustomerId);
        if(customerAddress ==null)
            throw new Exception("no address attached for this customer");

        var address = _context.Addresses.Find(customerAddress.AddressId);
        order.BillToAddress ??= address;
        order.ShipToAddress ??= address;
        
        _context.SalesOrderHeaders.Add(order);
        _context.SaveChanges();
        return order.SalesOrderId;

    }

    public SalesOrderRequest Find(int orderId)
    {
        var result =_context.SalesOrderHeaders.Find(orderId);
        var orderRequest= _mapper.Map<SalesOrderHeader, SalesOrderRequest>(result!);
        return orderRequest;
    }
    

    public int Delete(int orderId)
    {
        try
        {
            var product = _context.SalesOrderHeaders.Find(orderId);
            _context.SalesOrderHeaders.Remove(product!);
            _context.SaveChanges();
            return 1;
        }
        catch (ArgumentNullException e)
        {
            return 0;

        }

    }

    public int Update(SalesOrderRequestUpdate orderRequest)
    {

        var salesOrderHeader = _context.Products.Find(orderRequest.SalesOrderId);
        if (salesOrderHeader != null)
        {
            var ee= _context.Entry(salesOrderHeader);
            ee.CurrentValues.SetValues(orderRequest);
            _context.SaveChanges();
            return 1;
        }
    
        return 0; 

    }

    public int Purchase(int  orderHeaderId, List<SalesOrderDetail> salesOrderDetails)
    {
        var salesOrderHeaderDb =_context.SalesOrderHeaders.Find(orderHeaderId);
        foreach (var orderDetail in salesOrderDetails)
        {
            orderDetail.SalesOrderId = orderHeaderId;
           _context.SalesOrderDetails.Add(orderDetail);
        }

        var result= _context.SaveChanges();
           return result;

    }

    public List<SalesOrderHeader> GetAllOrders(int customerId)
    {
        return _context.SalesOrderHeaders.Where(s => s.CustomerId == customerId).ToList();
    }

    public  dynamic  GetAllProductsCustomer(int customerId)
    {
        var result  =
          ( from p in _context.Products
              join  od in _context.SalesOrderDetails
                  on p.ProductId equals od.ProductId
              join oh in _context.SalesOrderHeaders
                  on od.SalesOrderId equals oh.SalesOrderId
              join cs in _context.Customers
                  on oh.CustomerId equals cs.CustomerId
              where cs.CustomerId.Equals(customerId)
              select new
              {
                  p.ProductId,
                  p.Name,
                  p.Size
                  
              });
      return  result.ToList();

    }

    public int AddProductToOrder(int orderId, List<SalesOrderDetail> purchaseRequest)
    {
        foreach (var orderDetail in purchaseRequest)
        {
             _context.SalesOrderDetails.Add(orderDetail);
            orderDetail.SalesOrderId= orderId;
            _context.SalesOrderDetails.Add(orderDetail);
        }

  
        var result= _context.SaveChanges();
        return result;
    }

    public List<SalesOrderDetail> GetOrderDetails(int orderHeaderId)
    {
        return _context.SalesOrderDetails.Where(x => x.SalesOrderId == orderHeaderId).ToList();
    }
}