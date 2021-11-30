using PlaylistEditor.Models;
using PlaylistEditor.Services;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;

namespace PlaylistEditor.ViewModels
{
	/// <summary>
	/// 検索結果ビューのVM
	/// </summary>
	class SearchResultViewViewModel : ISearchResultViewViewModel
	{
		#region フィールド

		/// <summary>
		/// Disposeのタイミングに合わせてDisposeするリソースを登録する
		/// </summary>
		private readonly CompositeDisposable m_Disposables = new CompositeDisposable();

		/// <summary>
		/// Webクライエントサービス
		/// </summary>
		private IWebClientService m_WebClientService;

		#endregion

		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="webClientService"></param>
		public SearchResultViewViewModel(IWebClientService webClientService)
		{
			m_WebClientService = webClientService;
			SearchResultList = new ReactiveCollection<SearchResultViewItemViewModel>().AddTo(m_Disposables);
			SelectedItem = new ReactivePropertySlim<SearchResultViewItemViewModel>().AddTo(m_Disposables);
		}

		#endregion

		#region プロパティ

		/// <summary>
		/// 検索結果一覧
		/// </summary>
		public ReactiveCollection<SearchResultViewItemViewModel> SearchResultList { get; }

		/// <summary>
		/// 検索結果一覧で選択されたアイテム
		/// </summary>
		public ReactivePropertySlim<SearchResultViewItemViewModel> SelectedItem { get; set; }

		/// <summary>
		/// 検索結果一覧で選択されたアイテム一覧
		/// </summary>
		public IEnumerable<Video> CheckedItems => SearchResultList.Where(item => item.IsChecked.Value).Select(item => item.Video);

		#endregion

		#region 公開サービス

		/// <inheritdoc/>
		public void Update(IEnumerable<Video> videos)
		{
			SearchResultList.Clear();
			foreach(var video in videos)
			{
				SearchResultList.Add(new SearchResultViewItemViewModel(video, m_WebClientService));
			}
		}

		#endregion
	}
}
