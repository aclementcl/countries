using System.ComponentModel.DataAnnotations;

namespace Global.Manager.Dtos;

public class UpdateCountryDto
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = default!;
}
