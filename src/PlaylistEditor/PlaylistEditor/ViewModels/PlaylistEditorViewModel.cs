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
		public ObservableCollection<PlaylistViewModel> Playlists { get; set; } = new ObservableCollection<PlaylistViewModel>();

		/// <summary>
		/// 追加するプレイリスト名
		/// </summary>
		public ReactivePropertySlim<string> AddPlaylistTitle { get; }

		/// <summary>
		/// 追加するプレイリスト名
		/// </summary>
		public ReactivePropertySlim<string> AddPlaylistDescription { get; }


		#endregion

		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PlaylistEditorViewModel()
		{
			AddPlaylistTitle = new ReactivePropertySlim<string>().AddTo(m_Disposables);
			AddPlaylistDescription = new ReactivePropertySlim<string>().AddTo(m_Disposables);
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
			Playlists.Clear();

			var factory = new YoutubeServiceFactory();
			var service = await factory.Create();
			var newPlaylist = service.Playlists.List("snippet");
			// チャンネルIDを指定することでも取得可能
			// newPlaylist.ChannelId = "UCpkkP5J-16g3zgfuIihCTrA";
			newPlaylist.Mine = true;
			var list = await newPlaylist.ExecuteAsync();
			foreach (var playlist in list.Items)
			{
				var playlistVm = new PlaylistViewModel(playlist, this);
				Playlists.Add(playlistVm);
			}
		}

		/// <summary>
		/// プレイリストを追加する
		/// </summary>
		/// <returns></returns>
		public async Task AddPlaylist()
		{
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
			// 入力結果からプレイリストを追加する
			var factory = new YoutubeServiceFactory();
			var service = await factory.Create();
			await service.Playlists.Delete(id).ExecuteAsync();

			// プレイリスト一覧を更新
			await GetPlaylist();
		}

		#endregion
	}
}