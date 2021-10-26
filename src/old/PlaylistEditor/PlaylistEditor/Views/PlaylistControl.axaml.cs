using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using PlaylistEditor.ViewModels;
using System;

namespace PlaylistEditor.Views
{
	/// <summary>
	/// �v���C���X�g�R���g���[��
	/// </summary>
	public partial class PlaylistControl : UserControl
	{
		#region �t�B�[���h

		/// <summary>
		/// �v���C���X�g�R���g���[���̃��X�g�{�b�N�X
		/// </summary>
		private ListBox m_PlaylistControlListBox;

		#endregion

		#region �\�z

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public PlaylistControl()
		{
			InitializeComponent();

            SetupDnd();

            // TODO �n���h���o�^�̂����������炸�A�l�b�g�Œ����������e�����̂܂ܗ��p���Ă���
            m_PlaylistControlListBox = this.Find<ListBox>("PlaylistControlListBox");
			m_PlaylistControlListBox.AddHandler(PointerPressedEvent, DoDrag);
		}

		#endregion

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
					_ = ((PlaylistViewModel)DataContext).AddVideoToPlaylist((string)e.Data.Get("Movie"));
				}
				else if(e.Data.Contains("PlaylistItem"))
				{
					var vm = (PlaylistItemViewModel?)e.Data.Get("PlaylistItem");
					if (vm is null) return;

					((PlaylistViewModel)DataContext).AddPlaylistItem(vm);
					vm.PlaylistViewModel.RemovePlaylistItem(vm.Id);
				}
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
            if (m_PlaylistControlListBox.DataContext is not PlaylistViewModel vm) return;
            if (vm.SelectedItem == null) return;

            var dragData = new DataObject();
			dragData.Set("PlaylistItem", vm.SelectedItem.Value);
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
