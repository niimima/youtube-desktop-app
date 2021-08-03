using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using PlaylistEditor.ViewModels;

namespace PlaylistEditor.Views
{
    public class SearchResult : UserControl
    {
		#region �t�B�[���h

        /// <summary>
        /// �������ʂ̃��X�g�{�b�N�X
        /// </summary>
		private ListBox m_SearchResultListBox;

		#endregion

		#region �\�z

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
		public SearchResult()
        {
            InitializeComponent();

            // TODO �n���h���o�^�̂����������炸�A�l�b�g�Œ����������e��
            m_SearchResultListBox = this.Find<ListBox>("SearchResultListBox");
			m_SearchResultListBox.AddHandler(PointerPressedEvent, DoDrag, handledEventsToo: true);
        }

		#endregion

		#region ��������

        /// <summary>
        /// �h���b�O����
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
        /// �R���|�[�l���g�̏�����
        /// </summary>
		private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

		#endregion
	}
}
