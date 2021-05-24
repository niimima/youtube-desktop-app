using System;
using System.Collections.Generic;
using System.Text;

namespace PlaylistEditor.ViewModels
{
    /// <summary>
    /// メインウインドウのVM
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
	    #region 構築

        /// <summary>
        /// コンストラクタ
        /// </summary>
	    public MainWindowViewModel()
	    {
		    PlaylistEditorViewModel = new PlaylistEditorViewModel();
		    SearchBoxViewModel = new SearchBoxViewModel(this);
		    SearchResultViewModel = new SearchResultViewModel();
	    }

	    #endregion

        #region プロパティ

        /// <summary>
        /// プレイリストエディタのVM
        /// </summary>
        public PlaylistEditorViewModel PlaylistEditorViewModel { get; }

        /// <summary>
        /// 検索ボックスのVM
        /// </summary>
        public SearchBoxViewModel SearchBoxViewModel { get; }

        /// <summary>
        /// 検索結果のVM
        /// </summary>
        public SearchResultViewModel SearchResultViewModel { get; }

        #endregion


    }
}
