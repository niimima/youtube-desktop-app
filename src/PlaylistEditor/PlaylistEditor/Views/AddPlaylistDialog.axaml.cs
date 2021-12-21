using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PlaylistEditor.Views
{
	/// <summary>
	/// プレイリストを追加ダイアログ
	/// </summary>
	public partial class AddPlaylistDialog : Window
	{
		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public AddPlaylistDialog()
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

		/// <summary>
		/// コンポーネントの初期化
		/// </summary>
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		#endregion

		/// <summary>
		/// OKボタンのクリックハンドラ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OkButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
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
	}
}
