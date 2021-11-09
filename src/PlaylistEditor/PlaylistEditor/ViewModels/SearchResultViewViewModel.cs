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
	class SearchResultViewViewModel : ISearchResultViewViewModel
	{
		#region 公開サービス

		/// <inheritdoc/>
		public void Update(IEnumerable<Video> videos)
		{
		}

		#endregion
	}
}
