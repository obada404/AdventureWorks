﻿using AdventureWorks.Controllers;
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
    dynamic AllProductscustomer(int customerId);
    int addProductToOrder(int orderId, Orders orders);
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
       return _salesOrderRepository.find(orderId);
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
        var listofproducts = new List<SalesOrderDetail>();
        foreach (var VARIABLE in request.PurchaseRequest.product)
        {
            listofproducts.Add(_mapper.Map<OrderDetailRequest,SalesOrderDetail>(VARIABLE));
        }

       return _salesOrderRepository.purchas(_mapper.Map<SalesOrderRequest, SalesOrderHeader>(request.salesorder),
            listofproducts);
    }

    public List<SalesOrderHeader> AllOrders(int customerId)
    {
       return _salesOrderRepository.GetAllOrders(customerId);
    }

    public  dynamic AllProductscustomer(int customerId)
    {
         return _salesOrderRepository.getallproductscustomer(customerId);
    }

    public int addProductToOrder(int orderId, Orders orders)
    {
        var listofproducts = new List<SalesOrderDetail>();
        foreach (var VARIABLE in orders.product)
        {
            listofproducts.Add(_mapper.Map<OrderDetailRequestmin,SalesOrderDetail>(VARIABLE));
        }
        return _salesOrderRepository.addProductToOrder(orderId, listofproducts);
    }
}