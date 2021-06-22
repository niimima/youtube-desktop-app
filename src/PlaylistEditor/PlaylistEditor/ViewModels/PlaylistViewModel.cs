using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistEditor.ViewModels
{
	/// <summary>
	/// プレイリストのVM
	/// </summary>
	public class PlaylistViewModel
	{
		#region プロパティ

		/// <summary>
		/// プレイリスト
		/// </summary>
		private Playlist Playlist;

		/// <summary>
		/// オーナーであるプレイリストエディタのVM
		/// </summary>
		private PlaylistEditorViewModel PlaylistEditorViewModel;

		#endregion

		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PlaylistViewModel(Playlist playlist, PlaylistEditorViewModel playlistEditorViewModel)
		{
			Playlist = playlist;
			PlaylistEditorViewModel = playlistEditorViewModel;
		}

		#endregion

		#region オーバーライド

		public override string ToString()
		{
			return $"{Playlist.Snippet.Title} ({Playlist.Id})";
		}

		#endregion
	}
}
