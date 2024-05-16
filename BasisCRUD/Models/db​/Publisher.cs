using System;
using System.Collections.Generic;

namespace BasisCRUD.Models.db​;

public partial class Publisher
{
    public int PublishId { get; set; }

    public string PublishName { get; set; } = null!;

    public string ContactName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public virtual ICollection<Book> Books { get; } = new List<Book>();

    public static implicit operator Publisher(List<Publisher> v)
    {
        throw new NotImplementedException();
    }
}
