using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistEditor.ViewModels.Dialogs
{
	/// <summary>
	/// プレイリストを追加ダイアログのVM
	/// </summary>
	public class AddPlaylistDialogViewModel : ViewModelBase
	{
		#region プロパティ

		/// <summary>
		/// タイトル
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 概要
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// 結果
		/// </summary>
		public bool Result { get; set; }

		#endregion
	}
}
