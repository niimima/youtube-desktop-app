using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using PlaylistEditor.ViewModels;
using System;

namespace PlaylistEditor.Views
{
	/// <summary>
	/// プレイリストコントロール
	/// </summary>
	public partial class PlaylistControl : UserControl
	{
		#region フィールド

        /// <summary>
        /// カスタムフォーマット
        /// </summary>
        private const string CustomFormat = "application/xxx-avalonia-controlcatalog-custom";

		#endregion

		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PlaylistControl()
		{
			InitializeComponent();

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
		/// コンポーネントの初期化
		/// </summary>
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		#endregion
	}
}
