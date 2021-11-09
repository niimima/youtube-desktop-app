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
		/// <summary>
		/// 検索結果を更新する
		/// </summary>
		/// <param name="videos">動画一覧</param>
		void Update(IEnumerable<Video> videos);
	}
}
