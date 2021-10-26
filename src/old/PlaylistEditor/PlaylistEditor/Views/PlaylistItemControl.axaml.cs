using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using PlaylistEditor.ViewModels;

namespace PlaylistEditor.Views
{
	public partial class PlaylistItemControl : UserControl
	{
		#region �t�B�[���h

		/// <summary>
		/// �h���b�O�h���b�v�\�ȃ{�[�_�[
		/// </summary>
		private Border m_DragDropBorder;

		#endregion
		public PlaylistItemControl()
		{
			InitializeComponent();

            SetupDnd();

            // TODO �n���h���o�^�̂����������炸�A�l�b�g�Œ����������e�����̂܂ܗ��p���Ă���
            m_DragDropBorder = this.Find<Border>("DragDropBorder");
			m_DragDropBorder.AddHandler(PointerPressedEvent, DoDrag);
		}

		#region ��������

        /// <summary>
        /// �h���b�v���̃Z�b�g�A�b�v������
        /// </summary>
		void SetupDnd()
        {
            void DragOver(object sender, DragEventArgs e)
            {
                // Only allow Copy or Link as Drop Operations.
                e.DragEffects = e.DragEffects & (DragDropEffects.Copy | DragDropEffects.Link);

                // Only allow if the dragged data contains text or filenames.
                if (!e.Data.Contains("PlaylistItem") || !e.Data.Contains("Movie"))
                    e.DragEffects = DragDropEffects.None;
            }

            void Drop(object sender, DragEventArgs e)
            {
                if (e.Data.Contains("Movie"))
				{
					_ = ((PlaylistItemViewModel)DataContext).PlaylistViewModel.AddVideoToPlaylist((string)e.Data.Get("Movie"));
				}
				else if(e.Data.Contains("PlaylistItem"))
				{
					var vm = (PlaylistItemViewModel?)e.Data.Get("PlaylistItem");
					if (vm is null) return;

					((PlaylistItemViewModel)DataContext).PlaylistViewModel.AddPlaylistItem(vm);
					vm.PlaylistViewModel.RemovePlaylistItem(vm.Id);
				}

				// �����ŏ�����������A�o�u�����O���~�߂�
				e.Handled = true;
            }

            AddHandler(DragDrop.DropEvent, Drop);
            AddHandler(DragDrop.DragOverEvent, DragOver);
        }

        /// <summary>
        /// �h���b�O����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void DoDrag(object? sender, PointerPressedEventArgs e)
		{
            if (m_DragDropBorder.DataContext is not PlaylistItemViewModel vm) return;
            if (vm == null) return;

            var dragData = new DataObject();
			dragData.Set("PlaylistItem", vm);
            DragDrop.DoDragDrop(e, dragData, DragDropEffects.Copy);
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		#endregion
	}
}
