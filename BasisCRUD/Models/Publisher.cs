using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BasisCRUD.Models;

public partial class Publisher
{
    public int PublishId { get; set; }
    [Required]
    public string? PublishName { get; set; }

    public string? ContactName { get; set; }

    public string? Address { get; set; }

    public string? Telephone { get; set; }

    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
