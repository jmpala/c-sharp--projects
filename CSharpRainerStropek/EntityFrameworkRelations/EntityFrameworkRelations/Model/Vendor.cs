using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkRelations.Model;

public class Vendor
{
    public int Id { get; set; }

    [MaxLength(250)]
    public string VendorName { get; set; }

    public List<BrickAvailability> Availability { get; set; } = new();
}