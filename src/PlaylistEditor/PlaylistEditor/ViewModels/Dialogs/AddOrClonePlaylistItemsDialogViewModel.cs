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
    /// 検索したチャンネルから動画・プレイリストアイテムを自分のプレイリストアイテムに追加するダイアログのVM
    /// </summary>
    class AddOrClonePlaylistItemsDialogViewModel : ViewModelBase
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
		public AddOrClonePlaylistItemsDialogViewModel(IYouTubeService youtubeService, IWebClientService webClientService)
		{
			m_YouTubeService = youtubeService;
			m_WebClientService = webClientService;

			SearchWord = new ReactivePropertySlim<string>().AddTo(m_Disposables);
			SearchResultList = new ReactiveCollection<ChannelViewModel>().AddTo(m_Disposables);
			TargetPlaylistList = new ReactiveCollection<PlaylistViewModel>().AddTo(m_Disposables);
			TargetVideoList = new ReactiveCollection<VideoViewModel>().AddTo(m_Disposables);
			SearchResultList = new ReactiveCollection<ChannelViewModel>().AddTo(m_Disposables);
			SelectedItem = new ReactivePropertySlim<ChannelViewModel>().AddTo(m_Disposables);
			ActiveTab = new ReactivePropertySlim<ItemType>().AddTo(m_Disposables);

			// 選択アイテム・タブが変更されたらプレイリスト・動画一覧を更新
			SelectedItem.Subscribe(_ => UpdateActiveTab());
			ActiveTab.Subscribe(_ => UpdateActiveTab());
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
		public ReactiveCollection<ChannelViewModel> SearchResultList { get; }

		/// <summary>
		/// 検索結果一覧で選択されたアイテム
		/// </summary>
		public ReactivePropertySlim<ChannelViewModel> SelectedItem { get; set; }

		/// <summary>
		/// アクティブなタブのアイテムタイプ
		/// </summary>
		public ReactivePropertySlim<ItemType> ActiveTab { get; set; }

		/// <summary>
		/// 検索結果一覧で選択されたアイテム一覧
		/// </summary>
		// public IEnumerable<Playlist> CheckedItems => SearchResultList.Where(item => item.IsChecked.Value).Select(item => item.Playlist);

		/// <summary>
		/// 検索結果一覧（プレイリスト）
		/// </summary>
		public ReactiveCollection<PlaylistViewModel> TargetPlaylistList { get; }

		/// <summary>
		/// 検索結果一覧で選択されたプレイリスト一覧
		/// </summary>
		public IEnumerable<Playlist> CheckedPlaylists => TargetPlaylistList.Where(item => item.IsChecked.Value).Select(item => item.Playlist);

		/// <summary>
		/// 検索結果一覧（動画）
		/// </summary>
		public ReactiveCollection<VideoViewModel> TargetVideoList { get; }

		/// <summary>
		/// 検索結果一覧で選択された動画一覧
		/// </summary>
		public IEnumerable<Video> CheckedVideos => TargetVideoList.Where(item => item.IsChecked.Value).Select(item => item.Video);

		/// <summary>
		/// 結果
		/// </summary>
		public bool Result { get; set; }

		#endregion

		#region 公開サービス

		/// <summary>
		/// チャンネルを検索する
		/// </summary>
		public async void Search()
		{
			var result = await m_YouTubeService.SearchChannel(SearchWord.Value);
			Update(result);
		}

		/// <summary>
		/// 検索結果を更新する
		/// </summary>
		/// <param name="playlists"></param>
		public void Update(IEnumerable<Channel> channels)
		{
			SearchResultList.Clear();
			foreach(var channel in channels)
			{
				SearchResultList.Add(new ChannelViewModel(channel, m_WebClientService));
			}
		}

        #endregion

        #region 内部処理

		/// <summary>
		/// アクティブなタブの要素一覧を更新する
		/// </summary>
		private async void UpdateActiveTab()
        {
			// 未選択の場合は何もしない
			if (SelectedItem.Value == null) return;

            switch (ActiveTab.Value)
            {
				case ItemType.Playlist:
					var playlists = await m_YouTubeService.SearchPlaylistByChannelId(SelectedItem.Value.Id);
					TargetPlaylistList.Clear();
                    foreach (var playlist in playlists)
                    {
						TargetPlaylistList.Add(new PlaylistViewModel(playlist, m_WebClientService));
                    }
					break;
				case ItemType.Video:
					var videos = await m_YouTubeService.SearchVideoByChannelId(SelectedItem.Value.Id);
					TargetPlaylistList.Clear();
                    foreach (var video in videos)
                    {
						TargetVideoList.Add(new VideoViewModel(video, m_WebClientService));
                    }
					break;
            }
        }

        #endregion
    }
}
