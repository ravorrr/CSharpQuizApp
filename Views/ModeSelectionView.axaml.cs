using Avalonia.Controls;
using Avalonia.Interactivity;
using CSharpQuizApp.ViewModels;

namespace CSharpQuizApp.Views
{
    public partial class ModeSelectionView : UserControl
    {
        private MainWindow _mainWindow;

        public ModeSelectionView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void StartRandomQuiz_Click(object? sender, RoutedEventArgs e)
        {
            var viewModel = new RandomQuizViewModel();
            _mainWindow.NavigateToQuiz(viewModel);
        }

        private void StartAllQuestionsQuiz_Click(object? sender, RoutedEventArgs e)
        {
            var viewModel = new AllQuestionsQuizViewModel();
            _mainWindow.NavigateToQuiz(viewModel);
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