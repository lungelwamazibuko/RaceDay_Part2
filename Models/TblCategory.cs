using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RaceDay_Part2.Models;

public partial class TblCategory
{
    [Key]
    public int CategoryId { get; set; }
    [Required]
    public int EventId { get; set; }
        
    [Required]
    public int RouteId { get; set; }
    [Required]
    public string CategoryName { get; set; } = null!;

    public string? Gender { get; set; }

    public int? MinimumAge { get; set; }

    public int? MaximumAge { get; set; }

    public virtual TblEvent Event { get; set; } = null!;

    public virtual TblRoute Route { get; set; } = null!;

}
