using PlaylistEditor.Models;
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
	/// プレイリストコンテンツの子要素VM
	/// </summary>
	class PlaylistContentViewItemViewModel
	{
		#region フィールド

		/// <summary>
		/// Disposeのタイミングに合わせてDisposeするリソースを登録する
		/// </summary>
		private readonly CompositeDisposable m_Disposables = new CompositeDisposable();


		/// <summary>
		/// プレイリストアイテム
		/// </summary>
		private PlaylistItem m_PlaylistItem;

		/// <summary>
		/// Webクライエントサービス
		/// </summary>
		private IWebClientService m_WebClientService;

		#endregion

		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="item">プレイリストアイテム</param>
		/// <param name="webClientService">Webクライエントサービス</param>
		public PlaylistContentViewItemViewModel(PlaylistItem item, IWebClientService webClientService)
		{
			Image = new ReactivePropertySlim<Avalonia.Media.Imaging.Bitmap>().AddTo(m_Disposables);

			m_PlaylistItem = item;
			m_WebClientService = webClientService;
			m_WebClientService.DownloadImage(item.ThumbnailUrl, Image);
		}

		#endregion

		#region プロパティ

		/// <summary>
		/// ID
		/// </summary>
		public string Id => m_PlaylistItem.Id;

		/// <summary>
		/// タイトル
		/// </summary>
		public string Title => m_PlaylistItem.Title;

		/// <summary>
		/// 概要
		/// </summary>
		public string Description => m_PlaylistItem.Description;

		/// <summary>
		/// サムネイル画像
		/// </summary>
		public ReactivePropertySlim<Avalonia.Media.Imaging.Bitmap> Image { get; }

		#endregion
	}
}
