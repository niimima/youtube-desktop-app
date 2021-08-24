using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3.Data;
using PlaylistEditor.Services;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace PlaylistEditor.ViewModels
{
	/// <summary>
	/// プレイリストエディタのVM
	/// </summary>
	public class PlaylistEditorViewModel : ViewModelBase
	{
		#region フィールド

		/// <summary>
		/// Disposeのタイミングに合わせてDisposeするリソースを登録する
		/// </summary>
		private readonly CompositeDisposable m_Disposables = new CompositeDisposable();

		#endregion

		#region プロパティ

		/// <summary>
		/// プレイリスト一覧
		/// </summary>
		public ObservableCollection<PlaylistViewModel> PlaylistViewModels { get; } = new ObservableCollection<PlaylistViewModel>();

		/// <summary>
		/// 選択されているプレイリスト
		/// </summary>
		public ReactivePropertySlim<PlaylistViewModel> SelectedPlaylistViewModel { get; }

		/// <summary>
		/// 追加するプレイリスト名
		/// </summary>
		public ReactivePropertySlim<string> AddPlaylistTitle { get; }

		/// <summary>
		/// 追加するプレイリストの説明
		/// </summary>
		public ReactivePropertySlim<string> AddPlaylistDescription { get; }

		/// <summary>
		/// 追加する動画のID
		/// </summary>
		public ReactivePropertySlim<string> AddVideoId { get; }

		#endregion

		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PlaylistEditorViewModel()
		{
			AddPlaylistTitle = new ReactivePropertySlim<string>().AddTo(m_Disposables);
			AddPlaylistDescription = new ReactivePropertySlim<string>().AddTo(m_Disposables);
			AddVideoId = new ReactivePropertySlim<string>().AddTo(m_Disposables);
			SelectedPlaylistViewModel = new ReactivePropertySlim<PlaylistViewModel>().AddTo(m_Disposables);
		}

		#endregion

		#region 公開サービス

		/// <summary>
		/// プレイリストを取得する
		/// </summary>
		/// <returns></returns>
		public async Task GetPlaylist()
		{
			// Playlistをクリアする
			PlaylistViewModels.Clear();

			var factory = new YoutubeServiceFactory();
			var service = await factory.Create();
			var newPlaylist = service.Playlists.List("snippet");
			var newPlaylistItem = service.PlaylistItems.List("snippet");
			// 100件まで編集可能とする
			newPlaylistItem.MaxResults = 100;
			// チャンネルIDを指定することでも取得可能
			// newPlaylist.ChannelId = "UCpkkP5J-16g3zgfuIihCTrA";
			newPlaylist.Mine = true;
			var list = await newPlaylist.ExecuteAsync();
			foreach (var playlist in list.Items)
			{
				var playlistVm = new PlaylistViewModel(playlist, this);
				PlaylistViewModels.Add(playlistVm);

				newPlaylistItem.PlaylistId = playlist.Id;
				var item = await newPlaylistItem.ExecuteAsync();
				foreach (var playlistItem in item.Items)
				{
					playlistVm.PlaylistItemViewModels.Add(new PlaylistItemViewModel(playlistItem, playlistVm));
				}
			}
		}

		/// <summary>
		/// プレイリストを追加する
		/// </summary>
		/// <returns></returns>
		public async Task AddPlaylist()
		{
			// 入力の検証
			if (string.IsNullOrEmpty(AddPlaylistTitle.Value)) return;

			// 入力結果からプレイリストを追加する
			var factory = new YoutubeServiceFactory();
			var service = await factory.Create();
			var newPlaylist = new Playlist();
			newPlaylist.Snippet = new PlaylistSnippet();
			newPlaylist.Snippet.Title = AddPlaylistTitle.Value;
			newPlaylist.Snippet.Description = AddPlaylistDescription.Value;
			newPlaylist.Status = new PlaylistStatus();
			newPlaylist.Status.PrivacyStatus = "private";
			newPlaylist = await service.Playlists.Insert(newPlaylist, "snippet,status").ExecuteAsync();

			// 追加後にフォームの値をクリアする
			AddPlaylistTitle.Value = string.Empty;
			AddPlaylistDescription.Value = string.Empty;

			// プレイリスト一覧を更新
			await GetPlaylist();
		}

		/// <summary>
		/// プレイリストを削除する
		/// </summary>
		/// <param name="id">プレイリストのID</param>
		/// <returns></returns>
		public async Task DeletePlaylist(string id)
		{
			// 指定のIDと一致するプレイリストを削除する
			var factory = new YoutubeServiceFactory();
			var service = await factory.Create();
			await service.Playlists.Delete(id).ExecuteAsync();

			// プレイリスト一覧を更新
			await GetPlaylist();
		}

		/// <summary>
		/// ビデオをプレイリストに追加する
		/// </summary>
		/// <returns></returns>
		public async Task AddVideoToPlaylist()
		{
			// 入力の検証
			var playlistVm = SelectedPlaylistViewModel.Value;
			var videoId = AddVideoId.Value;
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
			SelectedPlaylistViewModel.Value.PlaylistItemViewModels.Add(new PlaylistItemViewModel(playlistItem, playlistVm));
		}

		#endregion
	}
}