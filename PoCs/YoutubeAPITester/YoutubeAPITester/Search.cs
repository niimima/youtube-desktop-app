using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace YoutubeAPITester
{
	/// <summary>
	/// YouTube Data API v3 sample: search by keyword.
	/// Relies on the Google APIs Client Library for .NET, v1.7.0 or higher.
	/// See https://code.google.com/p/google-api-dotnet-client/wiki/GettingStarted
	///
	/// Set ApiKey to the API key value from the APIs & auth > Registered apps tab of
	///   https://cloud.google.com/console
	/// Please ensure that you have enabled the YouTube Data API for your project.
	/// </summary>
	/// <remarks>
	/// 以下のプログラムを利用。
	/// https://github.com/youtube/api-samples/tree/master/dotnet/Google.Apis.YouTube.Samples.Search
	///
	/// APIの取得は以下の記事を参照。
	/// https://qiita.com/commy/items/5edda78c917bf70adb63
	/// </remarks>
    internal class Search 
    {
        [STAThread]
        static void Main(string[] args)
        {
	        Console.WriteLine("YouTube Data API: Search");
	        Console.WriteLine("========================");

	        try
	        {
		        new Search().Run().Wait();
	        }
	        catch (AggregateException ex)
	        {
		        foreach (var e in ex.InnerExceptions)
		        {
			        Console.WriteLine("Error: " + e.Message);
		        }
	        }

	        Console.WriteLine("Press any key to continue...");
	        Console.ReadKey();
        }

        private async Task Run()
        {
			// local.settings.jsonを以下の形式で格納すること
			// xxxxxは自身のAPIキーに置き換える
			// {
			//   "ApiKey" : "xxxxx"
			// }
			var json = File.ReadAllText(@".\local.settings.json");
			var settings = JsonSerializer.Deserialize<Settings>(json);
	        var youtubeService = new YouTubeService(new BaseClientService.Initializer()
	        {
		        ApiKey = settings.ApiKey,
		        ApplicationName = this.GetType().ToString()
	        });

	        var searchListRequest = youtubeService.Search.List("snippet");
	        searchListRequest.Q = "Google"; // Replace with your search term.
	        searchListRequest.MaxResults = 50;

	        // Call the search.list method to retrieve results matching the specified query term.
	        var searchListResponse = await searchListRequest.ExecuteAsync();

	        List<string> videos = new List<string>();
	        List<string> channels = new List<string>();
	        List<string> playlists = new List<string>();

	        // Add each result to the appropriate list, and then display the lists of
	        // matching videos, channels, and playlists.
	        foreach (var searchResult in searchListResponse.Items)
	        {
		        switch (searchResult.Id.Kind)
		        {
			        case "youtube#video":
				        videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
				        break;

			        case "youtube#channel":
				        channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
				        break;

			        case "youtube#playlist":
				        playlists.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
				        break;
		        }
	        }

	        Console.WriteLine(String.Format("Videos:\n{0}\n", string.Join("\n", videos)));
	        Console.WriteLine(String.Format("Channels:\n{0}\n", string.Join("\n", channels)));
	        Console.WriteLine(String.Format("Playlists:\n{0}\n", string.Join("\n", playlists)));
        }
    }
}
