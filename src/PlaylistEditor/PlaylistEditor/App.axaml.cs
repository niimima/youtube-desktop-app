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

                // �R���X�g���N�^���Ŕ񓯊��������ł��Ȃ����߁Avm������Ƀv���C���X�g���擾����񓯊����������s����
				_ = vm.PlaylistEditorViewModel.GetPlaylist();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
