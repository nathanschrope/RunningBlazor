using System.ComponentModel.DataAnnotations;

namespace RunningBlazor.Shared.Running;

public class DataTab
{
    public int ID { get; init; } = -1;

    [Required]
    [MinLength(1)]
    public required string Name { get; init; }

    [Required]
    [Range(0, double.MaxValue)]
    public required double Goal { get; init; }
}
