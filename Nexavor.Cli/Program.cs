using System.CommandLine;
using Nexavor.Cli.Commands;

var root = new RootCommand("Nexavor - Microservices Platform Generator");
root.Subcommands.Add(CreateCommand.Build());
return root.Parse(args).Invoke();