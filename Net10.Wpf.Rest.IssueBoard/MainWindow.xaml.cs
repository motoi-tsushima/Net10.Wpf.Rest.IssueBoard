using Net10.Wpf.Rest.IssueBoard.Services;
using Net10.Wpf.Rest.IssueBoard.Views;
using Shared.Rest.IssueBoard;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

namespace Net10.Wpf.Rest.IssueBoard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IssueApiService _apiService;

        public MainWindow()
        {
            InitializeComponent();
            _apiService = new IssueApiService();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadIssuesAsync();
        }

        private async Task LoadIssuesAsync()
        {
            try
            {
                var issues = await _apiService.GetAllIssuesAsync();
                IssuesDataGrid.ItemsSource = issues;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
                    $"APIサーバーに接続できません。\nAPIプロジェクト（Api.Rest.IssueBoard）が https://localhost:7270 で実行されていることを確認してください。\n\nエラー: {ex.Message}", 
                    "接続エラー", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"課題のロードに失敗しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void IssuesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (IssuesDataGrid.SelectedItem is IssueDto selectedIssue)
            {
                var detailWindow = new IssueDetailWindow(selectedIssue.Id);
                detailWindow.Owner = this;
                detailWindow.ShowDialog();
                await LoadIssuesAsync();
            }
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var createWindow = new IssueCreateWindow();
            createWindow.Owner = this;
            if (createWindow.ShowDialog() == true)
            {
                await LoadIssuesAsync();
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}