using Shared.Rest.IssueBoard;
using System.Globalization;
using System.Windows.Data;

namespace Net10.Wpf.Rest.IssueBoard.Converters;

public class IssueStatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IssueStatus status)
        {
            return status switch
            {
                IssueStatus.NotStarted => "未着手",
                IssueStatus.InProgress => "着手中",
                IssueStatus.ResolutionFailed => "解決失敗",
                IssueStatus.CannotConfirm => "課題確認不能",
                IssueStatus.Resolved => "解決済み",
                _ => status.ToString()
            };
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
