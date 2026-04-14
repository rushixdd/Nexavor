using System.CommandLine;
using Nexavor.Cli.Engine;

namespace Nexavor.Cli.Commands;

public static class CreateCommand
{
    public static Command Build()
    {
        var cmd = new Command("create", "Create ecosystem");

        var nameOption = new Option<string>("--name");
        var servicesOption = new Option<string>("--services");
        var frontendOption = new Option<string>("--frontend");
        var gatewayOption = new Option<string>("--gateway");
        var outputOption = new Option<string>("--output");
        var forceOption = new Option<bool>("--force");

        cmd.Add(forceOption);
        cmd.Add(nameOption);
        cmd.Add(servicesOption);
        cmd.Add(frontendOption);
        cmd.Add(gatewayOption);
        cmd.Add(outputOption);
        cmd.Add(forceOption);

        cmd.SetAction(parseResult =>
        {
            var name = parseResult.GetValue(nameOption);
            var services = parseResult.GetValue(servicesOption);
            var frontend = parseResult.GetValue(frontendOption);
            var gateway = parseResult.GetValue(gatewayOption);
            var output = parseResult.GetValue(outputOption);
            var force = parseResult.GetValue(forceOption);
            services ??= "user";
            frontend ??= "react";
            gateway ??= "yarp";

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("❌ --name is required");
                return;
            }

            if (string.IsNullOrWhiteSpace(output))
            {
                output = Directory.GetCurrentDirectory();
            }

            var config = new EcosystemConfig
            {
                AppName = name,
                Services = services.Split(',').Select(x => x.Trim()).ToList(),
                Frontend = frontend,
                Gateway = gateway,
                OutputPath = output,
                Force = force
            };

            var engine = new TemplateEngine();
            engine.GenerateEcosystem(config);
        });

        return cmd;
    }
}