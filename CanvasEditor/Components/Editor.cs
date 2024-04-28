using CanvasEditor.Events;
using CanvasEditor.Helpers;
using CanvasEditor.Models;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CanvasEditor.Components
{
    public class CanvasEditor : Canvas
    {
        #region Events
        public delegate void SelectedItemChanged(object sender, SelectedItemChangedEventArgs e);

        public event SelectedItemChanged OnSelectedItemChanged;
        #endregion

        public CanvasEditor()
        {
            var decorators = DecoratorManager.Instance.StackDecorators(this).ToArray();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            Mouse.Capture(this, CaptureMode.SubTree);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            Mouse.Capture(null);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property.Name == SelectableItem.SelectedItemProperty.Name)
            {
                OnSelectedItemChanged?.Invoke(this, new SelectedItemChangedEventArgs(e.NewValue as UIElement, e.OldValue as UIElement));
                Debug.WriteLine($"Property Changed {e.Property.Name} - OldValues: {e.OldValue} | NewValue {e.NewValue}");
            }
        }

    }
}
