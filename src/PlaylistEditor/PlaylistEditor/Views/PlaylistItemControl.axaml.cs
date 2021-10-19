using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using PlaylistEditor.ViewModels;

namespace PlaylistEditor.Views
{
	public partial class PlaylistItemControl : UserControl
	{
		#region フィールド

		/// <summary>
		/// ドラッグドロップ可能なボーダー
		/// </summary>
		private Border m_DragDropBorder;

		#endregion
		public PlaylistItemControl()
		{
			InitializeComponent();

            SetupDnd();

            // TODO ハンドラ登録のやり方が分からず、ネットで調査した内容をそのまま利用している
            m_DragDropBorder = this.Find<Border>("DragDropBorder");
			m_DragDropBorder.AddHandler(PointerPressedEvent, DoDrag);
		}

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

				// ここで処理をした後、バブリングを止める
				e.Handled = true;
            }

            AddHandler(DragDrop.DropEvent, Drop);
            AddHandler(DragDrop.DragOverEvent, DragOver);
        }

        /// <summary>
        /// ドラッグする
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
