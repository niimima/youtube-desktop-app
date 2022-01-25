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
using Splat;
using PlaylistEditor.Services;

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
			this.WhenActivated(d => d(ViewModel!.PlaylistContentViewViewModel.ShowAddPlaylistItemDialog.RegisterHandler(DoShowAddPlaylistItemDialogAsync)));
			this.WhenActivated(d => d(ViewModel!.PlaylistContentViewViewModel.ShowClonePlaylistItemsDialog.RegisterHandler(DoShowShowClonePlaylistItemsDialogAsync)));
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

		/// <summary>
		/// プレイリストアイテムを追加ダイアログを表示する。
		/// </summary>
		/// <param name="interaction">インタラクション</param>
		/// <returns></returns>
		private async Task DoShowAddPlaylistItemDialogAsync(InteractionContext<Unit, AddPlaylistItemDialogViewModel> interaction)
		{
			// ダイアログを表示
			var dialog = new AddPlaylistItemDialog();
			// TODO うまく注入するような実装にしたいが、できていない
			var vm = new AddPlaylistItemDialogViewModel(Locator.Current.GetService<IYouTubeService>(), Locator.Current.GetService<IWebClientService>());
			dialog.DataContext = vm;
			await dialog.ShowDialog<Unit>(this);

			// 表示結果を設定
			interaction.SetOutput(vm);
		}

		/// <summary>
		/// 公開されているプレイリストアイテムを自分のプレイリストアイテムとして追加するダイアログを表示する。
		/// </summary>
		/// <param name="interaction">インタラクション</param>
		/// <returns></returns>
		private async Task DoShowShowClonePlaylistItemsDialogAsync(InteractionContext<Unit, ClonePlaylistItemsDialogViewModel> interaction)
		{
			// ダイアログを表示
			var dialog = new ClonePlaylistItemsDialog();
			// TODO うまく注入するような実装にしたいが、できていない
			var vm = new ClonePlaylistItemsDialogViewModel(Locator.Current.GetService<IYouTubeService>(), Locator.Current.GetService<IWebClientService>());
			dialog.DataContext = vm;
			await dialog.ShowDialog<Unit>(this);

			// 表示結果を設定
			interaction.SetOutput(vm);
		}

		#endregion
	}
}
