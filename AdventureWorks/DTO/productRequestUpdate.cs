namespace AdventureWorks.DTO;

public class productRequestUpdate
{
    
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;
    
    public string ProductNumber { get; set; } = null!;

    public string? Color { get; set; }
    
    public decimal StandardCost { get; set; }

    public decimal ListPrice { get; set; }

    public string? Size { get; set; }

    public decimal? Weight { get; set; }
    
    public DateTime SellStartDate { get; set; }

    public DateTime? SellEndDate { get; set; }

    public DateTime? DiscontinuedDate { get; set; }

    
}