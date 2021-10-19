using Google.Apis.YouTube.v3.Data;
using PlaylistEditor.Services;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Disposables;

namespace PlaylistEditor.ViewModels
{
	/// <summary>
	/// プレイリストに所属する要素のVM
	/// </summary>
	public class PlaylistItemViewModel
	{
		#region フィールド

		/// <summary>
		/// Disposeのタイミングに合わせてDisposeするリソースを登録する
		/// </summary>
		private readonly CompositeDisposable m_Disposables = new CompositeDisposable();

		#endregion

		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="playlistItem">プレイリストに所属する要素</param>
		public PlaylistItemViewModel(PlaylistItem playlistItem, PlaylistViewModel playlistViewModel)
		{
			PlaylistItem = playlistItem;
			PlaylistViewModel = playlistViewModel;
			Image = new ReactivePropertySlim<Avalonia.Media.Imaging.Bitmap>().AddTo(m_Disposables);
			var client = new WebClientService();
			client.DownloadImage(PlaylistItem.Snippet.Thumbnails.Default__.Url, Image);
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

		/// <summary>
		/// サムネイル画像
		/// </summary>
		public ReactivePropertySlim<Avalonia.Media.Imaging.Bitmap> Image { get; }

		/// <summary>
		/// ID
		/// </summary>
		public string Id => PlaylistItem.Id;

		/// <summary>
		/// 動画の説明
		/// </summary>
		public string Description => PlaylistItem.Snippet.Description;

		/// <summary>
		/// 所属するプレイリストのVM
		/// </summary>
		public PlaylistViewModel PlaylistViewModel { get; }

		#endregion

		#region 公開サービス

		/// <summary>
		/// プレイリストアイテムを削除する
		/// </summary>
		public void RemovePlaylistItem()
		{
			PlaylistViewModel.RemovePlaylistItem(Id);
		}

		#endregion

		#region オーバーライド

		public override string ToString()
		{
			return $"Title: {Title}";
		}

		#endregion
	}
}
