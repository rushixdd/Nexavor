namespace Nexavor.Cli.Engine;

public class TemplateProcessor
{
    public void Copy(string source, string target)
    {
        if (!Directory.Exists(source))
            throw new Exception($"❌ Template not found: {source}");

        foreach (var file in Directory.GetFiles(source, "*", SearchOption.AllDirectories))
        {
            var relative = Path.GetRelativePath(source, file);
            var dest = Path.Combine(target, relative);

            Directory.CreateDirectory(Path.GetDirectoryName(dest)!);
            File.Copy(file, dest, true);
        }
    }

    public void ReplaceTokens(string folder, Dictionary<string, string> tokens)
    {
        foreach (var file in Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories))
        {
            var content = File.ReadAllText(file);

            foreach (var token in tokens)
                content = content.Replace(token.Key, token.Value);

            File.WriteAllText(file, content);
        }
    }
}