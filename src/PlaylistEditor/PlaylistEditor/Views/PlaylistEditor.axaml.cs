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
		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PlaylistEditor()
        {
            InitializeComponent();
        }

		#endregion

		#region 内部処理

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
