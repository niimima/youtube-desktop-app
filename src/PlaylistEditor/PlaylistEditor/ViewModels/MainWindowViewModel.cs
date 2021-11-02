using PlaylistEditor.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlaylistEditor.ViewModels
{
	/// <summary>
	/// ���C���E�C���h�E��VM
	/// </summary>
	internal class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
	{
		#region �t�B�[���h

		/// <summary>
		/// YouTube�T�[�r�X
		/// </summary>
		private IYouTubeService m_YouTubeService;

		#endregion

		#region �\�z

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="youtubeService">YouTube�T�[�r�X</param>
		internal MainWindowViewModel(IYouTubeService youtubeService)
		{
			m_YouTubeService = youtubeService;
		}

		#endregion
	}
}
