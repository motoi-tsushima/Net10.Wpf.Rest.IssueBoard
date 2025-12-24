using Net10.Wpf.Rest.IssueBoard.Services;
using Shared.Rest.IssueBoard;
using System.Windows;

namespace Net10.Wpf.Rest.IssueBoard.Views;

public partial class IssueDetailWindow : Window
{
    private readonly IssueApiService _apiService;
    private readonly int _issueId;
    private IssueDto? _issue;

    public IssueDetailWindow(int issueId)
    {
        InitializeComponent();
        _apiService = new IssueApiService();
        _issueId = issueId;
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        await LoadIssueAsync();
    }

    private async Task LoadIssueAsync()
    {
        try
        {
            _issue = await _apiService.GetIssueAsync(_issueId);
            if (_issue != null)
            {
                DisplayIssue(_issue);
            }
            else
            {
                MessageBox.Show("Issue not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to load issue: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Close();
        }
    }

    private void DisplayIssue(IssueDto issue)
    {
        AuthorNameTextBox.Text = issue.AuthorName;
        CreatedAtTextBox.Text = issue.CreatedAt.ToString("yyyy/MM/dd HH:mm:ss");
        CategoryTextBox.Text = issue.Category ?? "-";
        TitleTextBox.Text = issue.Title;
        DescriptionTextBox.Text = issue.Description;
        StatusTextBox.Text = GetStatusDisplayName(issue.Status);
        ResolutionTextBox.Text = issue.Resolution ?? string.Empty;
        ResolverNameTextBox.Text = issue.ResolverName ?? string.Empty;
        ResolvedAtTextBox.Text = issue.ResolvedAt?.ToString("yyyy/MM/dd HH:mm:ss") ?? string.Empty;
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

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (_issue != null)
        {
            var editWindow = new IssueEditWindow(_issue);
            editWindow.Owner = this;
            if (editWindow.ShowDialog() == true)
            {
                DialogResult = true;
                Close();
            }
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (_issue != null)
        {
            var deleteWindow = new IssueDeleteWindow(_issue);
            deleteWindow.Owner = this;
            if (deleteWindow.ShowDialog() == true)
            {
                DialogResult = true;
                Close();
            }
        }
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
