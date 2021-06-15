using System.Threading.Tasks;
using PlaylistEditor.Services;

namespace PlaylistEditor.ViewModels
{
	public class SearchBoxViewModel : ViewModelBase
	{
		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="ownerVm">オーナーのVM</param>
		public SearchBoxViewModel(ViewModelBase ownerVm)
		{
			Owner = ownerVm;
		}

		#endregion

		#region プロパティ

		/// <summary>
		/// オーナーのVM
		/// </summary>
		public ViewModelBase Owner { get; }

		/// <summary>
		/// メインウインドウのVM
		/// </summary>
		private MainWindowViewModel MainWindowViewModel => (MainWindowViewModel) Owner;

		#endregion

		#region 公開サービス

		/// <summary>
		/// 動画を検索する
		/// </summary>
		/// <returns></returns>
		public async Task Search()
		{
			var factory = new YoutubeServiceFactory();
			var service = await factory.Create();
			var searchListRequest = service.Search.List("snippet");
			searchListRequest.Q = "Google"; // Replace with your search term.
			searchListRequest.MaxResults = 50;

			var searchListResponse = await searchListRequest.ExecuteAsync();
			foreach (var searchResult in searchListResponse.Items)
			{
				switch (searchResult.Id.Kind)
				{
					case "youtube#video":
						MainWindowViewModel.PlaylistEditorViewModel.PlayLists.Add($"{searchResult.Snippet.Title} ({searchResult.Id.VideoId})");
						break;

					case "youtube#channel":
						MainWindowViewModel.PlaylistEditorViewModel.PlayLists.Add($"{searchResult.Snippet.Title} ({searchResult.Id.ChannelId})");
						break;

					case "youtube#playlist":
						MainWindowViewModel.PlaylistEditorViewModel.PlayLists.Add($"{searchResult.Snippet.Title} ({searchResult.Id.PlaylistId})");
						break;
				}
			}
		}

		/// <summary>
		/// プレイリストを取得する
		/// </summary>
		/// <returns></returns>
		public async Task GetPlaylist()
		{
			var factory = new YoutubeServiceFactory();
			var service = await factory.Create();
			var newPlaylist = service.Playlists.List("snippet");
			// チャンネルIDを指定することでも取得可能
			// newPlaylist.ChannelId = "UCpkkP5J-16g3zgfuIihCTrA";
			newPlaylist.Mine = true;
			var list = await newPlaylist.ExecuteAsync();
			foreach (var playlist in list.Items)
			{
				MainWindowViewModel.PlaylistEditorViewModel.PlayLists.Add($"{playlist.Snippet.Title} ({playlist.Id})");
			}
		}

		#endregion
	}
}