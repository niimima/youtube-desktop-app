using PlaylistEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistEditor.ViewModels.Interfaces
{
	/// <summary>
	/// プレイリスト一覧の要素VMのI/F
	/// </summary>
	interface IPlaylistListViewItemViewModel
	{
		/// <summary>
		/// プレイリスト
		/// </summary>
		Playlist Playlist { get; }

		/// <summary>
		/// ID
		/// </summary>
		string Id { get; }

		/// <summary>
		/// タイトル
		/// </summary>
		string Title { get; }

		/// <summary>
		/// 概要
		/// </summary>
		string Description { get; }
	}
}
