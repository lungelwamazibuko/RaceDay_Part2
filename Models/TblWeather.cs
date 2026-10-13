using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RaceDay.Models;

public partial class TblWeather
{
    [Key]
    public int WeatherId { get; set; }
    [Required]
    public int EventId { get; set; }

    public DateOnly WeatherDate { get; set; }

    public decimal? Temperature { get; set; }

    public string? Conditions { get; set; }

    public decimal? WindSpeed { get; set; }

   
}
