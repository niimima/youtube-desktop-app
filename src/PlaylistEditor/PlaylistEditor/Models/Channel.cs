using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistEditor.Models
{
	/// <summary>
	/// チャンネル
	/// </summary>
    public class Channel
    {
		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="channelId">動画のID</param>
		/// <param name="title">タイトル</param>
		/// <param name="description">概要</param>
		/// <param name="thumbnailUrl">サムネイルのURL</param>
        public Channel(string channelId, string title, string description, string thumbnailUrl)
        {
            ChannelId = channelId;
            Title = title;
            Description = description;
            ThumbnailUrl = thumbnailUrl;
        }

        #endregion

        #region プロパティ

		/// <summary>
		/// チャンネルのID
		/// </summary>
		public string ChannelId { get; }

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
