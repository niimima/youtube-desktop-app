namespace PlaylistEditor.ViewModels
{
    /// <summary>
    /// メインウインドウのVM
    /// </summary>
    internal class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
	{

		#region 構築

		/// <summary>
		/// コンストラクタ
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

		#region プロパティ

		/// <summary>
		/// プレイリスト一覧のVM
		/// </summary>
		public IPlaylistListViewViewModel PlaylistListViewViewModel { get; }

		/// <summary>
		/// プレイリストコンテンツのVM
		/// </summary>
		public IPlaylistContentViewViewModel PlaylistContentViewViewModel { get; }

		#endregion
	}
}
