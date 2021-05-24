using System.IO;
using System.Text.Json;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using PlaylistEditor.Models;

namespace PlaylistEditor.Services
{
	/// <summary>
	/// YouTubseServiceのファクトリ
	/// </summary>
	public class YoutubeServiceFactory
	{
		/// <summary>
		/// YouTubeServiceを利用できる状態で生成する
		/// </summary>
		public YouTubeService Create()
		{
			// local.settings.jsonを以下の形式で格納すること
			// xxxxxは自身のAPIキーに置き換える
			// {
			//   "ApiKey" : "xxxxx"
			// }
			var json = File.ReadAllText(@".\local.settings.json");
			var settings = JsonSerializer.Deserialize<Settings>(json);
			return new YouTubeService(new BaseClientService.Initializer()
			{
				ApiKey = settings!.ApiKey,
				ApplicationName = this.GetType().ToString()
			});
		}
	}
}