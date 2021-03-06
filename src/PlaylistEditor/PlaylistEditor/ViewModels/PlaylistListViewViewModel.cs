using PlaylistEditor.Services;
using PlaylistEditor.ViewModels.Dialogs;
using PlaylistEditor.ViewModels.Interfaces;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive.Linq;
using System.Reactive;

namespace PlaylistEditor.ViewModels
{
    /// <summary>
    /// プレイリスト一覧のVM
    /// </summary>
    class PlaylistListViewViewModel : ViewModelBase, IPlaylistListViewViewModel
	{
		#region フィールド

		/// <summary>
		/// Disposeのタイミングに合わせてDisposeするリソースを登録する
		/// </summary>
		private readonly CompositeDisposable m_Disposables = new CompositeDisposable();

		/// <summary>
		/// YouTubeサービス
		/// </summary>
		private IYouTubeService m_YouTubeService;

		/// <summary>
		/// Webクライエントサービス
		/// </summary>
		private IWebClientService m_WebClientService;

		#endregion

		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="youTubeService">YouTubeサービス</param>
		/// <param name="webClientService">Webクライエントサービス</param>
		public PlaylistListViewViewModel(IYouTubeService youTubeService, IWebClientService webClientService)
		{
			PlaylistList = new ReactiveCollection<IPlaylistListViewItemViewModel>().AddTo(m_Disposables);
			SelectedItem = new ReactivePropertySlim<PlaylistViewModel>().AddTo(m_Disposables);
			// 選択アイテムが変更されたら選択変更イベントも通知する
			SelectedItem.Subscribe(_ => RaiseSelectionChanged());

			m_YouTubeService = youTubeService;
			m_WebClientService = webClientService;

			// ダイアログを表示するインタラクションを保持
			ShowAddPlaylistDialog = new Interaction<Unit, AddPlaylistDialogViewModel>();
		}

		#endregion

		#region プロパティ

		/// <summary>
		/// プレイリスト一覧
		/// </summary>
		public ReactiveCollection<IPlaylistListViewItemViewModel> PlaylistList { get; }

		/// <summary>
		/// 選択されているプレイリスト
		/// </summary>
		public ReactivePropertySlim<PlaylistViewModel> SelectedItem { get; set; }

		/// <summary>
		/// プレイリストを追加するダイアログを表示するインタラクション
		/// </summary>
		public Interaction<Unit, AddPlaylistDialogViewModel> ShowAddPlaylistDialog { get; }

		#endregion

		#region イベント

		/// <summary>
		/// 選択変更イベント
		/// </summary>
		public event Action<IPlaylistListViewItemViewModel>? SelectionChanged;

		#endregion

		#region 公開サービス

		/// <summary>
		/// 初期化する
		/// </summary>
		internal async Task Initialize()
		{
			// プレイリスト一覧を空にする
			PlaylistList.Clear();

			var playlists = await m_YouTubeService.GetMyPlaylists();
			foreach (var playlist in playlists)
			{
				PlaylistList.Add(new PlaylistViewModel(playlist, m_WebClientService));
			}
		}

		/// <summary>
		/// プレイリストを追加する
		/// </summary>
		public async Task AddPlaylistAsync()
		{
			// ダイアログを表示
			var resultVm = await ShowAddPlaylistDialog.Handle(Unit.Default);
			if (resultVm.Result == false) return;

			// OKが押下された場合に追加
			await m_YouTubeService.AddPlaylist(resultVm.Title, resultVm.Description);

			// プレイリストを取得しなおす
			await Initialize();
		}

		/// <summary>
		/// プレイリストを削除する
		/// </summary>
		public async Task RemovePlaylist()
		{
			await m_YouTubeService.DeletePlaylist(SelectedItem.Value.Id);

			// プレイリストを取得しなおす
			await Initialize();
		}

		#endregion

		#region 内部処理

		/// <summary>
		/// 選択変更があったことを通知する
		/// </summary>
		private void RaiseSelectionChanged()
		{
			SelectionChanged?.Invoke(SelectedItem.Value);
		}

		#endregion
	}
}
