using System.ComponentModel.DataAnnotations;

namespace RunningBlazor.Shared.Running;

public record Setting
{
    public int ID { get; init; } = -1;

    [MinLength(1)]
    [Required]
    public required string Key { get; init; }

    [MinLength(1)]
    [Required]
    public required string Value { get; init; }
}