namespace Global.Manager.Entities;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;

    public int CountryId { get; set; }
    public Country Country { get; set; } = default!;
}
