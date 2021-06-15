using System.Collections.ObjectModel;

namespace PlaylistEditor.ViewModels
{
	public class SearchResultViewModel : ViewModelBase
	{
		#region プロパティ

		/// <summary>
		/// 検索結果一覧
		/// </summary>
		public ObservableCollection<string> SearchResultList { get; set; } = new ObservableCollection<string>();

		#endregion
	}
}