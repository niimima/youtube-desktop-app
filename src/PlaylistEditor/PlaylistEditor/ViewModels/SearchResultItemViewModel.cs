using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistEditor.ViewModels
{
	/// <summary>
	/// 検索結果の要素VM
	/// </summary>
	public class SearchResultItemViewModel : ViewModelBase
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
		/// <param name="id">ID</param>
		/// <param name="title">タイトル</param>
		/// <param name="description">動画の概要</param>
		/// <param name="url">URL</param>
		public SearchResultItemViewModel(string id, string title, string description, string url)
		{
			Id = id;
			Title = title;
			Description = description;
			Image = new ReactivePropertySlim<Avalonia.Media.Imaging.Bitmap>().AddTo(m_Disposables);
			DownloadImage(url);
		}

		#endregion

		#region プロパティ

		/// <summary>
		/// ID
		/// </summary>
		public string Id { get; }

		/// <summary>
		/// タイトル
		/// </summary>
		public string Title { get; }

		/// <summary>
		/// 動画の概要
		/// </summary>
		public string Description { get; }

		/// <summary>
		/// サムネイル画像
		/// </summary>
		public ReactivePropertySlim<Avalonia.Media.Imaging.Bitmap> Image { get; }

		#endregion

		#region オーバーライド

		public override string ToString()
		{
			return $"{Title} ({Id})";
		}

		#endregion

		#region 内部処理
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
