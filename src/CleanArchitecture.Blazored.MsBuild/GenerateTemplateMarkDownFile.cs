using System.Text;
using Microsoft.Build.Framework;
using Task = Microsoft.Build.Utilities.Task;

namespace CleanArchitecture.Blazored.MsBuild;

public class GenerateTemplateMarkDownFile : Task
{
    private const string OutputDirectory = "TemplateItems/Docs";
    [Required] public string DirectoryToSearch { get; set; } = string.Empty;
    [Required] public string OutputFileName { get; set; } = string.Empty;
    
    public override bool Execute()
    {
        try
        {
            
            DirectoryToSearch = Path.Combine(Directory.GetCurrentDirectory(), DirectoryToSearch);
            var allProjectFiles =
                Directory.EnumerateDirectories(DirectoryToSearch);

            var rootDirectory = GetSolutionFileLocation(DirectoryToSearch);

            List<ProjectData> projects = [];
        
            foreach (var projectFile in allProjectFiles)
            {
                var name = projectFile[(projectFile.LastIndexOf(Path.DirectorySeparatorChar) + 1)..];
                var readMeFile =
                    ConvertToCrossPlatformPath(Path.GetRelativePath(rootDirectory,
                        Path.Combine(projectFile, "README.md")));
            
                projects.Add(new ProjectData
                {
                    Name = name,
                    Data = readMeFile
                });
            }

            CreateTemplateFile(projects);
            return true;
        }
        catch (Exception ex)
        {
            Log.LogMessage(MessageImportance.High, $"Error: {ex.Message}\nStacktrace: {ex.StackTrace}");
            return false;
        }
    }

    private string GetSolutionFileLocation(string directory)
    {
        while (!Directory.EnumerateFiles(directory,"*.sln").Any())
        {
            directory = directory[..directory.LastIndexOf(Path.DirectorySeparatorChar)];
        }

        return directory;
    }

    private string ConvertToCrossPlatformPath(string path)
    {
        return path.Replace('\\', '/');
    }

    private void CreateTemplateFile(List<ProjectData> projects)
    {
        var sb = new StringBuilder();

        foreach (var project in projects)
        {
            sb.Append("* [");
            sb.Append(project.Name);
            sb.Append("](");
            sb.Append(project.Data);
            sb.Append(')');
            sb.AppendLine();
        }

        var outputFile = Path.Combine(OutputDirectory, OutputFileName);
        
        File.WriteAllText(outputFile, sb.ToString());
    }
}

public class ProjectData
{
    public required string Name { get; init; }
    public required string Data { get; init; }
}