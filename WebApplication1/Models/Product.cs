using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    [Display(Name = "Nazwa produktu")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Opis")]
    [StringLength(1000)]
    public string? Description { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Cena (PLN)")]
    public decimal Price { get; set; }

    [Required]
    [Display(Name = "Stan magazynowy")]
    public int Stock { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Kategoria")]
    public string Category { get; set; } = "Suplementy";
}