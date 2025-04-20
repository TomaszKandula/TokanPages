namespace TokanPages.Backend.Application.Logger.Commands.Models;

public class Parsed
{
    public Browser Browser { get; set; } = new();

    public Cpu Cpu { get; set; } = new();

    public Device Device { get; set; } = new();

    public Engine Engine { get; set; } = new();

    public Os Os { get; set; } = new();
}