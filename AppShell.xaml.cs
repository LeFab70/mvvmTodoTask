using TaskManager.Views;

namespace TaskManager;

public partial class AppShell : Shell
{
	public AppShell(TasksPage tasksPage, AddTaskPage addTaskPage)
	{
		InitializeComponent();

		var tabBar = new TabBar();
		tabBar.Items.Add(new ShellContent
		{
			Title = "Accueil",
			Content = tasksPage
		});
		tabBar.Items.Add(new ShellContent
		{
			Title = "Ajouter",
			Content = addTaskPage
		});

		Items.Add(tabBar);

		Routing.RegisterRoute(nameof(TaskDetailPage), typeof(TaskDetailPage));
	}
}
