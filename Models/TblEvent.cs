using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RaceDay_Part2.Models;
public enum EventType
{
    Run,
    Walk,
    Cycle
}

public partial class TblEvent
{
    [Key]
    public int EventId { get; set; }


    public string EventName { get; set; } = null!;

    public DateOnly EventDate { get; set; }

    public string Location { get; set; } = null!;

    public string? Description { get; set; }

    public EventType EventType { get; set; }
}


