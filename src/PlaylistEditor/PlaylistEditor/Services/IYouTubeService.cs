using PlaylistEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistEditor.Services
{
	/// <summary>
	/// YouTubeサービスのI/F
	/// </summary>
	interface IYouTubeService
	{
		/// <summary>
		/// 初期化します
		/// </summary>
		Task Initialize();

		/// <summary>
		/// 動画を検索します
		/// </summary>
		/// <param name="searchWord">検索ワード</param>
		/// <param name="maxResultCount">検索件数</param>
		/// <returns>動画一覧</returns>
		Task<IEnumerable<Video>> SearchVideo(string searchWord, int maxResultCount = 50);

		/// <summary>
		/// チャンネルを検索します
		/// </summary>
		/// <param name="channelId">チャンネルID</param>
		/// <param name="maxResultCount">検索件数</param>
		/// <returns>プレイリスト一覧</returns>
		Task<IEnumerable<Video>> SearchVideoByChannelId(string channelId, int maxResultCount = 50);

		/// <summary>
		/// プレイリストを検索します
		/// </summary>
		/// <param name="searchWord">検索ワード</param>
		/// <param name="maxResultCount">検索件数</param>
		/// <returns>プレイリスト一覧</returns>
		Task<IEnumerable<Playlist>> SearchPlaylist(string searchWord, int maxResultCount = 50);

		/// <summary>
		/// プレイリストを検索します
		/// </summary>
		/// <param name="channelId">チャンネルID</param>
		/// <param name="maxResultCount">検索件数</param>
		/// <returns>プレイリスト一覧</returns>
		Task<IEnumerable<Playlist>> SearchPlaylistByChannelId(string channelId, int maxResultCount = 50);

		/// <summary>
		/// チャンネルを検索します
		/// </summary>
		/// <param name="searchWord">検索ワード</param>
		/// <param name="maxResultCount">検索件数</param>
		/// <returns>チャンネル一覧</returns>
		Task<IEnumerable<Channel>> SearchChannel(string searchWord, int maxResultCount = 50);

		/// <summary>
		/// 自身のプレイリストを取得します
		/// </summary>
		/// <returns>自身のプレイリスト一覧</returns>
		Task<IEnumerable<Playlist>> GetMyPlaylists();

		/// <summary>
		/// プレイリストの子要素一覧を取得します
		/// </summary>
		/// <param name="id">ID</param>
		/// <returns>プレイリストの子要素一覧</returns>
		Task<IEnumerable<PlaylistItem>> GetPlaylistItems(string id);

		/// <summary>
		/// 対象のプレイリストアイテムに動画を追加します
		/// </summary>
		/// <param name="videos">動画一覧</param>
		/// <param name="playlistItem">追加対象のプレイリストアイテム</param>
		/// <returns></returns>
		Task AddVideosToPlaylistItem(IEnumerable<Video> videos, Playlist playlistItem);

		/// <summary>
		/// プレイリストアイテムを削除します
		/// </summary>
		/// <param name="ids">削除したいプレイリストアイテムのID一覧</param>
		/// <returns></returns>
		Task RemovePlaylistItems(IEnumerable<string> ids);

		/// <summary>
		/// プレイリストアイテムを移動します
		/// </summary>
		/// <param name="ids">移動したいプレイリストアイテムのID一覧</param>
		/// <param name="playlistId">移動先のプレイリストID</param>
		/// <returns></returns>
		Task MovePlaylistItems(IEnumerable<string> ids, IEnumerable<Google.Apis.YouTube.v3.Data.ResourceId> resourceIds, string playlistId);

		/// <summary>
		/// プレイリストを追加します
		/// </summary>
		/// <param name="title">タイトル</param>
		/// <param name="description">概要</param>
		/// <returns></returns>
		Task AddPlaylist(string title, string description);

		/// <summary>
		/// プレイリストを削除します
		/// </summary>
		/// <param name="id">ID</param>
		/// <returns></returns>
		Task DeletePlaylist(string id);
	}
}
