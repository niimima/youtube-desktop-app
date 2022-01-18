using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PlaylistEditor.ViewModels.Dialogs;

namespace PlaylistEditor.Views
{
	/// <summary>
	/// プレイリストアイテムを追加ダイアログ
	/// </summary>
	public partial class AddPlaylistItemDialog : Window
	{
		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public AddPlaylistItemDialog()
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
			((AddPlaylistItemDialogViewModel)DataContext!).Result = true;
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
