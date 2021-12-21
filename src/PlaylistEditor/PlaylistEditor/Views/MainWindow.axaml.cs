using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using PlaylistEditor.ViewModels;
using PlaylistEditor.ViewModels.Dialogs;
using ReactiveUI;
using System;
using System.Reactive;
using System.Threading.Tasks;

namespace PlaylistEditor.Views
{
	/// <summary>
	/// メインウインドウ
	/// </summary>
	internal partial class MainWindow : ReactiveWindow<MainWindowViewModel>
	{
		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
			this.WhenActivated(d => d(ViewModel!.PlaylistListViewViewModel.ShowAddPlaylistDialog.RegisterHandler(DoShowDialogAsync)));
		}

		/// <summary>
		/// コンポーネントを初期化する。
		/// </summary>
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		#endregion

		#region 内部処理

		/// <summary>
		/// ダイアログを表示する。
		/// </summary>
		/// <param name="interaction">インタラクション</param>
		/// <remarks>
		/// 以下を参考にして作成。
		/// https://docs.avaloniaui.net/tutorials/music-store-app/opening-a-dialog
		/// </remarks>
		/// <returns></returns>
		private async Task DoShowDialogAsync(InteractionContext<Unit, AddPlaylistDialogViewModel> interaction)
		{
			// ダイアログを表示
			var dialog = new AddPlaylistDialog();
			var vm = new AddPlaylistDialogViewModel();
			dialog.DataContext = vm;
			await dialog.ShowDialog<Unit>(this);

			// 表示結果を設定
			interaction.SetOutput(vm);
		}

		#endregion
	}
}
