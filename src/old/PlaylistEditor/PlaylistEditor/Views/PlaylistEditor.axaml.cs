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
		#region �\�z

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public PlaylistEditor()
        {
            InitializeComponent();
        }

		#endregion

		#region ��������

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
