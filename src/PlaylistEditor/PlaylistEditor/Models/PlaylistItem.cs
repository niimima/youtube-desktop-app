﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistEditor.Models
{
	/// <summary>
	/// プレイリストアイテム
	/// </summary>
	public class PlaylistItem
	{
		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="id">ID</param>
		/// <param name="title">タイトル</param>
		/// <param name="description">概要</param>
		/// <param name="thumbnailUrl">サムネイルのURL</param>
		public PlaylistItem(string id, string title, string description, string thumbnailUrl)
		{
			Id = id;
			Title = title;
			Description = description;
			ThumbnailUrl = thumbnailUrl;
		}

		#endregion

		#region プロパティ

		/// <summary>
		/// ID
		/// </summary>
		public string Id { get; }

		/// <summary>
		/// タイトル
		/// </summary>
		public string Title { get; }

		/// <summary>
		/// 概要
		/// </summary>
		public string Description { get; }

		/// <summary>
		/// サムネイルのURL
		/// </summary>
		public string ThumbnailUrl { get; }

		#endregion
	}
}
