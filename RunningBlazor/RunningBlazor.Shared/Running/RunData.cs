﻿using System.ComponentModel.DataAnnotations;

namespace RunningBlazor.Shared.Running;

public record RunData
{
    public int ID { get; init; } = -1;

    [Required]
    public required DateTime Date { get; init; }

    [Required]
    [Range(1, float.MaxValue)]
    public required int Milliseconds { get; init; }

    [Required]
    [Range(1, float.MaxValue)]
    public required int Feet { get; init; }

    public float Speed
    {
        get
        {
            return this.Miles / (this.Minutes / 60);
        }
    }

    public float Miles
    {
        get
        {
            return this.Feet / 5280;
        }
    }

    public float Minutes
    {
        get
        {
            return this.Milliseconds / 60000;
        }
    }
}