using PlaylistEditor.Models;
using PlaylistEditor.Services;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Disposables;

namespace PlaylistEditor.ViewModels
{
	/// <summary>
	/// 動画のVM
	/// </summary>
	public class VideoViewModel : ViewModelBase
	{
		#region フィールド

		/// <summary>
		/// Disposeのタイミングに合わせてDisposeするリソースを登録する
		/// </summary>
		private readonly CompositeDisposable m_Disposables = new CompositeDisposable();

		/// <summary>
		/// 動画
		/// </summary>
		private Video m_Video;

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
		public VideoViewModel(Video video, IWebClientService webClientService)
		{
			Image = new ReactivePropertySlim<Avalonia.Media.Imaging.Bitmap>().AddTo(m_Disposables);
			IsChecked = new ReactivePropertySlim<bool>().AddTo(m_Disposables);

			m_Video = video;
			m_WebClientService = webClientService;
			if (string.IsNullOrEmpty(video.ThumbnailUrl) == false)
			{
				m_WebClientService.DownloadImage(video.ThumbnailUrl, Image);
			}
		}

		#endregion

		#region プロパティ

		/// <summary>
		/// 動画
		/// </summary>
		public Video Video => m_Video;

		/// <summary>
		/// ID
		/// </summary>
		public string Id => m_Video.VideoId;

		/// <summary>
		/// タイトル
		/// </summary>
		public string Title => m_Video.Title;

		/// <summary>
		/// 動画の概要
		/// </summary>
		public string Description => m_Video.Description;

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
