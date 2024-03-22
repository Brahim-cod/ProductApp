﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ModelsDto;

public class OrderProductDto
{
    public int OrderId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public double Amount { get; set; }
    public ICollection<ProductDto> Products { get; set; }
}
