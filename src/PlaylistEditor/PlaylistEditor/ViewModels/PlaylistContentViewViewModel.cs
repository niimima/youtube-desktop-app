using PlaylistEditor.Models;
using PlaylistEditor.Services;
using PlaylistEditor.ViewModels.Interfaces;
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
	/// プレイリストコンテンツのVM
	/// </summary>
	internal class PlaylistContentViewViewModel : IPlaylistContentViewViewModel
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
		internal PlaylistContentViewViewModel(
			IYouTubeService youTubeService,
			IWebClientService webClientService,
			IPlaylistListViewViewModel playlistListViewViewModel,
			ISearchResultViewViewModel searchResultViewViewModel)
		{
			m_YouTubeService = youTubeService;
			m_WebClientService = webClientService;
			m_SearchResultViewViewModel = searchResultViewViewModel;
			playlistListViewViewModel.SelectionChanged += PlaylistListViewViewModel_SelectionChanged;

			Title = new ReactivePropertySlim<string>().AddTo(m_Disposables);
			Description = new ReactivePropertySlim<string>().AddTo(m_Disposables);
			PlaylistItemList = new ReactiveCollection<PlaylistContentViewItemViewModel>().AddTo(m_Disposables);
		}

		#endregion

		#region プロパティ

		/// <summary>
		/// タイトル
		/// </summary>
		public ReactivePropertySlim<string> Title { get; set; }

		/// <summary>
		/// 概要
		/// </summary>
		public ReactivePropertySlim<string> Description { get; set; }

		/// <summary>
		/// プレイリストアイテムの一覧
		/// </summary>
		public ReactiveCollection<PlaylistContentViewItemViewModel> PlaylistItemList { get; }

		/// <summary>
		/// プレイリスト
		/// </summary>
		private Playlist Playlist { get; set; }

		#endregion

		#region イベントハンドラ

		/// <summary>
		/// プレイリスト一覧の選択変更後イベントハンドラ
		/// </summary>
		/// <param name="itemViewModel"></param>
		private async void PlaylistListViewViewModel_SelectionChanged(Interfaces.IPlaylistListViewItemViewModel itemViewModel)
		{
			await UpdatePlaylistItemList(itemViewModel.Playlist);
		}

		/// <summary>
		/// プレイリストアイテムの一覧を更新する
		/// </summary>
		/// <param name="itemViewModel"></param>
		/// <returns></returns>
		private async Task UpdatePlaylistItemList(Playlist playlist)
		{
			Playlist = playlist;
			Title.Value = playlist.Title;
			Description.Value = playlist.Description;

			PlaylistItemList.Clear();
			var playlistItems = await m_YouTubeService.GetPlaylistItems(playlist.PlaylistId);
			foreach (var item in playlistItems)
			{
				PlaylistItemList.Add(new PlaylistContentViewItemViewModel(item, m_WebClientService));
			}
		}

		#endregion

		#region 公開サービス

		/// <summary>
		/// プレイリストアイテムを追加する
		/// </summary>
		public async Task AddVideosToPlaylistItemAsync()
		{
			var videos = m_SearchResultViewViewModel.CheckedItems;
			await m_YouTubeService.AddVideosToPlaylistItem(videos, Playlist);
			await UpdatePlaylistItemList(Playlist);
		}

		#endregion
	}
}
