using Microsoft.Build.Framework;
using Task = Microsoft.Build.Utilities.Task;

namespace CleanArchitecture.Blazored.MsBuild;

public class DeleteEmptyFolders : Task
{
    [Required] public string? RootFolder { get; set; }
    public override bool Execute()
    {
        if (string.IsNullOrWhiteSpace(RootFolder))
            return false;

        DeleteEmptySubFolders(RootFolder);

        return !Log.HasLoggedErrors;
    }

    private void DeleteEmptySubFolders(string folderPath)
    {
        try
        {
            if (!Directory.Exists(RootFolder))
                return;

            var subFolders = Directory.GetDirectories(folderPath);
            
            foreach (var subFolder in subFolders)
            {
                DeleteEmptySubFolders(subFolder);

                if (IsFolderEmpty(subFolder))
                    Directory.Delete(subFolder);
            }
        }
        catch (Exception ex)
        {
            Log.LogError(subcategory: null,
                errorCode: "DEF0001",
                helpKeyword: null,
                file: folderPath,
                lineNumber: 0,
                columnNumber: 0,
                endLineNumber: 0,
                endColumnNumber: 0,
                message: ex.Message);
        }
    }
    
    private static bool IsFolderEmpty(string folderPath)
    {
        // Check if the folder contains any files or subdirectories
        return Directory.GetFiles(folderPath).Length == 0 &&
               Directory.GetDirectories(folderPath).Length == 0;
    }
}