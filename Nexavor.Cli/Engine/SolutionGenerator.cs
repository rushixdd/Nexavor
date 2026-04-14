using Nexavor.Cli.Utils;

namespace Nexavor.Cli.Engine;

public class SolutionGenerator
{
    public void Generate(EcosystemConfig config)
    {
        var root = Path.Combine(config.OutputPath, config.AppName);

        Console.WriteLine("🧩 Creating root solution");

        // Create solution
        ProcessRunner.Run(
            "dotnet",
            $"new sln -n {config.AppName}",
            root
        );

        var slnPath = Path.Combine(root, $"{config.AppName}.sln");

        // ---------------- ADD SERVICES ----------------
        foreach (var svc in config.Services)
        {
            var clean = svc.Trim();
            var pascal = char.ToUpper(clean[0]) + clean.Substring(1) + "Service";

            var apiProj = Path.Combine(
                root,
                "backend/services",
                $"{clean}-service",
                pascal,
                $"{pascal}.API",
                $"{pascal}.API.csproj"
            );

            if (File.Exists(apiProj))
            {
                ProcessRunner.Run(
                    "dotnet",
                    $"sln \"{slnPath}\" add \"{apiProj}\"",
                    root
                );
            }
        }

        // ---------------- ADD GATEWAY ----------------
        var gatewayProj = Path.Combine(root, "backend/gateway", "Gateway.csproj");

        if (File.Exists(gatewayProj))
        {
            ProcessRunner.Run(
                "dotnet",
                $"sln \"{slnPath}\" add \"{gatewayProj}\"",
                root
            );
        }

        Console.WriteLine("✅ Solution wired");
    }

    public void CreateSolution(EcosystemConfig config, string root)
    {
        Console.WriteLine("🧩 Creating root solution");

        var forceFlag = config.Force ? "--force" : "";

        ProcessRunner.Run(
            "dotnet",
            $"new sln -n {config.AppName} --output \"{root}\" {forceFlag}",
            Directory.GetCurrentDirectory()
        );

        var slnPath = Path.Combine(root, $"{config.AppName}.sln");
        var slnxPath = Path.Combine(root, $"{config.AppName}.slnx");

        if (!File.Exists(slnPath) && !File.Exists(slnxPath))
        {
            throw new Exception("❌ Solution file was not created");
        }

        Console.WriteLine($"Checking solution at: {slnPath}");
    }


    public void AddProjects(EcosystemConfig config, string root)
    {
        Console.WriteLine("🔗 Adding projects to solution");

        var slnFile = File.Exists(Path.Combine(root, $"{config.AppName}.sln"))
     ? $"{config.AppName}.sln"
     : $"{config.AppName}.slnx";

        var fullSlnPath = Path.Combine(root, slnFile);

        if (!File.Exists(fullSlnPath))
        {
            throw new Exception($"❌ Solution not found at {fullSlnPath}");
        }

        // gateway
        var gatewayProj = Path.Combine("backend/gateway", "Gateway.csproj");

        if (File.Exists(Path.Combine(root, gatewayProj)))
        {
            ProcessRunner.Run(
                "dotnet",
                $"sln \"{slnFile}\" add \"{gatewayProj}\"",
                root   // ✅ SAME ROOT
            );
        }
    }
}