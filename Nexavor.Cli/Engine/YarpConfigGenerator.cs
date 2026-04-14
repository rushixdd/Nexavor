using System.Text.Json;

namespace Nexavor.Cli.Engine;

public class YarpConfigGenerator
{
    public void Generate(EcosystemConfig config)
    {
        var root = Path.Combine(config.OutputPath, config.AppName);

        var gatewayPath = Path.Combine(root, "backend/gateway");

        var routes = new List<object>();
        var clusters = new Dictionary<string, object>();

        foreach (var svc in config.Services)
        {
            var clean = svc.Trim();
            var clusterId = $"{clean}-cluster";

            // ROUTE
            routes.Add(new
            {
                RouteId = clean,
                ClusterId = clusterId,
                Match = new
                {
                    Path = $"/api/{clean}/{{**catch-all}}"
                }
            });

            // CLUSTER
            clusters[clusterId] = new
            {
                Destinations = new Dictionary<string, object>
                {
                    {
                        "d1",
                        new
                        {
                            Address = $"http://{clean}-service"
                        }
                    }
                }
            };
        }

        var configObject = new
        {
            ReverseProxy = new
            {
                Routes = routes,
                Clusters = clusters
            }
        };

        var json = JsonSerializer.Serialize(configObject, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(Path.Combine(gatewayPath, "appsettings.json"), json);

        Console.WriteLine("🌐 YARP routes generated");
    }
}