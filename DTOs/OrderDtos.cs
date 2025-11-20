namespace Lab09.DTOs;


public class ProductDto
{
    public string ProductName { get; set; } = String.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class OrderDetailsDto
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<ProductDto> Products { get; set; } = new List<ProductDto>();
}