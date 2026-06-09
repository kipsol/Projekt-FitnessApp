using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.DTOs;

public class OrderItemDto
{
    public int Id { get; set; }

    [Display(Name = "Zamówienie")]
    [Required(ErrorMessage = "Wybierz zamówienie.")]
    public int OrderId { get; set; }

    [Display(Name = "Produkt")]
    [Required(ErrorMessage = "Wybierz produkt.")]
    public int ProductId { get; set; }

    [Display(Name = "Ilość")]
    [Required(ErrorMessage = "Podaj ilość.")]
    [Range(1, int.MaxValue, ErrorMessage = "Ilość musi być co najmniej 1.")]
    public int Quantity { get; set; }

    [Display(Name = "Cena w momencie zakupu")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal PriceAtPurchase { get; set; }
}
