using System.ComponentModel.DataAnnotations;

namespace Global.Manager.Dtos;

public class CreateCityDto
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = default!;

    [Required]
    public int CountryId { get; set; }
}
