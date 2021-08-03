using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistEditor.ViewModels
{
	/// <summary>
	/// 検索結果の要素VM
	/// </summary>
	public class SearchResultItemViewModel : ViewModelBase
	{
		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="id">ID</param>
		/// <param name="title">タイトル</param>
		public SearchResultItemViewModel(string id, string title)
		{
			Id = id;
			Title = title;
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

		#endregion

		#region オーバーライド

		public override string ToString()
		{
			return $"{Title} ({Id})";
		}

		#endregion
	}
}
