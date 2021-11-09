using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
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
		public async void Initialize()
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
		public async Task<IEnumerable<Video>> SearchVideo(string searchWord, int maxResultCount = 50)
		{
			// YouTubeサービスに問い合わせ
			var searchListRequest = m_YouTubeService!.Search.List("snippet");
			searchListRequest.Q = searchWord;
			searchListRequest.MaxResults = maxResultCount;
			var searchListResponse = await searchListRequest.ExecuteAsync();

			// 取得した結果から動画を取得
			var searchVideos = searchListResponse.Items.Where(item => item.Id.Kind == "youtube#video");
			var resultVideos = new List<Video>();
			foreach(var item in searchVideos)
			{
				resultVideos.Add(new Video(item.Id.VideoId, item.Snippet.Title, item.Snippet.Description, item.Snippet.Thumbnails.Default__.Url));
			}

			return resultVideos;
		}

		#endregion
	}
}
