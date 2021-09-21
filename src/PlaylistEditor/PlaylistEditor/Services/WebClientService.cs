using Reactive.Bindings;
using System;
using System.IO;
using System.Net;

namespace PlaylistEditor.Services
{
	/// <summary>
	/// Webクライエントサービス
	/// </summary>
	class WebClientService
	{
		#region 公開サービス

		/// <summary>
		/// 指定したURLから画像を取得する
		/// </summary>
		/// <param name="url">URL</param>
		/// <param name="bitmapProperty">画像をセットしたいプロパティ</param>
		internal void DownloadImage(string url, ReactivePropertySlim<Avalonia.Media.Imaging.Bitmap> bitmapProperty)
		{
			using WebClient client = new();
			client.DownloadDataAsync(new Uri(url), bitmapProperty);
			client.DownloadDataCompleted += ClientDownloadDataCompleted;
		}


		#endregion

		#region

		/// <summary>
		/// データのダウンロード完了後イベントハンドラ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ClientDownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			try
			{
				byte[] bytes = e.Result;

				Stream stream = new MemoryStream(bytes);

				var image = new Avalonia.Media.Imaging.Bitmap(stream);
				var bitmapProperty = (ReactivePropertySlim<Avalonia.Media.Imaging.Bitmap>?)e.UserState;
				bitmapProperty!.Value = image;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
			}
		}

		#endregion
	}
}
