namespace PlaylistEditor.ViewModels
{
    /// <summary>
    /// ���C���E�C���h�E��VM
    /// </summary>
    internal class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
	{

		#region �\�z

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="playlistListViewViewModel"></param>
		/// <param name="playlistContentViewViewModel"></param>
		public MainWindowViewModel(IPlaylistListViewViewModel playlistListViewViewModel,
			IPlaylistContentViewViewModel playlistContentViewViewModel)
		{
			PlaylistListViewViewModel = playlistListViewViewModel;
			PlaylistContentViewViewModel = playlistContentViewViewModel;
		}

		#endregion

		#region �v���p�e�B

		/// <summary>
		/// �v���C���X�g�ꗗ��VM
		/// </summary>
		public IPlaylistListViewViewModel PlaylistListViewViewModel { get; }

		/// <summary>
		/// �v���C���X�g�R���e���c��VM
		/// </summary>
		public IPlaylistContentViewViewModel PlaylistContentViewViewModel { get; }

		#endregion
	}
}
