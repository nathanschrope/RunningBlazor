using System.ComponentModel.DataAnnotations;

namespace RunningBlazor.Shared.Running;

public class DataTabData
{
    public int ID { get; init; } = -1;

    [Required]
    public required DateTime Date { get; init; }

    [Required]
    [Range(0, double.MaxValue)]
    public required double Value { get; init; }
}
