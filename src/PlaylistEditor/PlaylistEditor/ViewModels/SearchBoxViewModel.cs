using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using PlaylistEditor.Models;
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
			var service = factory.Create();
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

		#endregion
	}
}