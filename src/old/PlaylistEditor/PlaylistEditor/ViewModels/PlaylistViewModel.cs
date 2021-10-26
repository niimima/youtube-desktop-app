using Google.Apis.YouTube.v3.Data;
using PlaylistEditor.Services;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistEditor.ViewModels
{
	/// <summary>
	/// プレイリストのVM
	/// </summary>
	public class PlaylistViewModel
	{
		#region フィールド

		/// <summary>
		/// Disposeのタイミングに合わせてDisposeするリソースを登録する
		/// </summary>
		private readonly CompositeDisposable m_Disposables = new CompositeDisposable();

		#endregion

		#region プロパティ

		/// <summary>
		/// プレイリスト
		/// </summary>
		public Playlist Playlist;

		/// <summary>
		/// プレイリストに所属する要素のVMの一覧
		/// </summary>
		public ObservableCollection<PlaylistItemViewModel> PlaylistItemViewModels { get; } = new ObservableCollection<PlaylistItemViewModel>();


		/// <summary>
		/// プレイリストに所属する要素のVMの一覧で選択されたアイテム
		/// </summary>
		public ReactivePropertySlim<PlaylistItemViewModel> SelectedItem { get; set; }

		/// <summary>
		/// オーナーであるプレイリストエディタのVM
		/// </summary>
		public PlaylistEditorViewModel PlaylistEditorViewModel;

		/// <summary>
		/// プレイリストのタイトル
		/// </summary>
		public string Title => Playlist.Snippet.Title;

		/// <summary>
		/// プレイリストのID
		/// </summary>
		public string Id => Playlist.Id;

		#endregion

		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PlaylistViewModel(Playlist playlist, PlaylistEditorViewModel playlistEditorViewModel)
		{
			Playlist = playlist;
			PlaylistEditorViewModel = playlistEditorViewModel;
			SelectedItem = new ReactivePropertySlim<PlaylistItemViewModel>().AddTo(m_Disposables);
		}

		#endregion

		#region オーバーライド

		public override string ToString()
		{
			return $"{Playlist.Snippet.Title} ({Playlist.Id})";
		}

		#endregion

		#region 公開サービス

		/// <summary>
		/// プレイリストを削除する
		/// </summary>
		/// <returns></returns>
		public async Task DeletePlaylist()
		{
			await PlaylistEditorViewModel.DeletePlaylist(Id);
		}

		/// <summary>
		/// ビデオをプレイリストに追加する
		/// </summary>
		/// <param name="id">ID</param>
		/// <returns></returns>
		public async Task AddVideoToPlaylist(string id)
		{
			// 入力の検証
			var playlistVm = this;
			var videoId = id;
			if (playlistVm == null || string.IsNullOrEmpty(videoId)) return;

			// 以下を参考にプレイリストに指定の動画を追加
			// https://github.com/youtube/api-samples/blob/master/dotnet/Google.Apis.YouTube.Samples.Playlists/PlaylistUpdates.cs#L94
			var factory = new YoutubeServiceFactory();
			var service = await factory.Create();
			var playlistItem = new PlaylistItem();
			playlistItem.Snippet = new PlaylistItemSnippet();
			playlistItem.Snippet.PlaylistId = playlistVm.Id;
			playlistItem.Snippet.ResourceId = new ResourceId();
			playlistItem.Snippet.ResourceId.Kind = "youtube#video";
			playlistItem.Snippet.ResourceId.VideoId = videoId;
			playlistItem = await service.PlaylistItems.Insert(playlistItem, "snippet").ExecuteAsync();
			PlaylistItemViewModels.Add(new PlaylistItemViewModel(playlistItem, this));
		}


		/// <summary>
		/// プレイリストのアイテムを削除します
		/// </summary>
		/// <param name="id">ID</param>
		internal async void RemovePlaylistItem(string id)
		{
			var factory = new YoutubeServiceFactory();
			var service = await factory.Create();
			await service.PlaylistItems.Delete(id).ExecuteAsync();

			// VMからも一致するアイテムを削除する
			PlaylistItemViewModels.Remove(PlaylistItemViewModels.First(item => item.Id == id));
		}

		/// <summary>
		/// プレイリストのアイテムを追加します
		/// </summary>
		/// <param name="vm">ViewModel</param>
		/// <returns></returns>
		internal async void AddPlaylistItem(PlaylistItemViewModel vm)
		{
			// 以下を参考にプレイリストに指定の動画を追加
			// https://github.com/youtube/api-samples/blob/master/dotnet/Google.Apis.YouTube.Samples.Playlists/PlaylistUpdates.cs#L94
			var factory = new YoutubeServiceFactory();
			var service = await factory.Create();
			var playlistItem = new PlaylistItem();
			playlistItem.Snippet = new PlaylistItemSnippet();
			playlistItem.Snippet.PlaylistId = Playlist.Id;
			playlistItem.Snippet.ResourceId = vm.PlaylistItem.Snippet.ResourceId;
			playlistItem = await service.PlaylistItems.Insert(playlistItem, "snippet").ExecuteAsync();
			PlaylistItemViewModels.Add(new PlaylistItemViewModel(playlistItem, this));
		}

		#endregion
	}
}
