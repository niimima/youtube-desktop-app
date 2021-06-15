using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PlaylistEditor.Services;

namespace PlaylistEditor.ViewModels
{
	/// <summary>
	/// プレイリストエディタのVM
	/// </summary>
	public class PlaylistEditorViewModel : ViewModelBase
	{
		#region プロパティ

		/// <summary>
		/// プレイリスト一覧
		/// </summary>
		public ObservableCollection<string> PlayLists { get; set; } = new ObservableCollection<string>();

		#endregion

		#region 公開サービス

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
				PlayLists.Add($"{playlist.Snippet.Title} ({playlist.Id})");
			}
		}

		#endregion
	}
}