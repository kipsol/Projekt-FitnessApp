using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.DTOs;

public class ProductDto
{
    public int Id { get; set; }

    [Display(Name = "Nazwa produktu")]
    [Required(ErrorMessage = "Nazwa produktu jest wymagana.")]
    [StringLength(150, ErrorMessage = "Nazwa może mieć maksymalnie 150 znaków.")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Opis")]
    [StringLength(1000, ErrorMessage = "Opis może mieć maksymalnie 1000 znaków.")]
    public string? Description { get; set; }

    [Display(Name = "Cena (PLN)")]
    [Required(ErrorMessage = "Cena jest wymagana.")]
    [Range(0.01, 99999.99, ErrorMessage = "Cena musi być większa od 0.")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Display(Name = "Stan magazynowy")]
    [Required(ErrorMessage = "Stan magazynowy jest wymagany.")]
    [Range(0, int.MaxValue, ErrorMessage = "Stan magazynowy nie może być ujemny.")]
    public int Stock { get; set; }

    [Display(Name = "Kategoria")]
    [Required(ErrorMessage = "Kategoria jest wymagana.")]
    [StringLength(50, ErrorMessage = "Kategoria może mieć maksymalnie 50 znaków.")]
    public string Category { get; set; } = "Suplementy";
}
