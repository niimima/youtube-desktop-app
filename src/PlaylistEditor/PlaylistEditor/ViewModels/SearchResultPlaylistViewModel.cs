using PlaylistEditor.Models;
using PlaylistEditor.Services;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistEditor.ViewModels
{
	/// <summary>
	/// 検索結果のプレイリストのVM
	/// </summary>
	class SearchResultPlaylistViewModel : ViewModelBase
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
		public SearchResultPlaylistViewModel(IWebClientService webClientService)
		{
			m_WebClientService = webClientService;
			SearchResultList = new ReactiveCollection<SearchResultPlaylistItemViewModel>().AddTo(m_Disposables);
			SelectedItem = new ReactivePropertySlim<SearchResultPlaylistItemViewModel>().AddTo(m_Disposables);
		}

		#endregion

		#region プロパティ

		/// <summary>
		/// 検索結果一覧
		/// </summary>
		public ReactiveCollection<SearchResultPlaylistItemViewModel> SearchResultList { get; }

		/// <summary>
		/// 検索結果一覧で選択されたアイテム
		/// </summary>
		public ReactivePropertySlim<SearchResultPlaylistItemViewModel> SelectedItem { get; set; }

		/// <summary>
		/// 検索結果一覧で選択されたアイテム一覧
		/// </summary>
		public IEnumerable<Playlist> CheckedItems => SearchResultList.Where(item => item.IsChecked.Value).Select(item => item.Playlist);

		#endregion

		#region 公開サービス

		/// <inheritdoc/>
		public void Update(IEnumerable<Playlist> playlists)
		{
			SearchResultList.Clear();
			foreach(var playlist in playlists)
			{
				SearchResultList.Add(new SearchResultPlaylistItemViewModel(playlist, m_WebClientService));
			}
		}

		#endregion
	}
}
