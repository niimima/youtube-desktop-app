using PlaylistEditor.Models;
using PlaylistEditor.Services;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Disposables;

namespace PlaylistEditor.ViewModels
{
    /// <summary>
    /// チャンネルのVM
    /// </summary>
    public class ChannelViewModel : ViewModelBase
    {
		#region フィールド

		/// <summary>
		/// Disposeのタイミングに合わせてDisposeするリソースを登録する
		/// </summary>
		private readonly CompositeDisposable m_Disposables = new CompositeDisposable();

		/// <summary>
		/// チャンネル
		/// </summary>
		private Channel m_Channel;

		/// <summary>
		/// Webクライエントサービス
		/// </summary>
		private IWebClientService m_WebClientService;

		#endregion

		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="channel">チャンネル</param>
		/// <param name="webClientService">Webクライエントサービス</param>
		public ChannelViewModel(Channel channel, IWebClientService webClientService)
		{
			Image = new ReactivePropertySlim<Avalonia.Media.Imaging.Bitmap>().AddTo(m_Disposables);
			IsChecked = new ReactivePropertySlim<bool>().AddTo(m_Disposables);

			m_Channel = channel;
			m_WebClientService = webClientService;
			if (string.IsNullOrEmpty(channel.ThumbnailUrl) == false)
			{
				m_WebClientService.DownloadImage(channel.ThumbnailUrl, Image);
			}
		}

		#endregion

		#region プロパティ

		/// <summary>
		/// チャンネル
		/// </summary>
		public Channel Channel => m_Channel;

		/// <summary>
		/// ID
		/// </summary>
		public string Id => m_Channel.ChannelId;

		/// <summary>
		/// タイトル
		/// </summary>
		public string Title => m_Channel.Title;

		/// <summary>
		/// チャンネルの概要
		/// </summary>
		public string Description => m_Channel.Description;

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
