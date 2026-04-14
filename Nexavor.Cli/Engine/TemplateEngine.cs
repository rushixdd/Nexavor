using Nexavor.Cli.Utils;

namespace Nexavor.Cli.Engine;

public class TemplateEngine
{
    private readonly TemplateProcessor _processor = new();
    private readonly DockerComposeGenerator _compose = new();
    private readonly YarpConfigGenerator _yarp = new();
    private readonly SolutionGenerator _solution = new();
    public void GenerateEcosystem(EcosystemConfig config)
    {
        Console.WriteLine($"🚀 Creating {config.AppName}");
        var root = Path.Combine(config.OutputPath, config.AppName);

        if (Directory.Exists(root))
        {
            if (!config.Force)
            {
                Console.WriteLine("❌ Project already exists. Use --force to overwrite.");
                return;
            }

            Console.WriteLine("⚠️ Overwriting existing project...");

            Directory.Delete(root, true);
        }

        Directory.CreateDirectory(root);
        _solution.CreateSolution(config, root);
        GenerateBackend(config);
        GenerateFrontend(config);
        GenerateGateway(config);
        GenerateInfra(config, root);
        _solution.AddProjects(config, root);
        Console.WriteLine("✅ Nexavor ecosystem ready");
    }

    // ---------------- BACKEND ----------------
    private void GenerateBackend(EcosystemConfig config)
    {
        foreach (var svc in config.Services)
        {
            var clean = svc.Trim();
            var pascal = char.ToUpper(clean[0]) + clean.Substring(1) + "Service";
            var root = Path.Combine(config.OutputPath, config.AppName);

            var servicePath = Path.Combine(root, "backend/services", $"{clean}-service");

            Directory.CreateDirectory(servicePath);

            Console.WriteLine($"⚙️ Creating service: {pascal}");

            ProcessRunner.Run(
                "dotnet",
                $"new nexavor-service -n {pascal}",
                servicePath
            );
        }
    }

    // ---------------- FRONTEND ----------------
    private void GenerateFrontend(EcosystemConfig config)
    {
        var root = Path.Combine(config.OutputPath, config.AppName);

        if (config.Frontend == "none") return;

        Console.WriteLine("🎨 Creating frontend");

        var source = Path.Combine("Templates", "Frontend", config.Frontend);
        var target = Path.Combine(root, "frontend/web-app");

        _processor.Copy(source, target);

        _processor.ReplaceTokens(target, new()
        {
            { "{{APP_NAME}}", config.AppName }
        });
    }

    // ---------------- GATEWAY ----------------
    private void GenerateGateway(EcosystemConfig config)
    {
        if (config.Gateway != "yarp") return;

        Console.WriteLine("🌐 Creating gateway");
        var root = Path.Combine(config.OutputPath, config.AppName);

        var source = PathHelper.GetTemplatePath("Gateway", "Yarp");
        var target = Path.Combine(root, "backend/gateway");

        _processor.Copy(source, target);

        _processor.ReplaceTokens(target, new()
        {
            { "{{APP_NAME}}", config.AppName }
            });

            _yarp.Generate(config);
        }

    // ---------------- INFRA ----------------
    private void GenerateInfra(EcosystemConfig config, string root)
    {
        Console.WriteLine("🐳 Creating infra");

        _compose.Generate(config, root);
    }
}