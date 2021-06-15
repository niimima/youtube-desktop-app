using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistEditor.Models
{
	/// <summary>
	/// 設定
	/// </summary>
	internal class Settings
	{
		/// <summary>
		/// クライエントID
		/// </summary>
		public string ClientId { get; set; } = string.Empty;

		/// <summary>
		/// クライエントシークレット
		/// </summary>
		public string ClientSecret { get; set; } = string.Empty;
	}
}
