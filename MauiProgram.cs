using Microsoft.Extensions.Logging;
using TaskManager.Services;
using TaskManager.ViewModels;
using TaskManager.Views;

namespace TaskManager;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<AppShell>();
		builder.Services.AddSingleton<TasksRepository>();
		builder.Services.AddTransient<TasksViewModel>();
		builder.Services.AddTransient<TaskDetailViewModel>();
		builder.Services.AddTransient<AddTaskViewModel>();
		builder.Services.AddTransient<TasksPage>();
		builder.Services.AddTransient<TaskDetailPage>();
		builder.Services.AddTransient<AddTaskPage>();

		return builder.Build();
	}
}
