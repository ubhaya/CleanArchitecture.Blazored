using Microsoft.Build.Framework;
using Task = Microsoft.Build.Utilities.Task;

namespace CleanArchitecture.Blazored.MsBuild;

public class MyTask : Task
{
    public string? MyProperty { get; set; }
    public override bool Execute()
    {
        Log.LogMessage(MessageImportance.High, $"The task was passed {MyProperty}.");
        return true;
    }
}
