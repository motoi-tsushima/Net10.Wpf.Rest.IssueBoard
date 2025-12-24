using System.Configuration;

namespace Net10.Wpf.Rest.IssueBoard.Properties;

[SettingsProvider(typeof(LocalFileSettingsProvider))]
public sealed class Settings : ApplicationSettingsBase
{
    private static Settings defaultInstance = (Settings)Synchronized(new Settings());

    public static Settings Default => defaultInstance;

    [UserScopedSetting]
    [DefaultSettingValue("")]
    public string LastAuthorName
    {
        get => (string)this["LastAuthorName"];
        set => this["LastAuthorName"] = value;
    }
}
