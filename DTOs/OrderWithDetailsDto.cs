using System;
using System.Collections.Generic;

namespace Lab8_JamilTurpo.DTOs;

public class OrderWithDetailsDto
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public int ClientId { get; set; }
    
    public List<OrderDetailDto> Details { get; set; }
}