using System.Windows;

namespace CanvasEditor.Events
{
    public class SelectedItemChangedEventArgs
    {
        public UIElement? OldSelectedItem { get; private set; }

        public UIElement? SelectedItem { get; private set;}

        public SelectedItemChangedEventArgs(UIElement? selectedItem, UIElement? oldSelectedItem)
        {
            SelectedItem = selectedItem;
            OldSelectedItem = oldSelectedItem;

        }
    }
}
