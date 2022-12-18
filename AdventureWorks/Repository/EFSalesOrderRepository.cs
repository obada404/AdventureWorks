using System.Security.Cryptography;
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
        
        var address = _context.CustomerAddresses.FirstOrDefault(x=> x.CustomerId == order.CustomerId);
        if(address ==null)
            throw new Exception("no address attached for this customer");

        var addressc = _context.Addresses.Find(address.AddressId);
        if(order.BillToAddress ==null ) 
            order.BillToAddress = addressc;
        if(order.ShipToAddress ==null ) 
            order.ShipToAddress = addressc;
        
        _context.SalesOrderHeaders.Add(order);
        _context.SaveChanges();
        return order.SalesOrderId;

    }

    public SalesOrderRequest find(int orderid)
    {
        var result =_context.SalesOrderHeaders.Find(orderid);
        return _mapper.Map<SalesOrderHeader, SalesOrderRequest>(result);
    }

    public SalesOrderHeader find(SalesOrderHeader order)
    {
        throw new NotImplementedException();
    }

    public int Delete(int orderid)
    {
        try
        {
            var product = _context.SalesOrderHeaders.Find(orderid);
            var ruslt = _context.SalesOrderHeaders.Remove(product);
            _context.SaveChanges();
            return 1;
        }
        catch (ArgumentNullException e)
        {
            throw e;

        }

    }

    public int Update(SalesOrderRequestUpdate orderRequest)
    {

        var SalesOrderHeader = _context.Products.Find(orderRequest.SalesOrderId);
        if (SalesOrderHeader != null)
        {
            var ee= _context.Entry(SalesOrderHeader);
            ee.CurrentValues.SetValues(orderRequest);
            _context.SaveChanges();
            return 1;
        }
    
        return 0; 

    }

    public int purchas(SalesOrderHeader salesOrderHeader, List<SalesOrderDetail> salesOrderDetails)
    {
        var salesOrderHeaderID =Add(salesOrderHeader);
        var salesOrderHeaderDB =_context.SalesOrderHeaders.Find(salesOrderHeaderID);
        foreach (var VARIABLE in salesOrderDetails)
        {
            VARIABLE.SalesOrder = salesOrderHeaderDB;
            var a = _context.SalesOrderDetails.Add(VARIABLE);
        }

  
           var result= _context.SaveChanges();
           return result;

    }

    public List<SalesOrderHeader> GetAllOrders(int customerId)
    {
        return _context.SalesOrderHeaders.Where(s => s.CustomerId == customerId).ToList();
    }

    public  dynamic  getallproductscustomer(int customerId)
    {
        var result  =
          ( from P in _context.Products
              join  OD in _context.SalesOrderDetails
                  on P.ProductId equals OD.ProductId
              join OH in _context.SalesOrderHeaders
                  on OD.SalesOrderId equals OH.SalesOrderId
              join CS in _context.Customers
                  on OH.CustomerId equals CS.CustomerId
              where CS.CustomerId.Equals(customerId)
              select new
              {
                  ID   =P.ProductId,
                  Name =P.Name,
                  Size =P.Size
                  
              });
      return  result.ToList();

    }

    public int addProductToOrder(int orderId, List<SalesOrderDetail> purchaseRequest)
    {
        foreach (var VARIABLE in purchaseRequest)
        {
            var detiles = _context.SalesOrderDetails.Add(VARIABLE);
            VARIABLE.SalesOrderId= orderId;
            var a = _context.SalesOrderDetails.Add(VARIABLE);
        }

  
        var result= _context.SaveChanges();
        return result;
    }
}