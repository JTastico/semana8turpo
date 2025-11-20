namespace Lab8_JamilTurpo.DTOs;

public class ClientOrderDto
{
    public string ClientName { get; set; }
    public List<OrderDto> Orders { get; set; }
}

public class ClientProductCountDto
{
    public string ClientName { get; set; }
    public int TotalProducts { get; set; }
}

public class OrderDto
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
}
public class ClientDto
{
    public string? ClientName { get; set; } 
    public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
}

public class SalesByClientDto
{
    public string ClientName { get; set; } = string.Empty;
    public decimal TotalSales { get; set; }
}