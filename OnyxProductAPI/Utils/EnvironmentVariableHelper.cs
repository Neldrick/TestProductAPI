using System.Text.Json;

namespace OnyxProductAPI.Utils;

public class EnvironmentVariableHelper : IDisposable
{
    private string _filePath = string.Empty;

    public EnvironmentVariableHelper(string filePath = "/local.settings.json")
    {
        _filePath = filePath;
    }

    public void Dispose()
    {
        if (File.Exists(Directory.GetCurrentDirectory() + _filePath))
        {
            using var file = File.Open(Directory.GetCurrentDirectory() + _filePath, FileMode.Open);
            var document = JsonDocument.Parse(file);
            var variables = document.RootElement.EnumerateObject();
            foreach (var variable in variables)
            {
                Environment.SetEnvironmentVariable(variable.Name, null);
            }
        }
    }

    public void SetEnvironmentVariableFromFile()
    {
        if (File.Exists(Directory.GetCurrentDirectory() + _filePath))
        {
            using var file = File.Open(Directory.GetCurrentDirectory() + _filePath, FileMode.Open);
            var document = JsonDocument.Parse(file);
            var variables = document.RootElement.EnumerateObject();
            foreach (var variable in variables)
            {
                Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
            }
        }
    }
}

