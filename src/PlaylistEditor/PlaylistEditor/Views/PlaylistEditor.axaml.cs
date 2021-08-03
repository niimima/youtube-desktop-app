using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using System;

namespace PlaylistEditor.Views
{
    /// <summary>
    /// �v���C���X�g�G�f�B�^
    /// </summary>
    public class PlaylistEditor : UserControl
    {
		#region �t�B�[���h

        /// <summary>
        /// �h���b�v�X�e�[�g
        /// </summary>
        TextBlock m_DropState;

        /// <summary>
        /// �J�X�^���t�H�[�}�b�g
        /// </summary>
        private const string CustomFormat = "application/xxx-avalonia-controlcatalog-custom";

		#endregion

		#region �\�z

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public PlaylistEditor()
        {
            InitializeComponent();

            m_DropState = this.Find<TextBlock>("DropState");

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
                if (!e.Data.Contains(DataFormats.Text)
                    && !e.Data.Contains(DataFormats.FileNames)
                    && !e.Data.Contains(CustomFormat))
                    e.DragEffects = DragDropEffects.None;
            }

            void Drop(object sender, DragEventArgs e)
            {
                if (e.Data.Contains(DataFormats.Text))
                    m_DropState.Text = e.Data.GetText();
                else if (e.Data.Contains(DataFormats.FileNames))
                    m_DropState.Text = string.Join(Environment.NewLine, e.Data.GetFileNames());
                else if (e.Data.Contains(CustomFormat))
                    m_DropState.Text = "Custom: " + e.Data.Get(CustomFormat);
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
