using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using PlaylistEditor.ViewModels;

namespace PlaylistEditor.Views
{
    public class SearchResult : UserControl
    {
		#region フィールド

        /// <summary>
        /// 検索結果のリストボックス
        /// </summary>
		private ListBox m_SearchResultListBox;

		#endregion

		#region 構築

        /// <summary>
        /// コンストラクタ
        /// </summary>
		public SearchResult()
        {
            InitializeComponent();

            // TODO ハンドラ登録のやり方が分からず、ネットで調査した内容を
            m_SearchResultListBox = this.Find<ListBox>("SearchResultListBox");
			m_SearchResultListBox.AddHandler(PointerPressedEvent, DoDrag, handledEventsToo: true);
        }

		#endregion

		#region 内部処理

        /// <summary>
        /// ドラッグする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void DoDrag(object? sender, PointerPressedEventArgs e)
		{
            if (m_SearchResultListBox.DataContext is not SearchResultViewModel vm) return;
            if (vm.SelectedItem == null) return;

            var dragData = new DataObject();
			dragData.Set(DataFormats.Text, vm.SelectedItem.Value.Id);
            DragDrop.DoDragDrop(e, dragData, DragDropEffects.Copy);
		}

        /// <summary>
        /// コンポーネントの初期化
        /// </summary>
		private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

		#endregion
	}
}
