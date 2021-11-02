using PlaylistEditor.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlaylistEditor.ViewModels
{
	/// <summary>
	/// メインウインドウのVM
	/// </summary>
	internal class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
	{
		#region フィールド

		/// <summary>
		/// YouTubeサービス
		/// </summary>
		private IYouTubeService m_YouTubeService;

		#endregion

		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="youtubeService">YouTubeサービス</param>
		internal MainWindowViewModel(IYouTubeService youtubeService)
		{
			m_YouTubeService = youtubeService;
		}

		#endregion
	}
}
