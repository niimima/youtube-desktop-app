using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using PlaylistEditor.ViewModels;
using PlaylistEditor.Views;

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
                var vm = new MainWindowViewModel();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = vm,
                };

                // コンストラクタ内で非同期処理ができないため、vm生成後にプレイリストを取得する非同期処理を実行する
				_ = vm.PlaylistEditorViewModel.GetPlaylist();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
