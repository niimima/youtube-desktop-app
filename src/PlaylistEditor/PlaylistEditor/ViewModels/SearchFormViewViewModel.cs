using PlaylistEditor.Services;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Disposables;

namespace PlaylistEditor.ViewModels
{
	/// <summary>
	/// 検索フォームビューのVM
	/// </summary>
	class SearchFormViewViewModel : ViewModelBase, ISearchFormViewViewModel
	{
		#region フィールド

		/// <summary>
		/// YouTubeサービス
		/// </summary>
		private IYouTubeService m_YouTubeService;

		// TODO イベントアグリゲータを使ってフィールドに持たせないようにしたい
		/// <summary>
		/// 検索結果ビューのVM
		/// </summary>
		private ISearchResultViewViewModel m_SearchResultViewViewModel;

		/// <summary>
		/// Disposeのタイミングに合わせてDisposeするリソースを登録する
		/// </summary>
		private readonly CompositeDisposable m_Disposables = new CompositeDisposable();

		#endregion

		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="youtubeService">YouTubeサービス</param>
		/// <param name="searchResultViewViewModel">検索結果ビューのVM</param>
		public SearchFormViewViewModel(IYouTubeService youtubeService, ISearchResultViewViewModel searchResultViewViewModel)
		{
			m_YouTubeService = youtubeService;
			m_SearchResultViewViewModel = searchResultViewViewModel;
			SearchWord = new ReactivePropertySlim<string>().AddTo(m_Disposables);
		}

		#endregion

		#region プロパティ

		/// <summary>
		/// 検索ワード
		/// </summary>
		public ReactivePropertySlim<string> SearchWord { get; set; }

		#endregion

		#region 公開サービス

		/// <summary>
		/// 動画を検索する
		/// </summary>
		public async void Search()
		{
			var result = await m_YouTubeService.SearchVideo(SearchWord.Value);
			m_SearchResultViewViewModel.Update(result);
		}

		#endregion
	}
}
