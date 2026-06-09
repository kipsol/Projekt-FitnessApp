using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    [StringLength(30)]
    public string Status { get; set; } = "Oczekujące";

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}