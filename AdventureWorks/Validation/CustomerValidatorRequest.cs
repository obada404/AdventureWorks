using AdventureWorks.DTO;
using FluentValidation;


namespace AdventureWorks.Validation;

public class CustomerValidatorRequest : AbstractValidator<CustomerRequest> 
{

    public CustomerValidatorRequest()
    {

        RuleFor(x => x.Title).MaximumLength(8);
        RuleFor(x => x.FirstName).MaximumLength(50);
        RuleFor(x => x.LastName).MaximumLength(50);
        RuleFor(x => x.MiddleName).MaximumLength(50);
        RuleFor(x => x.Suffix).MaximumLength(10);
        RuleFor(x => x.CompanyName).MaximumLength(128);
        RuleFor(x => x.EmailAddress).EmailAddress().MaximumLength(50);



    }
    
}
public class CustomerValidatorRequestUpdate : AbstractValidator<CustomerRequestUpdate>
{

    public CustomerValidatorRequestUpdate()
    {
        RuleFor(x => x.CustomerId).NotNull().NotEmpty();
        RuleFor(x => x.Title).MaximumLength(8);
        RuleFor(x => x.FirstName).MaximumLength(50);
        RuleFor(x => x.LastName).MaximumLength(50);
        RuleFor(x => x.MiddleName).MaximumLength(50);
        RuleFor(x => x.Suffix).MaximumLength(10);
        RuleFor(x => x.CompanyName).MaximumLength(128);
        RuleFor(x => x.EmailAddress).EmailAddress().MaximumLength(50);




    }
    
}
public class CustomerValidatorSignup : AbstractValidator<CustomerSignup>
{

    public CustomerValidatorSignup()
    {
        RuleFor(x => x.Title).MaximumLength(8);
        RuleFor(x => x.FirstName).MaximumLength(50);
        RuleFor(x => x.LastName).MaximumLength(50);
        RuleFor(x => x.MiddleName).MaximumLength(50);
        RuleFor(x => x.Suffix).MaximumLength(10);
        RuleFor(x => x.CompanyName).MaximumLength(128);
        RuleFor(x => x.EmailAddress).EmailAddress().MaximumLength(50);
        RuleFor(x => x.PasswordHash).NotEmpty().MaximumLength(128);




    }
    
}
public class CustomerValidatorLogin : AbstractValidator<CustomerLogin>
{

    public CustomerValidatorLogin()
    {
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        RuleFor(x => x.CustomerId).NotEmpty();

    }
    
}