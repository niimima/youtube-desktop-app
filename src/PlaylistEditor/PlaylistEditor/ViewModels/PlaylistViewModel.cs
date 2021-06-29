using Google.Apis.YouTube.v3.Data;
using PlaylistEditor.Services;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistEditor.ViewModels
{
	/// <summary>
	/// プレイリストのVM
	/// </summary>
	public class PlaylistViewModel
	{
		#region フィールド

		/// <summary>
		/// Disposeのタイミングに合わせてDisposeするリソースを登録する
		/// </summary>
		private readonly CompositeDisposable m_Disposables = new CompositeDisposable();

		#endregion

		#region プロパティ

		/// <summary>
		/// プレイリスト
		/// </summary>
		private Playlist Playlist;

		/// <summary>
		/// オーナーであるプレイリストエディタのVM
		/// </summary>
		private PlaylistEditorViewModel PlaylistEditorViewModel;

		/// <summary>
		/// プレイリストのタイトル
		/// </summary>
		public string Title => Playlist.Snippet.Title;

		/// <summary>
		/// プレイリストのID
		/// </summary>
		public string Id => Playlist.Id;

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

		#region 公開サービス

		/// <summary>
		/// プレイリストを削除する
		/// </summary>
		/// <returns></returns>
		public async Task DeletePlaylist()
		{
			await PlaylistEditorViewModel.DeletePlaylist(Id);
		}

		#endregion
	}
}
