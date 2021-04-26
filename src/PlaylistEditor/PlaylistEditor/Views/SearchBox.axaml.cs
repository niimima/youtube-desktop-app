using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PlaylistEditor.Views
{
    public class SearchBox : UserControl
    {
        public SearchBox()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
