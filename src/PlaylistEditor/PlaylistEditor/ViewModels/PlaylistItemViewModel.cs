using Google.Apis.YouTube.v3.Data;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

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
		public PlaylistItemViewModel(PlaylistItem playlistItem)
		{
			PlaylistItem = playlistItem;
			Image = new ReactivePropertySlim<Avalonia.Media.Imaging.Bitmap>().AddTo(m_Disposables);
			DownloadImage(PlaylistItem.Snippet.Thumbnails.Default__.Url);
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

		#endregion

		#region オーバーライド

		public override string ToString()
		{
			return $"Title: {Title}";
		}

		/*
		Image DownloadImage(string fromUrl)
		{
			using (System.Net.WebClient webClient = new System.Net.WebClient())
			{
				Image image;
				using (Stream stream = webClient.OpenRead(fromUrl))
				{
					image = System.Drawing.Image.FromStream(stream);
				}
				return image;
			}
		}
		*/

		public void DownloadImage(string url)
		{
			using (System.Net.WebClient client = new System.Net.WebClient())
            {
                client.DownloadDataAsync(new Uri(url));
				client.DownloadDataCompleted += ClientDownloadDataCompleted;
            }
        }

		private void ClientDownloadDataCompleted(object sender, System.Net.DownloadDataCompletedEventArgs e)
		{
			try
			{
				byte[] bytes = e.Result;

				Stream stream = new MemoryStream(bytes);

				var image = new Avalonia.Media.Imaging.Bitmap(stream);
				Image.Value = image;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
				Image.Value = null; // Could not download...
			}
		}

		#endregion
	}
}
