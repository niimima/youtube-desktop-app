using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PlaylistEditor.Views
{
	public partial class PlaylistContentView : UserControl
	{
		public PlaylistContentView()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
