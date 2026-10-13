using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RaceDay.Models;

public partial class TblRegistration
{
    [Key]
    public int RegistrationId { get; set; }
    [Required]
    public int ParticipantId { get; set; }
    [Required]
    public int CategoryId { get; set; }

    public DateTime RegistrationDate { get; set; }

    public int BibNumber { get; set; }

    public string RegistrationStatus { get; set; } = null!;

}
