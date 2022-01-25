using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PlaylistEditor.ViewModels.Dialogs;

namespace PlaylistEditor.Views
{
	/// <summary>
	/// ���J����Ă���v���C���X�g�A�C�e���������̃v���C���X�g�A�C�e���Ƃ��Ēǉ�����_�C�A���O
	/// </summary>
	public partial class ClonePlaylistItemsDialog : Window
	{
		#region �\�z

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public ClonePlaylistItemsDialog()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif

			// �C�x���g�n���h���̓o�^
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

		#region �C�x���g�n���h��

		/// <summary>
		/// OK�{�^���̃N���b�N�n���h��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OkButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			((ClonePlaylistItemsDialogViewModel)DataContext!).Result = true;
			Close();
		}

		/// <summary>
		/// �L�����Z���{�^���̃N���b�N�n���h��
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
