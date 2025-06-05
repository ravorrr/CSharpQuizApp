using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Threading;
using CSharpQuizApp.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CSharpQuizApp.Views;

namespace CSharpQuizApp.ViewModels;

public class CategorySelectionViewModel : ObservableObject
{
    private readonly MainWindow _mainWindow;

    public ObservableCollection<string> Categories { get; } = new();

    public ICommand SelectCategoryCommand { get; }

    public CategorySelectionViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        SelectCategoryCommand = new RelayCommand<string>(OnCategorySelected);

        LoadCategories();
    }

    private void LoadCategories()
    {
        var categories = QuizDatabase.LoadCategories();
        Categories.Clear();
        foreach (var category in categories)
            Categories.Add(category);
    }

    private void OnCategorySelected(string category)
    {
        // Stwórz ViewModel quizu dla wybranej kategorii
        var quizViewModel = new CategoryQuizViewModel(category);

        // Przejdź do widoku quizu z tym ViewModel
        Dispatcher.UIThread.Post(() =>
        {
            _mainWindow.NavigateToQuiz(quizViewModel);
        });
    }
}