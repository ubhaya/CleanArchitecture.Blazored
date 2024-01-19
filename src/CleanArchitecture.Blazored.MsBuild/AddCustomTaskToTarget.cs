using System.CodeDom.Compiler;
using System.Text;
using Microsoft.Build.Framework;
using Task = Microsoft.Build.Utilities.Task;

namespace CleanArchitecture.Blazored.MsBuild;

public class AddCustomTaskToTarget : Task
{
    private const string FileName = "build/CleanArchitecture.Blazored.MsBuild.props";
    [Required] public required ITaskItem[] Files { get; set; }

    [Required] public required string Namespace { get; set; }

    [Required] public required string TaskAssembly { get; set; }

    public override bool Execute()
    {
        var tasks = GetAllTask(Files);
        GenerateUsingTask(tasks);
        return true;
    }

    private IEnumerable<string> GetAllTask(ITaskItem[] files)
    {
        return files.Where(item => item.GetMetadata("Extension") == ".cs")
            .Select(item => item.GetMetadata("Filename"))
            .Select(fileName => "$" + $"(MSBuildThisFileName).{fileName}");
    }

    private void GenerateUsingTask(IEnumerable<string> tasks)
    {
        var baseTextWriter = new StringWriter();
        var indentWriter = new IndentedTextWriter(baseTextWriter);
        indentWriter.Indent = 0;
        foreach (var task in tasks)
        {
            indentWriter.WriteLine($$"""
                                     <UsingTask TaskName="{{task}}"
                                                AssemblyFile="{{"$"+"(CustomTasksAssembly)"}}" />
                                     """);
        }
        
        var template = File.ReadAllText("build/UsingTasks.props.template");
        
        var replacingText = $$"""
                              <!--Register our custom task Starts-->

                              {{baseTextWriter}}
                              <!--Register our custom task Ends-->
                              """;
                
        var newContent = template.Replace("<!--Register our custom task-->", replacingText);
                        
        File.WriteAllText(FileName, newContent);
    }
}