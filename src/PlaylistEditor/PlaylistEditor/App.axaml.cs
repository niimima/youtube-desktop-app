using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using PlaylistEditor.ViewModels;
using PlaylistEditor.Views;
using Splat;

namespace PlaylistEditor
{
	public class App : Application
	{
		public override void Initialize()
		{
			AvaloniaXamlLoader.Load(this);
		}

		public override void OnFrameworkInitializationCompleted()
		{
			if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
			{
				DataContext = Locator.Current.GetService<IMainWindowViewModel>();
				desktop.MainWindow = new MainWindow
				{
					DataContext = DataContext
				};
			}

			base.OnFrameworkInitializationCompleted();
		}
	}
}
