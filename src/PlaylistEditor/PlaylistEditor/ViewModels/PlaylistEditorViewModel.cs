using System;
using Google.Apis.YouTube.v3;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Google.Apis.Services;
using PlaylistEditor.Models;
using ReactiveUI;
using System.Text.Json;

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

	}
}