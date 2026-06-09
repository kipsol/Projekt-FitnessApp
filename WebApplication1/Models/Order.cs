<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
=======
﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
>>>>>>> origin/master

namespace WebApplication1.Models;

public class Order
{
    public int Id { get; set; }
<<<<<<< HEAD

=======
>>>>>>> origin/master
    public DateTime OrderDate { get; set; } = DateTime.Now;

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    [StringLength(30)]
<<<<<<< HEAD
    public string Status { get; set; } = "Oczekujace";

    public string? UserId { get; set; }

    public IdentityUser? User { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
=======
    public string Status { get; set; } = "Oczekujące";

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
>>>>>>> origin/master
