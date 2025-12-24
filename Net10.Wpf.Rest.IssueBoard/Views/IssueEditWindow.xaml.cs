using Net10.Wpf.Rest.IssueBoard.Services;
using Shared.Rest.IssueBoard;
using System.Windows;

namespace Net10.Wpf.Rest.IssueBoard.Views;

public partial class IssueEditWindow : Window
{
    private readonly IssueApiService _apiService;
    private readonly IssueDto _issue;

    public IssueEditWindow(IssueDto issue)
    {
        InitializeComponent();
        _apiService = new IssueApiService();
        _issue = issue;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        InitializeStatusComboBox();
        DisplayIssue();
    }

    private void InitializeStatusComboBox()
    {
        var statuses = new[]
        {
            new { Value = IssueStatus.NotStarted, Display = "NotStarted" },
            new { Value = IssueStatus.InProgress, Display = "InProgress" },
            new { Value = IssueStatus.ResolutionFailed, Display = "ResolutionFailed" },
            new { Value = IssueStatus.CannotConfirm, Display = "CannotConfirm" },
            new { Value = IssueStatus.Resolved, Display = "Resolved" }
        };

        StatusComboBox.ItemsSource = statuses;
    }

    private void DisplayIssue()
    {
        AuthorNameTextBox.Text = _issue.AuthorName;
        CreatedAtTextBox.Text = _issue.CreatedAt.ToString("yyyy/MM/dd HH:mm:ss");
        CategoryTextBox.Text = _issue.Category ?? string.Empty;
        TitleTextBox.Text = _issue.Title;
        DescriptionTextBox.Text = _issue.Description;
        StatusComboBox.SelectedValue = _issue.Status;
        ResolutionTextBox.Text = _issue.Resolution ?? string.Empty;
        ResolverNameTextBox.Text = _issue.ResolverName ?? string.Empty;
        ResolvedAtTextBox.Text = _issue.ResolvedAt?.ToString("yyyy/MM/dd HH:mm:ss") ?? string.Empty;
    }

    private async void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        if (!ValidateInput())
        {
            return;
        }

        try
        {
            var updateDto = new UpdateIssueDto
            {
                Category = string.IsNullOrWhiteSpace(CategoryTextBox.Text) ? null : CategoryTextBox.Text,
                Title = TitleTextBox.Text,
                Description = DescriptionTextBox.Text,
                Status = (IssueStatus)StatusComboBox.SelectedValue,
                Resolution = string.IsNullOrWhiteSpace(ResolutionTextBox.Text) ? null : ResolutionTextBox.Text,
                ResolverName = string.IsNullOrWhiteSpace(ResolverNameTextBox.Text) ? null : ResolverNameTextBox.Text
            };

            await _apiService.UpdateIssueAsync(_issue.Id, updateDto);
            MessageBox.Show("Issue updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to update issue: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
        {
            MessageBox.Show("Title is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            TitleTextBox.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
        {
            MessageBox.Show("Description is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            DescriptionTextBox.Focus();
            return false;
        }

        if (StatusComboBox.SelectedValue == null)
        {
            MessageBox.Show("Please select a status.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            StatusComboBox.Focus();
            return false;
        }

        return true;
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
