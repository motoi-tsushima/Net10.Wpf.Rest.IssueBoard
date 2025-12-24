using Net10.Wpf.Rest.IssueBoard.Services;
using Shared.Rest.IssueBoard;
using System.Windows;

namespace Net10.Wpf.Rest.IssueBoard.Views;

public partial class IssueDeleteWindow : Window
{
    private readonly IssueApiService _apiService;
    private readonly IssueDto _issue;

    public IssueDeleteWindow(IssueDto issue)
    {
        InitializeComponent();
        _apiService = new IssueApiService();
        _issue = issue;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        DisplayIssue();
    }

    private void DisplayIssue()
    {
        AuthorNameTextBox.Text = _issue.AuthorName;
        CreatedAtTextBox.Text = _issue.CreatedAt.ToString("yyyy/MM/dd HH:mm:ss");
        CategoryTextBox.Text = _issue.Category ?? "-";
        TitleTextBox.Text = _issue.Title;
        DescriptionTextBox.Text = _issue.Description;
        StatusTextBox.Text = GetStatusDisplayName(_issue.Status);
        ResolutionTextBox.Text = _issue.Resolution ?? string.Empty;
        ResolverNameTextBox.Text = _issue.ResolverName ?? string.Empty;
        ResolvedAtTextBox.Text = _issue.ResolvedAt?.ToString("yyyy/MM/dd HH:mm:ss") ?? string.Empty;
    }

    private string GetStatusDisplayName(IssueStatus status)
    {
        return status switch
        {
            IssueStatus.NotStarted => "NotStarted",
            IssueStatus.InProgress => "InProgress",
            IssueStatus.ResolutionFailed => "ResolutionFailed",
            IssueStatus.CannotConfirm => "CannotConfirm",
            IssueStatus.Resolved => "Resolved",
            _ => status.ToString()
        };
    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            await _apiService.DeleteIssueAsync(_issue.Id);
            MessageBox.Show("Issue deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to delete issue: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
