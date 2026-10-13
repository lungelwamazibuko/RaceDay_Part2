using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RaceDay.Models;

public partial class TblRoute
{
    [Key]
    public int RouteId { get; set; }
    [Required]
    public int EventId { get; set; }

    public string RouteName { get; set; } = null!;

    public decimal DistanceKm { get; set; }

    public string? Description { get; set; }

}
