using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
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
        /// �J�X�^���t�H�[�}�b�g
        /// </summary>
        private const string CustomFormat = "application/xxx-avalonia-controlcatalog-custom";

		#endregion

		#region �\�z

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public PlaylistControl()
		{
			InitializeComponent();

            SetupDnd();
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
                if (!e.Data.Contains(DataFormats.Text))
                    e.DragEffects = DragDropEffects.None;
            }

            void Drop(object sender, DragEventArgs e)
            {
                if (e.Data.Contains(DataFormats.Text))
				{
					_ = ((PlaylistViewModel)DataContext).AddVideoToPlaylist(e.Data.GetText());
				}
            }

            AddHandler(DragDrop.DropEvent, Drop);
            AddHandler(DragDrop.DragOverEvent, DragOver);
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
