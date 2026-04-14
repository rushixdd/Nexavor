namespace Nexavor.Cli.Engine;

public class EcosystemConfig
{
    public required string AppName { get; set; }

    public List<string> Services { get; set; } = new();

    public string Frontend { get; set; } = "react";
    public string Gateway { get; set; } = "yarp";
    public string OutputPath { get; set; } = Directory.GetCurrentDirectory();
    public bool Force { get; set; }
}