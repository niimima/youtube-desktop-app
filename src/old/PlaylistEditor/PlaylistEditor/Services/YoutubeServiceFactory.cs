using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;

namespace PlaylistEditor.Services
{
	/// <summary>
	/// YouTubseServiceのファクトリ
	/// </summary>
	/// <remarks>
	/// 以下のコードを参照して認証情報を取得している
	/// https://github.com/youtube/api-samples/tree/master/dotnet
	/// </remarks>
	public class YoutubeServiceFactory
	{
		/// <summary>
		/// YouTubeServiceを利用できる状態で生成する
		/// </summary>
		public async Task<YouTubeService> Create()
		{
			UserCredential credential;
			using (var stream = new FileStream("local.settings.json", FileMode.Open, FileAccess.Read))
			{
				credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
					GoogleClientSecrets.Load(stream).Secrets,
					// This OAuth 2.0 access scope allows an application to upload files to the
					// authenticated user's YouTube channel, but doesn't allow other types of access.
					new[] { YouTubeService.Scope.Youtube },
					"user",
					CancellationToken.None,
					new FileDataStore(this.GetType().ToString())
				);
			}

			var youtubeService = new YouTubeService(new BaseClientService.Initializer()
			{
				HttpClientInitializer = credential,
				ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
			});

			return youtubeService;
		}
	}
}