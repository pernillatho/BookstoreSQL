using System;
using System.Collections.Generic;

namespace Labb2Databaser.Models;

public partial class Genre
{
    public string Isbn { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual Book IsbnNavigation { get; set; } = null!;
}
