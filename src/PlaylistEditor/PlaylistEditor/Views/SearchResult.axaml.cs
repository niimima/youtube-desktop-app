using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PlaylistEditor.Views
{
    public class SearchResult : UserControl
    {
        public SearchResult()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
