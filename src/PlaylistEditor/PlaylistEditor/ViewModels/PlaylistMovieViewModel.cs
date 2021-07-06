using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistEditor.ViewModels
{
	/// <summary>
	/// プレイリストに所属する要素のVM
	/// </summary>
	public class PlaylistItemViewModel
	{
		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="playlistItem">プレイリストに所属する要素</param>
		public PlaylistItemViewModel(PlaylistItem playlistItem)
		{
			PlaylistItem = playlistItem;
		}

		#endregion

		#region プロパティ

		/// <summary>
		/// プレイリストに所属する要素
		/// </summary>
		public PlaylistItem PlaylistItem { get; }

		/// <summary>
		/// タイトル
		/// </summary>
		public string Title => PlaylistItem.Snippet.Title;

		#endregion

		#region オーバーライド

		public override string ToString()
		{
			return $"Title: {Title}";
		}

		#endregion
	}
}
