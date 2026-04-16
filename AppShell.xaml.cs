using TaskManager.Views;

namespace TaskManager;

public partial class AppShell : Shell
{
	public AppShell(TasksPage tasksPage)
	{
		InitializeComponent();

		Items.Add(new ShellContent
		{
			Title = "Tâches",
			Content = tasksPage
		});

		Routing.RegisterRoute(nameof(TaskDetailPage), typeof(TaskDetailPage));
	}
}
