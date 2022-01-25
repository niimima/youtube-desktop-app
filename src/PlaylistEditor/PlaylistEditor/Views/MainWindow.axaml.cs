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
	/// ���C���E�C���h�E
	/// </summary>
	internal partial class MainWindow : ReactiveWindow<MainWindowViewModel>
	{
		#region �\�z

		/// <summary>
		/// �R���X�g���N�^
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
		/// �R���|�[�l���g������������B
		/// </summary>
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		#endregion

		#region ��������

		/// <summary>
		/// �_�C�A���O��\������B
		/// </summary>
		/// <param name="interaction">�C���^���N�V����</param>
		/// <remarks>
		/// �ȉ����Q�l�ɂ��č쐬�B
		/// https://docs.avaloniaui.net/tutorials/music-store-app/opening-a-dialog
		/// </remarks>
		/// <returns></returns>
		private async Task DoShowDialogAsync(InteractionContext<Unit, AddPlaylistDialogViewModel> interaction)
		{
			// �_�C�A���O��\��
			var dialog = new AddPlaylistDialog();
			var vm = new AddPlaylistDialogViewModel();
			dialog.DataContext = vm;
			await dialog.ShowDialog<Unit>(this);

			// �\�����ʂ�ݒ�
			interaction.SetOutput(vm);
		}

		/// <summary>
		/// �v���C���X�g�A�C�e����ǉ��_�C�A���O��\������B
		/// </summary>
		/// <param name="interaction">�C���^���N�V����</param>
		/// <returns></returns>
		private async Task DoShowAddPlaylistItemDialogAsync(InteractionContext<Unit, AddPlaylistItemDialogViewModel> interaction)
		{
			// �_�C�A���O��\��
			var dialog = new AddPlaylistItemDialog();
			// TODO ���܂���������悤�Ȏ����ɂ��������A�ł��Ă��Ȃ�
			var vm = new AddPlaylistItemDialogViewModel(Locator.Current.GetService<IYouTubeService>(), Locator.Current.GetService<IWebClientService>());
			dialog.DataContext = vm;
			await dialog.ShowDialog<Unit>(this);

			// �\�����ʂ�ݒ�
			interaction.SetOutput(vm);
		}

		/// <summary>
		/// ���J����Ă���v���C���X�g�A�C�e���������̃v���C���X�g�A�C�e���Ƃ��Ēǉ�����_�C�A���O��\������B
		/// </summary>
		/// <param name="interaction">�C���^���N�V����</param>
		/// <returns></returns>
		private async Task DoShowShowClonePlaylistItemsDialogAsync(InteractionContext<Unit, ClonePlaylistItemsDialogViewModel> interaction)
		{
			// �_�C�A���O��\��
			var dialog = new ClonePlaylistItemsDialog();
			// TODO ���܂���������悤�Ȏ����ɂ��������A�ł��Ă��Ȃ�
			var vm = new ClonePlaylistItemsDialogViewModel(Locator.Current.GetService<IYouTubeService>(), Locator.Current.GetService<IWebClientService>());
			dialog.DataContext = vm;
			await dialog.ShowDialog<Unit>(this);

			// �\�����ʂ�ݒ�
			interaction.SetOutput(vm);
		}

		#endregion
	}
}
