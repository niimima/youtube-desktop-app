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
		/// <param name="searchResultViewViewModel"></param>
		/// <param name="searchFormViewViewModel"></param>
		public MainWindowViewModel(IPlaylistListViewViewModel playlistListViewViewModel,
			IPlaylistContentViewViewModel playlistContentViewViewModel,
			ISearchResultViewViewModel searchResultViewViewModel,
			ISearchFormViewViewModel searchFormViewViewModel)
		{
			PlaylistListViewViewModel = playlistListViewViewModel;
			PlaylistContentViewViewModel = playlistContentViewViewModel;
			SearchResultViewViewModel = searchResultViewViewModel;
			SearchFormViewViewModel = searchFormViewViewModel;
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

		/// <summary>
		/// �������ʃr���[��VM
		/// </summary>
		public ISearchResultViewViewModel SearchResultViewViewModel { get; }

		/// <summary>
		/// �����t�H�[����VM
		/// </summary>
		public ISearchFormViewViewModel SearchFormViewViewModel { get; }

		#endregion
	}
}
