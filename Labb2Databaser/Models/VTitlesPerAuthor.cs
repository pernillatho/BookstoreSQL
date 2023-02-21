using System;
using System.Collections.Generic;

namespace Labb2Databaser.Models;

public partial class VTitlesPerAuthor
{
    public string Name { get; set; } = null!;

    public string Age { get; set; } = null!;

    public string TotalTitles { get; set; } = null!;

    public string TotalStoreValue { get; set; } = null!;
}
