﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine.Types
{
    public record BasketItem(string Sku, int Quantity, decimal UnitCost);
    public record Promotion(string Placeholder);
}

