using System;
using System.Collections.Generic;

namespace EntityFrameworkUsingDBFirst.Models;

public partial class BookCategory
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
