using PlaylistEditor.Models;
using PlaylistEditor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		}

		#endregion

		#region 公開サービス

		/// <summary>
		/// 動画を検索する
		/// </summary>
		public async void Search(string searchWord)
		{
			var result = await m_YouTubeService.SearchVideo(searchWord);
			m_SearchResultViewViewModel.Update(result);
		}

		#endregion
	}
}
