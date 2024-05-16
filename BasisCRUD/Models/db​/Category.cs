using System;
using System.Collections.Generic;

namespace BasisCRUD.Models.db​;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Book> Books { get; } = new List<Book>();

    public static implicit operator Category(List<Category> v)
    {
        throw new NotImplementedException();
    }
}
