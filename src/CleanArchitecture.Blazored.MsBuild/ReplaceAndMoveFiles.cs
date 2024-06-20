using Microsoft.Build.Framework;
using Task = Microsoft.Build.Utilities.Task;

namespace CleanArchitecture.Blazored.MsBuild;

public class ReplaceAndMoveFiles : Task
{
    [Required] public string FileName { get; set; } = string.Empty;
    [Required] public string MatchExpression { get; set; } = string.Empty;
    [Required] public string ReplacementText { get; set; } = string.Empty;
    
    public override bool Execute()
    {
        try
        {
            if (!FileName.Contains(MatchExpression)) return true;

            var newPath = FileName.Replace(MatchExpression, ReplacementText);
            var destinationDirectory = Path.GetDirectoryName(newPath);
            Directory.CreateDirectory(destinationDirectory!);

            // Move the file
            File.Move(FileName, newPath, overwrite: true);
            
            Log.LogMessage($"Moved: {FileName} to {newPath}");
            return true;
        }
        catch (Exception ex)
        {
            Log.LogError(ex.Message);
            return false;
        }
    }
}