using Microsoft.Extensions.DependencyInjection.Extensions;
using NotificationToDo.Data;
using NotificationToDo.Views;
using Plugin.LocalNotification;

namespace NotificationToDo;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
            .UseMauiApp<App>()
            .UseLocalNotification(c =>
            {
                c.AddAndroid(a =>
                {
                    a.AddChannel(new Plugin.LocalNotification.AndroidOption.NotificationChannelRequest());
                });
            })
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<TodoListPage>();
		builder.Services.AddTransient<TodoItemPage>();

		builder.Services.AddSingleton<TodoItemDatabase>();

		return builder.Build();

		
	}
}
