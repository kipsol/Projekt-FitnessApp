using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.DTOs;

public class OrderDto
{
    public int Id { get; set; }

    [Display(Name = "Data zamówienia")]
    [Required(ErrorMessage = "Data zamówienia jest wymagana.")]
    public DateTime OrderDate { get; set; } = DateTime.Now;

    [Display(Name = "Łączna kwota")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    [Display(Name = "Status")]
    [Required(ErrorMessage = "Status jest wymagany.")]
    [StringLength(30, ErrorMessage = "Status może mieć maksymalnie 30 znaków.")]
    public string Status { get; set; } = "Oczekujące";
}
