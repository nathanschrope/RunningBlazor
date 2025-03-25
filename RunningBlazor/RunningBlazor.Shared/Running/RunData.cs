using System.ComponentModel.DataAnnotations;

namespace RunningBlazor.Shared.Running;

public record RunData
{
    public int ID { get; init; } = -1;

    [Required]
    public required DateTime Date { get; init; }

    [Required]
    [Range(1, int.MaxValue)]
    public required int Milliseconds { get; init; }

    [Required]
    [Range(1, int.MaxValue)]
    public required int Feet { get; init; }

    public float Speed => (float)this.Miles / ((float)this.Minutes / 60);

    public float Miles => (float)this.Feet / 5280;


    public float Minutes => (float)this.Milliseconds / 60000;
}