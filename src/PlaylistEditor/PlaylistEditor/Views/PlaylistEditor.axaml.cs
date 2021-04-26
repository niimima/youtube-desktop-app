using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PlaylistEditor.Views
{
    public class PlaylistEditor : UserControl
    {
        public PlaylistEditor()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
