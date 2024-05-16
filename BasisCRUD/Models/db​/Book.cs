using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasisCRUD.Models.db​;

public partial class Book
{
    public int BookId { get; set; }

    public string BookName { get; set; } = null!;

    public int CategoryId { get; set; }

    public int PublishId { get; set; }

    public string Isbn { get; set; } = null!;

    public double BookCost { get; set; }

    public double? BookPrice { get; set; }
    public virtual Category? Category { get; set; } 

    public virtual Publisher? Publish { get; set; } 
}
