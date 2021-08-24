using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;

namespace PlaylistEditor.ViewModels
{
	public class SearchResultViewModel : ViewModelBase
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
		public SearchResultViewModel()
		{
			SelectedItem = new ReactivePropertySlim<SearchResultItemViewModel>().AddTo(m_Disposables);

		}

		#endregion

		#region プロパティ

		/// <summary>
		/// 検索結果一覧
		/// </summary>
		public ObservableCollection<SearchResultItemViewModel> SearchResultList { get; set; } = new ObservableCollection<SearchResultItemViewModel>();

		/// <summary>
		/// 検索結果一覧で選択されたアイテム
		/// </summary>
		public ReactivePropertySlim<SearchResultItemViewModel> SelectedItem { get; set; }

		#endregion

	}
}