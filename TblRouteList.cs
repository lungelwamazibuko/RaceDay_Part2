// File: Models/TblRoute.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDay_Part2.Models
{
    public class TblRoute
    {
        [Key]
        public int RouteId { get; set; }

        [Required]
        [MaxLength(200)]
        public string RouteName { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal? DistanceKm { get; set; }

    }
}