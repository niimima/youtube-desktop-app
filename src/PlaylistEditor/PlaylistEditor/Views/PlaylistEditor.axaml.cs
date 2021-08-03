using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using System;

namespace PlaylistEditor.Views
{
    /// <summary>
    /// プレイリストエディタ
    /// </summary>
    public class PlaylistEditor : UserControl
    {
		#region フィールド

        /// <summary>
        /// ドロップステート
        /// </summary>
        TextBlock m_DropState;

        /// <summary>
        /// カスタムフォーマット
        /// </summary>
        private const string CustomFormat = "application/xxx-avalonia-controlcatalog-custom";

		#endregion

		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PlaylistEditor()
        {
            InitializeComponent();

            m_DropState = this.Find<TextBlock>("DropState");

            SetupDnd();
        }

		#endregion

		#region 内部処理

        /// <summary>
        /// ドロップ時のセットアップをする
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
        /// コンポーネントの初期化
        /// </summary>
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

		#endregion
	}
}
