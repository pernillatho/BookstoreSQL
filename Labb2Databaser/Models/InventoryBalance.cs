﻿using System;
using System.Collections.Generic;

namespace Labb2Databaser.Models;

public partial class InventoryBalance
{
    public int StoreId { get; set; }

    public string Isbn { get; set; } = null!;

    public int Quantity { get; set; }

    public virtual Book IsbnNavigation { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;

}
