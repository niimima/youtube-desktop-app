using PlaylistEditor.ViewModels.Dialogs;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistEditor.ViewModels
{
	/// <summary>
	/// プレイリストコンテンツのVM
	/// </summary>
	interface IPlaylistContentViewViewModel
	{

		/// <summary>
		/// プレイリストアイテムを追加するダイアログを表示するインタラクション
		/// </summary>
		public Interaction<Unit, AddPlaylistItemDialogViewModel> ShowAddPlaylistItemDialog { get; }
	}
}
