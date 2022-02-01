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

		#region プロパティ

		/// <summary>
		/// プレイリスト一覧のVM
		/// </summary>
		public IPlaylistListViewViewModel PlaylistListViewViewModel { get; }

		/// <summary>
		/// プレイリストコンテンツのVM
		/// </summary>
		public IPlaylistContentViewViewModel PlaylistContentViewViewModel { get; }

		/// <summary>
		/// 検索結果ビューのVM
		/// </summary>
		public ISearchResultViewViewModel SearchResultViewViewModel { get; }

		/// <summary>
		/// 検索フォームのVM
		/// </summary>
		public ISearchFormViewViewModel SearchFormViewViewModel { get; }

		#endregion
	}
}
