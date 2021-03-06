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

		/// <summary>
		/// 公開されているプレイリストアイテムを自分のプレイリストアイテムとして追加するダイアログを表示するインタラクション
		/// </summary>
		public Interaction<Unit, ClonePlaylistItemsDialogViewModel> ShowClonePlaylistItemsDialog { get; }

		/// <summary>
		/// 検索したチャンネルから動画・プレイリストアイテムを自分のプレイリストアイテムに追加するインタラクション
		/// </summary>
		public Interaction<Unit, AddOrClonePlaylistItemsDialogViewModel> ShowAddOrClonePlaylistItemsDialog { get; }
	}
}
