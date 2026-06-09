using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class CwiczenieDto
{
    public int Id { get; set; }

    [Display(Name = "Nazwa")]
    [Required(ErrorMessage = "Podaj nazwę ćwiczenia.")]
    [StringLength(120, ErrorMessage = "Nazwa może mieć maksymalnie 120 znaków.")]
    public string Nazwa { get; set; } = string.Empty;

    [Display(Name = "Opis wykonania")]
    [Required(ErrorMessage = "Podaj opis wykonania ćwiczenia.")]
    [StringLength(500, ErrorMessage = "Opis może mieć maksymalnie 500 znaków.")]
    public string OpisWykonania { get; set; } = string.Empty;

    [Display(Name = "Partia mięśniowa")]
    [Required(ErrorMessage = "Wybierz partię mięśniową.")]
    public int PartiaMiesniowaId { get; set; }

    [Display(Name = "Maszyna")]
    public int? MaszynaId { get; set; }

    [Display(Name = "Liczba serii")]
    [Required(ErrorMessage = "Podaj liczbę serii.")]
    [Range(1, 100, ErrorMessage = "Liczba serii musi być między 1 a 100.")]
    public int LiczbaSerii { get; set; }

    [Display(Name = "Liczba powtórzeń")]
    [Required(ErrorMessage = "Podaj liczbę powtórzeń.")]
    [StringLength(50, ErrorMessage = "Maksymalnie 50 znaków.")]
    public string LiczbaPowtorzen { get; set; } = string.Empty;

    [Display(Name = "Przerwa (sekundy)")]
    [Required(ErrorMessage = "Podaj czas przerwy.")]
    [Range(0, 600, ErrorMessage = "Przerwa musi być między 0 a 600 sekund.")]
    public int PrzerwaSekundy { get; set; }

    [Display(Name = "Plik (ścieżka)")]
    public string? PlikSciezka { get; set; }
}
