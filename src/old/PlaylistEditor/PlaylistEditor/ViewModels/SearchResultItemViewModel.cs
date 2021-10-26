using PlaylistEditor.Services;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Disposables;

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
			var client = new WebClientService();
			client.DownloadImage(url, Image);
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
	}
}
