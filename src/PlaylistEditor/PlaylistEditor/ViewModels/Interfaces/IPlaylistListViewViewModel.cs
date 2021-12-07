using PlaylistEditor.Models;
using PlaylistEditor.ViewModels.Interfaces;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistEditor.ViewModels
{
	/// <summary>
	/// プレイリスト一覧のVM
	/// </summary>
	interface IPlaylistListViewViewModel
	{
		/// <summary>
		/// 選択変更イベント
		/// </summary>
		event Action<IPlaylistListViewItemViewModel>? SelectionChanged;

		/// <summary>
		/// プレイリスト一覧
		/// </summary>
		ReactiveCollection<IPlaylistListViewItemViewModel> PlaylistList { get; }
	}
}
