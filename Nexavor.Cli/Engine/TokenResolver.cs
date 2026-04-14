namespace Nexavor.Cli.Engine;

public class TokenResolver
{
    public Dictionary<string, string> BuildServiceTokens(ServiceConfig config)
    {
        return new Dictionary<string, string>
        {
            { "{{APP_NAME}}", config.AppName },
            { "{{SERVICE_NAME}}", config.ServiceName },
            { "{{SERVICE_NAME_LOWER}}", config.ServiceName.ToLower() },
            { "{{NAMESPACE}}", $"{config.AppName}.{config.ServiceName}" }
        };
    }
}