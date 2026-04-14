namespace Nexavor.Cli.Engine;

public class ServiceConfig
{
    public required string AppName { get; set; }
    public required string ServiceName { get; set; }

    public string Database { get; set; } = "postgres";
    public string Auth { get; set; } = "none";
    public string Cache { get; set; } = "none";
}