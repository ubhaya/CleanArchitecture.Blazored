using Microsoft.Build.Framework;
using Task = Microsoft.Build.Utilities.Task;

namespace CleanArchitecture.Blazored.MsBuild;

public class InsertIntoFile :Task
{
    [Required] public string? FilePath { get; set; }
    [Required] public string? Text { get; set; }
    [Required] public int LineNumber { get; set; }
    
    public override bool Execute()
    {
        if (string.IsNullOrWhiteSpace(FilePath) || string.IsNullOrWhiteSpace(Text))
        {
            return false;
        }
        var lines = File.Exists(FilePath)
            ? File.ReadAllLines(FilePath).ToList()
            : new List<string>(1);
        
        lines.Insert(Math.Min(LineNumber-1, lines.Count),Text);
        File.WriteAllLines(FilePath, lines);
        return true;
    }
}