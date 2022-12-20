using AdventureWorks.DTO;
using FluentValidation;


namespace AdventureWorks.Validation;

public class ProductValidatorRequest : AbstractValidator<productRequest> 
{

    public ProductValidatorRequest()
    {

        RuleFor(x => x.Weight).InclusiveBetween(1, 99999);
        RuleFor(x => x.Size).MaximumLength(5);
        RuleFor(x => x.Name).MaximumLength(50);
        RuleFor(x => x.ProductNumber).MaximumLength(25);
        RuleFor(x => x.Color).MaximumLength(15);
        RuleFor(x => x.SellStartDate).NotEmpty();
        RuleFor(x => x.SellEndDate).NotEmpty();



    }
    
}
public class ProductValidatorRequestUpdate : AbstractValidator<productRequestUpdate>
{

    public ProductValidatorRequestUpdate()
    {
        RuleFor(x => x.ProductId).NotNull().NotEmpty();
        RuleFor(x => x.Weight).InclusiveBetween(1, 99999);
        RuleFor(x => x.Size).MaximumLength(5);
        RuleFor(x => x.Name).MaximumLength(50);
        RuleFor(x => x.ProductNumber).MaximumLength(25);
        RuleFor(x => x.Color).MaximumLength(15);
        RuleFor(x => x.SellStartDate).NotEmpty();
        RuleFor(x => x.SellEndDate).NotEmpty();



    }
    
}