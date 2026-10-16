using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RaceDay_Part2.Models;

public partial class TblCategory
{
    [Key]
    public int CategoryId { get; set; }
    [Required]
    public int EventId { get; set; }
        
    [Required]
    public int RouteId { get; set; }        // keep if used
    [Required]
    public string CategoryName { get; set; } = null!;
    [Required]
    public string? Gender { get; set; }
    public int? MinimumAge { get; set; }

    public int? MaximumAge { get; set; }

    public double? DistanceKm { get; set; } // new: distance in km

    public virtual TblEvent? Event { get; set; }

    public virtual TblRoute Route { get; set; } = null!;

}

public class CategoryViewModel
{
    public int? CategoryId { get; set; }

    [Required]
    public int EventId { get; set; }

    // optional read-only event label for the view
    public string? EventName { get; set; }

    [Required(ErrorMessage = "Route is required")]
    [Display(Name = "Route")]
    public int RouteId { get; set; }

    // select items for routes (populated by controller)
    public IEnumerable<SelectListItem> RouteSelectList { get; set; } = Enumerable.Empty<SelectListItem>();

    [Required]
    [Display(Name = "Category Name")]
    public string CategoryName { get; set; } = null!;

    [Required]
    public string Gender { get; set; } = null!;

    [Range(0, 200)]
    [Display(Name = "Minimum Age")]
    public int? MinimumAge { get; set; }

    [Range(0, 200)]
    [Display(Name = "Maximum Age")]
    public int? MaximumAge { get; set; }

    [Display(Name = "Distance (km)")]
    public double? DistanceKm { get; set; }
}
