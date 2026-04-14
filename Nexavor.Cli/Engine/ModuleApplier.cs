namespace Nexavor.Cli.Engine;

public class ModuleApplier
{
    public void Apply(string outputPath, ServiceConfig config, Dictionary<string, string> tokens)
    {
        var basePath = Path.Combine("Templates", "Backend", "Service", "Modules");

        if (config.Database == "postgres")
            ApplyModule(Path.Combine(basePath, "Database/Postgres"), outputPath, tokens);

        if (config.Auth == "jwt")
            ApplyModule(Path.Combine(basePath, "Auth/Jwt"), outputPath, tokens);

        if (config.Cache == "redis")
            ApplyModule(Path.Combine(basePath, "Cache/Redis"), outputPath, tokens);
    }

    private void ApplyModule(string modulePath, string outputPath, Dictionary<string, string> tokens)
    {
        if (!Directory.Exists(modulePath)) return;

        var processor = new TemplateProcessor();

        processor.Copy(modulePath, outputPath);
        processor.ReplaceTokens(outputPath, tokens);
    }
}