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
	/// プレイリスト一覧のVM
	/// </summary>
	class PlaylistListViewViewModel : IPlaylistListViewViewModel
	{
		#region フィールド

		/// <summary>
		/// Disposeのタイミングに合わせてDisposeするリソースを登録する
		/// </summary>
		private readonly CompositeDisposable m_Disposables = new CompositeDisposable();

		/// <summary>
		/// YouTubeサービス
		/// </summary>
		private IYouTubeService m_YouTubeService;

		/// <summary>
		/// Webクライエントサービス
		/// </summary>
		private IWebClientService m_WebClientService;

		#endregion

		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="youTubeService">YouTubeサービス</param>
		public PlaylistListViewViewModel(IYouTubeService youTubeService, IWebClientService webClientService)
		{
			PlaylistList = new ReactiveCollection<PlaylistListViewItemViewModel>().AddTo(m_Disposables);
			SelectedItem = new ReactivePropertySlim<PlaylistListViewItemViewModel>().AddTo(m_Disposables);
			m_YouTubeService = youTubeService;
			m_WebClientService = webClientService;
		}

		#endregion

		#region プロパティ

		/// <summary>
		/// プレイリスト一覧
		/// </summary>
		public ReactiveCollection<PlaylistListViewItemViewModel> PlaylistList { get; }

		/// <summary>
		/// 選択されているプレイリスト
		/// </summary>
		public ReactivePropertySlim<PlaylistListViewItemViewModel> SelectedItem { get; set; }

		#endregion

		#region 公開サービス

		/// <summary>
		/// 初期化する
		/// </summary>
		internal async Task Initialize()
		{
			var playlists = await m_YouTubeService.GetMyPlaylists();
			foreach(var playlist in playlists)
			{
				PlaylistList.Add(new PlaylistListViewItemViewModel(playlist, m_WebClientService));
			}
		}

		#endregion
	}
}
