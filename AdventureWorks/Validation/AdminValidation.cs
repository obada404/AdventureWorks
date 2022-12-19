using AdventureWorks.DTO;
using FluentValidation;

namespace AdventureWorks.Validation;

public class AdminValidation
{
    
}

public class AdminValidatorSignup : AbstractValidator<AdminSignup>
{
    public AdminValidatorSignup()
    {
        RuleFor(x => x.Email).EmailAddress();

        RuleFor(x => x.Role).NotEmpty();
        RuleFor(x => x.AdminName).NotEmpty();
        RuleFor(x => x.HashedPassword).NotEmpty();

    }
    
}
public class AdminValidatorRequestUpdate : AbstractValidator<AdminRequestUpdate>
{
    public AdminValidatorRequestUpdate()
    {
        RuleFor(x => x.AdminId).NotEmpty();
        RuleFor(x => x.HashedPassword).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Role).NotEmpty();
        RuleFor(x => x.AdminName).NotEmpty();

    }
    
}
public class AdminValidatorLogin : AbstractValidator<AdminLogin>
{
    public AdminValidatorLogin()
    {
        RuleFor(x => x.AdminId).NotEmpty();
        RuleFor(x => x.HashedPassword).NotEmpty();

    }
    
}