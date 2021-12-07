using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using PlaylistEditor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlaylistEditor.Services
{
	/// <summary>
	/// YouTubseServiceをラップし、よく使用する機能をメソッド化して提供します。
	/// </summary>
	/// <remarks>
	/// 以下のコードを参照して認証情報を取得している
	/// https://github.com/youtube/api-samples/tree/master/dotnet
	/// </remarks>
	public class YouTubeServiceWrapper : IYouTubeService
	{
		#region フィールド

		/// <summary>
		/// YouTubeへのアクセスに利用するサービス
		/// </summary>
		private YouTubeService? m_YouTubeService;

		#endregion

		#region 公開サービス

		/// <inheritdoc/>
		public async Task Initialize()
		{
			UserCredential credential;
			using (var stream = new FileStream("local.settings.json", FileMode.Open, FileAccess.Read))
			{
				credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
					GoogleClientSecrets.FromStream(stream).Secrets,
					// This OAuth 2.0 access scope allows an application to upload files to the
					// authenticated user's YouTube channel, but doesn't allow other types of access.
					new[] { YouTubeService.Scope.Youtube },
					"user",
					CancellationToken.None,
					new FileDataStore(this.GetType().ToString())
				);
			}

			m_YouTubeService = new YouTubeService(new BaseClientService.Initializer()
			{
				HttpClientInitializer = credential,
				ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
			});
		}

		/// <inheritdoc/>
		public async Task<IEnumerable<Models.Video>> SearchVideo(string searchWord, int maxResultCount = 50)
		{
			// YouTubeサービスに問い合わせ
			var searchListRequest = m_YouTubeService!.Search.List("snippet");
			searchListRequest.Q = searchWord;
			searchListRequest.MaxResults = maxResultCount;
			var searchListResponse = await searchListRequest.ExecuteAsync();

			// 取得した結果から動画を取得
			var searchVideos = searchListResponse.Items.Where(item => item.Id.Kind == "youtube#video");
			var resultVideos = new List<Models.Video>();
			foreach(var item in searchVideos)
			{
				resultVideos.Add(new Models.Video(item.Id.VideoId, item.Snippet.Title, item.Snippet.Description, item.Snippet.Thumbnails.Default__.Url));
			}

			return resultVideos;
		}

		/// <inheritdoc/>
		public async Task<IEnumerable<Models.Playlist>> GetMyPlaylists()
		{
			var newPlaylist = m_YouTubeService!.Playlists.List("snippet");
			// チャンネルIDを指定することでも取得可能
			// newPlaylist.ChannelId = "UCpkkP5J-16g3zgfuIihCTrA";
			newPlaylist.Mine = true;
			var list = await newPlaylist.ExecuteAsync();
			var resultPlaylists = new List<Models.Playlist>();
			foreach (var playlist in list.Items)
			{
				resultPlaylists.Add(new Models.Playlist(playlist.Id, playlist.Snippet.Title, playlist.Snippet.Description, playlist.Snippet.Thumbnails.Default__.Url));
			}
			return resultPlaylists;
		}

		/// <inheritdoc/>
		public async Task<IEnumerable<Models.PlaylistItem>> GetPlaylistItems(string id)
		{
			var playlistItems = m_YouTubeService!.PlaylistItems.List("snippet");
			// 100件まで編集可能とする
			playlistItems.MaxResults = 100;
			playlistItems.PlaylistId = id;

			var items = await playlistItems.ExecuteAsync();
			var resultPlaylistItems = new List<Models.PlaylistItem>();
			foreach (var playlistItem in items.Items)
			{
				if (playlistItem.Snippet.Thumbnails.Default__ == null)
				{
					resultPlaylistItems.Add(new Models.PlaylistItem(playlistItem.Id, playlistItem.Snippet.ResourceId, playlistItem.Snippet.Title, playlistItem.Snippet.Description, string.Empty));
				}
				else
				{
					resultPlaylistItems.Add(new Models.PlaylistItem(playlistItem.Id, playlistItem.Snippet.ResourceId, playlistItem.Snippet.Title, playlistItem.Snippet.Description, playlistItem.Snippet.Thumbnails.Default__.Url));
				}
			}

			return resultPlaylistItems;
		}

		/// <inheritdoc/>
		public async Task AddVideosToPlaylistItem(IEnumerable<Models.Video> videos, Models.Playlist playlist)
		{
			// 入力の検証
			foreach (var video in videos)
			{
				// 以下を参考にプレイリストに指定の動画を追加
				// https://github.com/youtube/api-samples/blob/master/dotnet/Google.Apis.YouTube.Samples.Playlists/PlaylistUpdates.cs#L94
				var actualPlaylistItem = new Google.Apis.YouTube.v3.Data.PlaylistItem();
				actualPlaylistItem.Snippet = new PlaylistItemSnippet();
				actualPlaylistItem.Snippet.PlaylistId = playlist.PlaylistId;
				actualPlaylistItem.Snippet.ResourceId = new ResourceId();
				actualPlaylistItem.Snippet.ResourceId.Kind = "youtube#video";
				actualPlaylistItem.Snippet.ResourceId.VideoId = video.VideoId;
				_ = await m_YouTubeService!.PlaylistItems.Insert(actualPlaylistItem, "snippet").ExecuteAsync();
			}
		}

		/// <inheritdoc/>
		public async Task RemovePlaylistItems(IEnumerable<string> ids)
		{
			foreach (var id in ids)
			{
				await m_YouTubeService!.PlaylistItems.Delete(id).ExecuteAsync();
			}
		}


		/// <inheritdoc/>
		public async Task MovePlaylistItems(IEnumerable<string> ids, IEnumerable<ResourceId> resourcesIds, string playlistId)
		{
			foreach (var id in resourcesIds)
			{
				AddPlaylistItems(id, playlistId);
			}
			foreach (var id in ids)
			{
				await m_YouTubeService!.PlaylistItems.Delete(id).ExecuteAsync();
			}
		}

		#endregion

		private async void AddPlaylistItems(ResourceId resourceId, string playlistId)
		{
			// 以下を参考にプレイリストに指定の動画を追加
			// https://github.com/youtube/api-samples/blob/master/dotnet/Google.Apis.YouTube.Samples.Playlists/PlaylistUpdates.cs#L94
			var playlistItem = new Google.Apis.YouTube.v3.Data.PlaylistItem();
			playlistItem.Snippet = new PlaylistItemSnippet();
			playlistItem.Snippet.PlaylistId = playlistId;
			playlistItem.Snippet.ResourceId = resourceId;
			await m_YouTubeService!.PlaylistItems.Insert(playlistItem, "snippet").ExecuteAsync();
		}
	}
}
