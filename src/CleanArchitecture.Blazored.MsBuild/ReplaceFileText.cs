using System.Text.RegularExpressions;
using Microsoft.Build.Framework;
using Task = Microsoft.Build.Utilities.Task;

namespace CleanArchitecture.Blazored.MsBuild;

public class ReplaceFileText : Task
{
    [Required] public string FileName { get; set; } = string.Empty;
    [Required] public string MatchExpression { get; set; } = string.Empty;
    [Required] public string ReplacementText { get; set; } = string.Empty;
    
    public override bool Execute()
    {
        try
        {
            Log.LogMessage(MessageImportance.High, $"Replacing {MatchExpression}->{ReplacementText} in {FileName}");
            var contentBeforeReplace = File.ReadAllText(FileName);
            var contentAfterReplace = Regex.Replace(contentBeforeReplace, MatchExpression, ReplacementText);
            File.WriteAllText(FileName, contentAfterReplace);
            return true;
        }
        catch (Exception ex)
        {
            Log.LogError(ex.Message);
            return false;
        }
    }
}