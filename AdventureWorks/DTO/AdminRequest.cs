namespace AdventureWorks.DTO;

public class AdminRequest
{
    public int AdminId { get; set; }

    public string? AdminName { get; set; }

    public string? Email { get; set; }
    
    public string? Role { get; set; }
}