using Avalonia.Controls;
using Avalonia.Interactivity;

namespace QuizApp.Views
{
    public partial class ModeSelectionView : UserControl
    {
        private readonly MainWindow _mainWindow;

        public ModeSelectionView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void StartRandomQuiz_Click(object? sender, RoutedEventArgs e)
        {
            _mainWindow.StartRandomQuiz();
        }

        private void StartAllQuestionsQuiz_Click(object? sender, RoutedEventArgs e)
        {
            _mainWindow.StartAllQuestionsQuiz();
        }
        
        private void StartSurvivalQuiz_Click(object? sender, RoutedEventArgs e)
        {
            _mainWindow.StartSurvivalQuiz();
        }
        
        private void ShowCategorySelection_Click(object? sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToCategorySelection();
        }
        
        private void BackToMenu_Click(object? sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToStart();
        }
    }
}