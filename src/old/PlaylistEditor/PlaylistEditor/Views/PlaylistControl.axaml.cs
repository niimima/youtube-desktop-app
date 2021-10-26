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
	/// プレイリストコントロール
	/// </summary>
	public partial class PlaylistControl : UserControl
	{
		#region フィールド

		/// <summary>
		/// プレイリストコントロールのリストボックス
		/// </summary>
		private ListBox m_PlaylistControlListBox;

		#endregion

		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PlaylistControl()
		{
			InitializeComponent();

            SetupDnd();

            // TODO ハンドラ登録のやり方が分からず、ネットで調査した内容をそのまま利用している
            m_PlaylistControlListBox = this.Find<ListBox>("PlaylistControlListBox");
			m_PlaylistControlListBox.AddHandler(PointerPressedEvent, DoDrag);
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
        /// ドラッグする
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
		/// コンポーネントの初期化
		/// </summary>
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		#endregion
	}
}
