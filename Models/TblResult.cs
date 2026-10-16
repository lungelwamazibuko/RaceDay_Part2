using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RaceDay_Part2.Models;

public partial class TblResult
{
    [Key]
    public int ResultId { get; set; }
    [Required]
    public int RegistrationId { get; set; }

    public TimeOnly? FinishTime { get; set; }

    public int? Position { get; set; }

    public string ResultStatus { get; set; } = null!;

}
