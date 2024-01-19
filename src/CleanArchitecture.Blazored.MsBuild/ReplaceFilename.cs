using Microsoft.Build.Framework;
using Task = Microsoft.Build.Utilities.Task;

namespace CleanArchitecture.Blazored.MsBuild;

public class ReplaceFilename : Task
{
    [Required] public string Filename { get; set; } = string.Empty;
    [Required] public string MatchExpression { get; set; } = string.Empty;
    [Required] public string ReplacementText { get; set; } = string.Empty;

    public override bool Execute()
    {
        try
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Filename);

            if (!fileNameWithoutExtension.Contains(MatchExpression)) return true;
            var path = Path.GetDirectoryName(Filename);
            if (string.IsNullOrWhiteSpace(path))
            {
                Log.LogError($"path variable cannot be null in {Filename}");
                return false;
            }
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(MatchExpression, ReplacementText);
            var extension = Path.GetExtension(Filename);
            var newFileName = Path.Combine(path, $"{fileNameWithoutExtension}{extension}");
            Log.LogMessage(MessageImportance.High, $"{Filename} -> {newFileName}");
            try
            {
                File.Move(Filename, newFileName, overwrite: true);
            }
            catch
            {
                File.Delete(Filename);
            }

            return true;
        }
        catch (Exception ex)
        {
            Log.LogError(ex.Message);
            return false;
        }
    }
}