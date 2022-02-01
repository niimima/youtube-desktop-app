using PlaylistEditor.Models;
using PlaylistEditor.Services;
using PlaylistEditor.ViewModels.Dialogs;
using PlaylistEditor.ViewModels.Interfaces;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
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
		/// プレイリスト一覧のVM
		/// </summary>
		private IPlaylistListViewViewModel m_PlaylistListViewViewModel;

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
			IPlaylistListViewViewModel playlistListViewViewModel)
		{
			m_YouTubeService = youTubeService;
			m_WebClientService = webClientService;
            m_PlaylistListViewViewModel = playlistListViewViewModel;
			playlistListViewViewModel.SelectionChanged += PlaylistListViewViewModel_SelectionChanged;

			Title = new ReactivePropertySlim<string>().AddTo(m_Disposables);
			Description = new ReactivePropertySlim<string>().AddTo(m_Disposables);
			PlaylistItemList = new ReactiveCollection<PlaylistContentViewItemViewModel>().AddTo(m_Disposables);

			Playlist = new ReactivePropertySlim<Playlist?>().AddTo(m_Disposables);
			CanAddVideosToPlaylistItemAsync = new ReactivePropertySlim<bool>().AddTo(m_Disposables);
			CanClonePlaylistItemsFromPlaylist = new ReactivePropertySlim<bool>().AddTo(m_Disposables);
			Playlist.Subscribe(v => {
				CanAddVideosToPlaylistItemAsync.Value = (v != null);
				CanClonePlaylistItemsFromPlaylist.Value = (v != null);
				});
			AddVideosToPlaylistItemAsyncCommand = CanAddVideosToPlaylistItemAsync
				.ToReactiveCommand()
				.WithSubscribe(async () => await AddVideosToPlaylistItemAsync())
				.AddTo(m_Disposables);
			ClonePlaylistItemsFromPlaylistCommand = CanClonePlaylistItemsFromPlaylist
				.ToReactiveCommand()
				.WithSubscribe(async () => await ClonePlaylistItemsFromPlaylist())
				.AddTo(m_Disposables);

			// ダイアログを表示するインタラクションを保持
			ShowAddPlaylistItemDialog = new Interaction<Unit, AddPlaylistItemDialogViewModel>();
			ShowClonePlaylistItemsDialog = new Interaction<Unit, ClonePlaylistItemsDialogViewModel>();
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
		/// 動画をプレイリストアイテムに追加できるか
		/// </summary>
		public ReactivePropertySlim<bool> CanAddVideosToPlaylistItemAsync { get; }

		/// <summary>
		/// 公開されているプレイリストアイテムを自分のプレイリストアイテムとして追加できるか
		/// </summary>
		public ReactivePropertySlim<bool> CanClonePlaylistItemsFromPlaylist { get; }

		/// <summary>
		/// プレイリスト
		/// </summary>
		private ReactivePropertySlim<Playlist?> Playlist { get; set; }

		/// <summary>
		/// プレイリスト一覧
		/// </summary>
		public ReactiveCollection<IPlaylistListViewItemViewModel> PlaylistList => m_PlaylistListViewViewModel.PlaylistList;

		/// <summary>
		/// プレイリストを追加するダイアログを表示するインタラクション
		/// </summary>
		public Interaction<Unit, AddPlaylistItemDialogViewModel> ShowAddPlaylistItemDialog { get; }

		/// <summary>
		/// 公開されているプレイリストアイテムを自分のプレイリストアイテムとして追加するダイアログを表示するインタラクション
		/// </summary>
		public Interaction<Unit, ClonePlaylistItemsDialogViewModel> ShowClonePlaylistItemsDialog { get; }

		/// <summary>
		/// 動画をプレイリストアイテムに追加するコマンド
		/// </summary>
		public Reactive.Bindings.ReactiveCommand AddVideosToPlaylistItemAsyncCommand { get; }

		/// <summary>
		/// 公開されているプレイリストアイテムを自分のプレイリストアイテムとして追加するコマンド
		/// </summary>
		public Reactive.Bindings.ReactiveCommand ClonePlaylistItemsFromPlaylistCommand { get; }

		#endregion

		#region イベントハンドラ

		/// <summary>
		/// プレイリスト一覧の選択変更後イベントハンドラ
		/// </summary>
		/// <param name="itemViewModel"></param>
		private async void PlaylistListViewViewModel_SelectionChanged(Interfaces.IPlaylistListViewItemViewModel itemViewModel)
		{
			await UpdatePlaylistItemList(itemViewModel?.Playlist);
		}

		/// <summary>
		/// プレイリストアイテムの一覧を更新する
		/// </summary>
		/// <param name="itemViewModel"></param>
		/// <returns></returns>
		private async Task UpdatePlaylistItemList(Playlist? playlist)
		{
			PlaylistItemList.Clear();
			Playlist.Value = playlist;
			if (playlist == null) return;

			Title.Value = playlist.Title;
			Description.Value = playlist.Description;

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
			// ダイアログを表示
			var resultVm = await ShowAddPlaylistItemDialog.Handle(Unit.Default);
			if (resultVm.Result == false) return;

			var videos = resultVm.CheckedItems;
			await m_YouTubeService.AddVideosToPlaylistItem(videos, Playlist.Value!);
			await UpdatePlaylistItemList(Playlist.Value);
		}

		/// <summary>
		/// 公開されているプレイリストアイテムを自分のプレイリストアイテムとして追加する
		/// </summary>
		public async Task ClonePlaylistItemsFromPlaylist()
		{
			// ダイアログを表示
			var resultVm = await ShowClonePlaylistItemsDialog.Handle(Unit.Default);
			if (resultVm.Result == false) return;

			var playlists = resultVm.CheckedItems;
			var videos = new List<Video>();
			foreach (var playlist in playlists)
			{
				var playlistItems = await m_YouTubeService.GetPlaylistItems(playlist.PlaylistId);
				foreach (var playlistItem in playlistItems)
				{
					videos.Add(new Video(playlistItem.ResourcesId.VideoId, playlistItem.Title, playlistItem.Description, playlistItem.ThumbnailUrl));
				}
			}

			await m_YouTubeService.AddVideosToPlaylistItem(videos, Playlist.Value!);
			await UpdatePlaylistItemList(Playlist.Value);
		}

		/// <summary>
		/// プレイリストアイテムを削除する
		/// </summary>
		/// <returns></returns>
		public async Task RemovePlaylistItemAsync()
		{
			var selectedItemIds = PlaylistItemList.Where(item => item.IsChecked.Value).Select(item => item.Id);
			await m_YouTubeService.RemovePlaylistItems(selectedItemIds);
			await UpdatePlaylistItemList(Playlist.Value);
		}

		/// <summary>
		/// プレイリストアイテムを移動する
		/// </summary>
		/// <returns></returns>
		public async Task MovePlaylistItemAsync(IPlaylistListViewItemViewModel playlist)
		{
			var selectedItemIds = PlaylistItemList.Where(item => item.IsChecked.Value).Select(item => item.Id);
			var selectedItemResourceIds = PlaylistItemList.Where(item => item.IsChecked.Value).Select(item => item.ResourcesId);
			await m_YouTubeService.MovePlaylistItems(selectedItemIds, selectedItemResourceIds, playlist.Id);
			await UpdatePlaylistItemList(Playlist.Value);
		}

		#endregion
	}
}
