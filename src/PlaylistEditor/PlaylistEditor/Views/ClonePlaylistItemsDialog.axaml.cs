using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PlaylistEditor.ViewModels.Dialogs;

namespace PlaylistEditor.Views
{
	/// <summary>
	/// 公開されているプレイリストアイテムを自分のプレイリストアイテムとして追加するダイアログ
	/// </summary>
	public partial class ClonePlaylistItemsDialog : Window
	{
		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ClonePlaylistItemsDialog()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif

			// イベントハンドラの登録
			var okButton = this.FindControl<Button>("m_OKButton");
			var cancelButton = this.FindControl<Button>("m_CancelButton");

			okButton.Click += OkButton_Click;
			cancelButton.Click += CancelButton_Click;
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		#endregion

		#region イベントハンドラ

		/// <summary>
		/// OKボタンのクリックハンドラ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OkButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			((ClonePlaylistItemsDialogViewModel)DataContext!).Result = true;
			Close();
		}

		/// <summary>
		/// キャンセルボタンのクリックハンドラ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CancelButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			Close();
		}

		#endregion
	}
}
