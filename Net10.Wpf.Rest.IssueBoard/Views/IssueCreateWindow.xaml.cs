using Net10.Wpf.Rest.IssueBoard.Services;
using Net10.Wpf.Rest.IssueBoard.Properties;
using Shared.Rest.IssueBoard;
using System.Net.Http;
using System.Windows;

namespace Net10.Wpf.Rest.IssueBoard.Views;

public partial class IssueCreateWindow : Window
{
    private readonly IssueApiService _apiService;

    public IssueCreateWindow()
    {
        InitializeComponent();
        _apiService = new IssueApiService();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        LoadLastAuthorName();
    }

    private void LoadLastAuthorName()
    {
        var lastAuthorName = Settings.Default.LastAuthorName;
        if (!string.IsNullOrWhiteSpace(lastAuthorName))
        {
            AuthorNameTextBox.Text = lastAuthorName;
        }
    }

    private async void CreateButton_Click(object sender, RoutedEventArgs e)
    {
        if (!ValidateInput())
        {
            return;
        }

        try
        {
            var createDto = new CreateIssueDto
            {
                AuthorName = AuthorNameTextBox.Text,
                Category = string.IsNullOrWhiteSpace(CategoryTextBox.Text) ? null : CategoryTextBox.Text,
                Title = TitleTextBox.Text,
                Description = DescriptionTextBox.Text
            };

            await _apiService.CreateIssueAsync(createDto);
            
            SaveAuthorName(AuthorNameTextBox.Text);
            
            MessageBox.Show("Issue created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
            Close();
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show(
                $"Cannot connect to API server.\nPlease make sure the API project (Api.Rest.IssueBoard) is running.\n\nError: {ex.Message}", 
                "Connection Error", 
                MessageBoxButton.OK, 
                MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to create issue: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(AuthorNameTextBox.Text))
        {
            MessageBox.Show("AuthorName is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            AuthorNameTextBox.Focus();
            return false;
        }

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

        return true;
    }

    private void SaveAuthorName(string authorName)
    {
        try
        {
            Settings.Default.LastAuthorName = authorName;
            Settings.Default.Save();
        }
        catch
        {
            // Ignore save errors
        }
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
