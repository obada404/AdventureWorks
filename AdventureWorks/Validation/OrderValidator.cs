using AdventureWorks.DTO;
using FluentValidation;

namespace AdventureWorks.Validation;

public class OrderValidatorRequest : AbstractValidator<SalesOrderRequest> 
{

    public OrderValidatorRequest()
    {

        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.DueDate).NotEmpty();
        RuleFor(x => x.ModifiedDate).NotEmpty();
        RuleFor(x => x.OrderDate).NotEmpty();
        RuleFor(x => x.ShipDate).NotEmpty();




    }
    
}

public class OrderValidatorRequestUpdate : AbstractValidator<SalesOrderRequestUpdate> 
{

    public OrderValidatorRequestUpdate()
    {
        RuleFor(x => x.SalesOrderId).NotEmpty();
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.DueDate).NotEmpty();
        RuleFor(x => x.ModifiedDate).NotEmpty();
        RuleFor(x => x.OrderDate).NotEmpty();
        RuleFor(x => x.ShipDate).NotEmpty();




    }
    
}

public class PurchaseRequestValidator : AbstractValidator<PurchaseRequest>
{
    public PurchaseRequestValidator()
    {
        
    }
}