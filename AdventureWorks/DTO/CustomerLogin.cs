namespace AdventureWorks.DTO;

public class CustomerLogin
{
    
    public int CustomerId { get; set; }
    /// <summary>
    /// E-mail address for the person.
    /// </summary>
    public string? EmailAddress { get; set; }
    
    /// <summary>
    /// HashedPassword for the e-mail account.
    /// </summary>
    public string Password { get; set; } = null!;


}