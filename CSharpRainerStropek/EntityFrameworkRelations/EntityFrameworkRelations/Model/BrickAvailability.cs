using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkRelations.Model;

public class BrickAvailability
{
    public int Id { get; set; }

    
    public Vendor Vendor { get; set; }
    // FK - leads to vendor
    public int VendorId { get; set; }

    
    public Brick Brick { get; set; }
    // FK - leads to vendor
    public int BrickId { get; set; }


    public int AvailableAmount { get; set; }
    
    [Column(TypeName = "decimal(8, 2)")]
    public decimal PriceEur { get; set; }
}