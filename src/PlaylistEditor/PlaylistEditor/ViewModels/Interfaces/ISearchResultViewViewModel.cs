using PlaylistEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistEditor.ViewModels
{
	/// <summary>
	/// 検索結果ビューのVM
	/// </summary>
	interface ISearchResultViewViewModel
	{
		#region プロパティ

		/// <summary>
		/// 検索結果一覧で選択されたアイテム一覧
		/// </summary>
		IEnumerable<Video> CheckedItems { get; }

		#endregion

		#region 公開サービス

		/// <summary>
		/// 検索結果を更新する
		/// </summary>
		/// <param name="videos">動画一覧</param>
		void Update(IEnumerable<Video> videos);

		#endregion
	}
}
