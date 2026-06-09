using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Order
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Data zamówienia")]
    public DateTime OrderDate { get; set; } = DateTime.Now;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Łączna kwota")]
    public decimal TotalPrice { get; set; }

    [Required]
    [StringLength(30)]
    [Display(Name = "Status")]
    public string Status { get; set; } = "Oczekujące";

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}