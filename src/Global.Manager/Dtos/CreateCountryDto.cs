using System.ComponentModel.DataAnnotations;

namespace Global.Manager.Dtos;

public class CreateCountryDto
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = default!;
}
