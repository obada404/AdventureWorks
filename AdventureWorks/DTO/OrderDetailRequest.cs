namespace AdventureWorks.DTO;

public class OrderDetailRequest
{
    /// <summary>
    /// Primary key. Foreign key to SalesOrderHeader.SalesOrderID.
    /// </summary>
    public int SalesOrderId { get; set; }

    /// <summary>
    /// Primary key. One incremental unique number per product sold.
    /// </summary>
    public int SalesOrderDetailId { get; set; }

    /// <summary>
    /// Quantity ordered per product.
    /// </summary>
    public short OrderQty { get; set; }

    /// <summary>
    /// Product sold to customer. Foreign key to Product.ProductID.
    /// </summary>
    public int ProductId { get; set; }
}
public class OrderDetailRequestmin
{
    
    /// <summary>
    /// Quantity ordered per product.
    /// </summary>
    public short OrderQty { get; set; }

    /// <summary>
    /// Product sold to customer. Foreign key to Product.ProductID.
    /// </summary>
    public int ProductId { get; set; }
}
