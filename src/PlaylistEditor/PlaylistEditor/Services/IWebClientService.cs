using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistEditor.Services
{
	/// <summary>
	/// WebクライエントサービスのI/F
	/// </summary>
	public interface IWebClientService
	{
		/// <summary>
		/// 指定したURLから画像を取得する
		/// </summary>
		/// <param name="url">URL</param>
		/// <param name="bitmapProperty">画像を設定したいプロパティ</param>
		void DownloadImage(string url, ReactivePropertySlim<Avalonia.Media.Imaging.Bitmap> bitmapProperty);
	}
}
