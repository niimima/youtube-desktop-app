using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PlaylistEditor.Views
{
	public partial class SearchFormView : UserControl
	{
		public SearchFormView()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
