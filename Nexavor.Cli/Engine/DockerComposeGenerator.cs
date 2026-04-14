namespace Nexavor.Cli.Engine;

public class DockerComposeGenerator
{
    public void Generate(EcosystemConfig config, string root)
    {
        var infraPath = Path.Combine(root, "infra");
        Directory.CreateDirectory(infraPath);

        var sb = new System.Text.StringBuilder();

        sb.AppendLine("version: '3.8'");
        sb.AppendLine();
        sb.AppendLine("services:");

        // ---------------- GATEWAY ----------------
        sb.AppendLine("  gateway:");
        sb.AppendLine("    build: ../backend/gateway");
        sb.AppendLine("    ports:");
        sb.AppendLine("      - \"5000:80\"");
        sb.AppendLine();

        int port = 5001;

        // ---------------- SERVICES ----------------
        foreach (var svc in config.Services)
        {
            var clean = svc.Trim();
            var pascal = char.ToUpper(clean[0]) + clean.Substring(1) + "Service";

            sb.AppendLine($"  {clean}-service:");
            sb.AppendLine($"    build: ../backend/services/{clean}-service/{pascal}.API");
            sb.AppendLine("    ports:");
            sb.AppendLine($"      - \"{port}:80\"");
            sb.AppendLine("    depends_on:");
            sb.AppendLine("      - gateway");
            sb.AppendLine();

            port++;
        }

        File.WriteAllText(Path.Combine(infraPath, "docker-compose.yml"), sb.ToString());

        Console.WriteLine("🐳 docker-compose generated");
    }
}