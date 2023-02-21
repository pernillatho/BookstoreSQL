using System;
using System.Collections.Generic;

namespace Labb2Databaser.Models;

public partial class Store
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public string City { get; set; } = null!;

    public virtual ICollection<InventoryBalance> InventoryBalances { get; } = new List<InventoryBalance>();

    public override string ToString()
    {
        return Name;
    }
}
