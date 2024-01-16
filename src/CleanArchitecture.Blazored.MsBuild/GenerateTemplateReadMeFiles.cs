using System.Text.RegularExpressions;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Task = Microsoft.Build.Utilities.Task;

namespace CleanArchitecture.Blazored.MsBuild;

public partial class GenerateTemplateReadMeFiles : Task
{
    private const string DocumentPath = "TemplateItems/Docs";
    private const string FileNamePattern = @"README_CleanArchitecture\.([A-Za-z0-9_]+)\.md";
    
    [Output] public ITaskItem[] ReadMeFiles { get; set; } = default!; 
    
    public override bool Execute()
    {
        try
        {
            var allFiles = Directory.EnumerateFiles(DocumentPath, "*.md");

            var filteredFiles = allFiles
                .Select(ExtractInformation)
                .Where(s=>s is not null)
                .ToArray();
                // .Where(f => regex.IsMatch(Path.GetFileName(f)))
                // .Select(ExtractInformation)
                // .ToArray();
        
            ReadMeFiles = filteredFiles!;
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static ITaskItem? ExtractInformation(string filePath)
    {
        var fileName = Path.GetFileName(filePath);
        var regex = MyRegex();
        var match = regex.Match(fileName);
        if (!match.Success)
            return null;

        var projectName = $"CleanArchitecture.{match.Groups[1].Value}";
        var destinationPath = Path.Combine("content", projectName, "README.md");

        var item = new TaskItem(destinationPath);
        item.SetMetadata("Key", fileName);

        return item;
    }

    [GeneratedRegex(FileNamePattern)]
    private static partial Regex MyRegex();
}