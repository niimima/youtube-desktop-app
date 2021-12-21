﻿using PlaylistEditor.Models;
using PlaylistEditor.Services;
using PlaylistEditor.ViewModels.Dialogs;
using PlaylistEditor.ViewModels.Interfaces;
using PlaylistEditor.Views;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
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
			SelectedItem = new ReactivePropertySlim<PlaylistListViewItemViewModel>().AddTo(m_Disposables);
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
		public ReactivePropertySlim<PlaylistListViewItemViewModel> SelectedItem { get; set; }

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
			var playlists = await m_YouTubeService.GetMyPlaylists();
			foreach(var playlist in playlists)
			{
				PlaylistList.Add(new PlaylistListViewItemViewModel(playlist, m_WebClientService));
			}
		}

		/// <summary>
		/// プレイリストを追加する
		/// </summary>
		public async Task AddPlaylistAsync()
		{
			var resultVm = await ShowAddPlaylistDialog.Handle(Unit.Default);
		}

		public void RemovePlaylist()
		{

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
