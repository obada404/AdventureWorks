namespace AdventureWorks.DTO;

public class DTOs
{
}
public record prudectRequestEnv<T> ( T product);

public record PurchaseRequestEnv<T, T2>(T salesorder, T2 PurchaseRequest);


public record Token ( String token);

