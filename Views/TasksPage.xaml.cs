using TaskManager.ViewModels;

namespace TaskManager.Views;

public partial class TasksPage : ContentPage
{
    public TasksPage(TasksViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}

