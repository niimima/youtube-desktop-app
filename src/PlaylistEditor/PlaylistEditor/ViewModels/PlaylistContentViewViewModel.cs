using PlaylistEditor.Services;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistEditor.ViewModels
{
	/// <summary>
	/// プレイリストコンテンツのVM
	/// </summary>
	internal class PlaylistContentViewViewModel : IPlaylistContentViewViewModel
	{
		#region フィールド

		/// <summary>
		/// YouTubeサービス
		/// </summary>
		private IYouTubeService m_YouTubeService;

		/// <summary>
		/// Webクライエントサービス
		/// </summary>
		private IWebClientService m_WebClientService;

		/// <summary>
		/// Disposeのタイミングに合わせてDisposeするリソースを登録する
		/// </summary>
		private readonly CompositeDisposable m_Disposables = new CompositeDisposable();

		#endregion

		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		internal PlaylistContentViewViewModel(
			IYouTubeService youTubeService,
			IWebClientService webClientService,
			IPlaylistListViewViewModel playlistListViewViewModel)
		{
			m_YouTubeService = youTubeService;
			m_WebClientService = webClientService;
			playlistListViewViewModel.SelectionChanged += PlaylistListViewViewModel_SelectionChanged;

			Title = new ReactivePropertySlim<string>().AddTo(m_Disposables);
			Description = new ReactivePropertySlim<string>().AddTo(m_Disposables);
			PlaylistItemList = new ReactiveCollection<PlaylistContentViewItemViewModel>().AddTo(m_Disposables);
		}

		#endregion

		#region プロパティ

		/// <summary>
		/// タイトル
		/// </summary>
		public ReactivePropertySlim<string> Title { get; set; }

		/// <summary>
		/// 概要
		/// </summary>
		public ReactivePropertySlim<string> Description { get; set; }

		/// <summary>
		/// プレイリストアイテムの一覧
		/// </summary>
		public ReactiveCollection<PlaylistContentViewItemViewModel> PlaylistItemList { get; }


		#endregion

		#region イベントハンドラ

		/// <summary>
		/// プレイリスト一覧の選択変更後イベントハンドラ
		/// </summary>
		/// <param name="itemViewModel"></param>
		private async void PlaylistListViewViewModel_SelectionChanged(Interfaces.IPlaylistListViewItemViewModel itemViewModel)
		{
			Title.Value = itemViewModel.Title;
			Description.Value = itemViewModel.Description;

			PlaylistItemList.Clear();
			var playlistItems = await m_YouTubeService.GetPlaylistItems(itemViewModel.Id);
			foreach (var item in playlistItems)
			{
				PlaylistItemList.Add(new PlaylistContentViewItemViewModel(item, m_WebClientService));
			}
		}

		#endregion
	}
}
