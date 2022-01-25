using PlaylistEditor.Models;
using PlaylistEditor.Services;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Disposables;

namespace PlaylistEditor.ViewModels
{
	/// <summary>
	/// プレイリストの検索結果の要素VM
	/// </summary>
	class SearchResultPlaylistItemViewModel : ViewModelBase
	{
		#region フィールド

		/// <summary>
		/// Disposeのタイミングに合わせてDisposeするリソースを登録する
		/// </summary>
		private readonly CompositeDisposable m_Disposables = new CompositeDisposable();

		/// <summary>
		/// プレイリスト
		/// </summary>
		private Playlist m_Playlist;

		/// <summary>
		/// Webクライエントサービス
		/// </summary>
		private IWebClientService m_WebClientService;

		#endregion

		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="video">動画</param>
		/// <param name="webClientService">Webクライエントサービス</param>
		public SearchResultPlaylistItemViewModel(Playlist playlist, IWebClientService webClientService)
		{
			Image = new ReactivePropertySlim<Avalonia.Media.Imaging.Bitmap>().AddTo(m_Disposables);
			IsChecked = new ReactivePropertySlim<bool>().AddTo(m_Disposables);

			m_Playlist = playlist;
			m_WebClientService = webClientService;
			if (string.IsNullOrEmpty(playlist.ThumbnailUrl) == false)
			{
				m_WebClientService.DownloadImage(playlist.ThumbnailUrl, Image);
			}
		}

		#endregion

		#region プロパティ

		/// <summary>
		/// 動画
		/// </summary>
		public Playlist Playlist => m_Playlist;

		/// <summary>
		/// ID
		/// </summary>
		public string Id => m_Playlist.PlaylistId;

		/// <summary>
		/// タイトル
		/// </summary>
		public string Title => m_Playlist.Title;

		/// <summary>
		/// 概要
		/// </summary>
		public string Description => m_Playlist.Description;

		/// <summary>
		/// サムネイル画像
		/// </summary>
		public ReactivePropertySlim<Avalonia.Media.Imaging.Bitmap> Image { get; }

		/// <summary>
		/// チェック状態か
		/// </summary>
		public ReactivePropertySlim<bool> IsChecked { get; }

		#endregion
	}
}
