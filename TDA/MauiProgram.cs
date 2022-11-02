

using Microsoft.Extensions.DependencyInjection;
using Plugin.Maui.Audio;
using TDA.ViewModels;

namespace TDA;

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
        builder.Services.AddSingleton(AudioManager.Current);
		builder.Services.AddTransient<MainPage>();

        return builder.Build();
	}
}
