using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using PlaylistEditor.ViewModels;
using PlaylistEditor.ViewModels.Dialogs;
using ReactiveUI;
using System;
using System.Threading.Tasks;

namespace PlaylistEditor.Views
{
	internal partial class MainWindow : ReactiveWindow<MainWindowViewModel>
	{
		public MainWindow()
		{
			InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
			this.WhenActivated(d => d(ViewModel!.PlaylistListViewViewModel.ShowAddPlaylistDialog.RegisterHandler(DoShowDialogAsync)));
		}

		private async Task DoShowDialogAsync(InteractionContext<AddPlaylistDialogViewModel, bool> interaction)
		{
			var dialog = new AddPlaylistDialog();
			dialog.DataContext = interaction.Input;

			var result = await dialog.ShowDialog<bool>(this);
			interaction.SetOutput(result);
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
