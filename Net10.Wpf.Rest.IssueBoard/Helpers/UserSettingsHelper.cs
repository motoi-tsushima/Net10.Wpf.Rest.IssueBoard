using System.Configuration;

namespace Net10.Wpf.Rest.IssueBoard.Helpers;

public static class UserSettingsHelper
{
    private const string AuthorNameKey = "LastAuthorName";

    public static string GetLastAuthorName()
    {
        try
        {
            var settings = Properties.Settings.Default;
            return settings[AuthorNameKey]?.ToString() ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    public static void SaveAuthorName(string authorName)
    {
        try
        {
            var settings = Properties.Settings.Default;
            settings[AuthorNameKey] = authorName;
            settings.Save();
        }
        catch
        {
            // Ignore save errors
        }
    }
}
