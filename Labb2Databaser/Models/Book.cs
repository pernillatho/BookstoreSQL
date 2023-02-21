using System;
using System.Collections.Generic;

namespace Labb2Databaser.Models;

public partial class Book
{
    public string Isbn { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Language { get; set; } = null!;

    public int Price { get; set; }

    public DateTime? ReleaseYear { get; set; }

    public int AuthorId { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<Genre> Genres { get; } = new List<Genre>();

    public virtual ICollection<InventoryBalance> InventoryBalances { get; } = new List<InventoryBalance>();

    public virtual ICollection<Publisher> Publishers { get; } = new List<Publisher>();

    public override string ToString()
    {
        return Title;
    }
}
