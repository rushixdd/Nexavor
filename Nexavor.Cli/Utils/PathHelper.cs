namespace Nexavor.Cli.Utils;

public static class PathHelper
{
    public static string GetTemplatePath(params string[] parts)
    {
        var dir = AppContext.BaseDirectory;

        while (dir != null && !Directory.Exists(Path.Combine(dir, "Templates")))
        {
            dir = Directory.GetParent(dir)?.FullName!;
        }

        if (dir == null)
            throw new Exception("❌ Templates folder not found");

        return Path.Combine(dir, "Templates", Path.Combine(parts));
    }
}