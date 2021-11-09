using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistEditor.Models
{
	/// <summary>
	/// 動画
	/// </summary>
	public class Video
	{
		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="videoId">動画のID</param>
		/// <param name="title">タイトル</param>
		/// <param name="description">概要</param>
		/// <param name="thumbnailUrl">サムネイルのURL</param>
		public Video(string videoId, string title, string description, string thumbnailUrl)
		{
			VideoId = videoId;
			Title = title;
			Description = description;
			ThumbnailUrl = thumbnailUrl;
		}

		#endregion

		#region プロパティ

		/// <summary>
		/// 動画のID
		/// </summary>
		public string VideoId { get; }

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
