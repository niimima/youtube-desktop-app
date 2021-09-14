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
		public async Task Search(string param)
		{
			// 事前にクリアする
			MainWindowViewModel.SearchResultViewModel.SearchResultList.Clear();

			var factory = new YoutubeServiceFactory();
			var service = await factory.Create();
			var searchListRequest = service.Search.List("snippet");
			searchListRequest.Q = param;
			searchListRequest.MaxResults = 50;
			var searchListResponse = await searchListRequest.ExecuteAsync();
			foreach (var searchResult in searchListResponse.Items)
			{
				switch (searchResult.Id.Kind)
				{
					case "youtube#video":
						MainWindowViewModel.SearchResultViewModel.SearchResultList.Add(new SearchResultItemViewModel(searchResult.Id.VideoId, searchResult.Snippet.Title, searchResult.Snippet.Description, searchResult.Snippet.Thumbnails.Default__.Url));
						break;
				}
			}
		}

		#endregion
	}
}