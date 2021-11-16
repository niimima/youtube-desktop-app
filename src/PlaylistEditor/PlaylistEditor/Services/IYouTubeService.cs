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
		void Initialize();

		/// <summary>
		/// 動画を検索します
		/// </summary>
		/// <param name="searchWord">検索ワード</param>
		/// <param name="maxResultCount">検索件数</param>
		/// <returns>動画一覧</returns>
		Task<IEnumerable<Video>> SearchVideo(string searchWord, int maxResultCount = 50);

		/// <summary>
		/// 自身のプレイリストを取得します
		/// </summary>
		/// <returns>自身のプレイリスト一覧</returns>
		Task<IEnumerable<Playlist>> GetMyPlaylists();
	}
}
