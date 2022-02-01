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

namespace PlaylistEditor.ViewModels.Dialogs
{
	/// <summary>
	/// 公開されているプレイリストアイテムを自分のプレイリストアイテムとして追加するダイアログのVM
	/// </summary>
	class ClonePlaylistItemsDialogViewModel : ViewModelBase
	{
		#region フィールド

		/// <summary>
		/// YouTubeサービス
		/// </summary>
		private IYouTubeService m_YouTubeService;

		/// <summary>
		/// Webクライエントサービス
		/// </summary>
		private IWebClientService m_WebClientService;

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
		/// <param name="webClientService">Webクライエントサービス</param>
		public ClonePlaylistItemsDialogViewModel(IYouTubeService youtubeService, IWebClientService webClientService)
		{
			m_YouTubeService = youtubeService;
			m_WebClientService = webClientService;

			SearchWord = new ReactivePropertySlim<string>().AddTo(m_Disposables);
			SearchResultList = new ReactiveCollection<PlaylistViewModel>().AddTo(m_Disposables);
			SelectedItem = new ReactivePropertySlim<PlaylistViewModel>().AddTo(m_Disposables);
		}

		#endregion

		#region プロパティ

		/// <summary>
		/// 検索ワード
		/// </summary>
		public ReactivePropertySlim<string> SearchWord { get; set; }

		/// <summary>
		/// 検索結果一覧
		/// </summary>
		public ReactiveCollection<PlaylistViewModel> SearchResultList { get; }

		/// <summary>
		/// 検索結果一覧で選択されたアイテム
		/// </summary>
		public ReactivePropertySlim<PlaylistViewModel> SelectedItem { get; set; }

		/// <summary>
		/// 検索結果一覧で選択されたアイテム一覧
		/// </summary>
		public IEnumerable<Playlist> CheckedItems => SearchResultList.Where(item => item.IsChecked.Value).Select(item => item.Playlist);

		/// <summary>
		/// 結果
		/// </summary>
		public bool Result { get; set; }

		#endregion

		#region 公開サービス

		/// <summary>
		/// 動画を検索する
		/// </summary>
		public async void Search()
		{
			var result = await m_YouTubeService.SearchPlaylist(SearchWord.Value);
			Update(result);
		}

		/// <summary>
		/// 検索結果を更新する
		/// </summary>
		/// <param name="playlists"></param>
		public void Update(IEnumerable<Playlist> playlists)
		{
			SearchResultList.Clear();
			foreach(var playlist in playlists)
			{
				SearchResultList.Add(new PlaylistViewModel(playlist, m_WebClientService));
			}
		}

		#endregion
	}
}
