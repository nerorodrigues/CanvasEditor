using CanvasEditor.Components;
using System.Windows;
using System.Windows.Markup;

namespace CanvasEditor.Models
{
    public class SelectableItem: UIElement
    {
        public static void SetIsSelected(UIElement target, bool isSelected) => target.SetValue(IsSelectedProperty, isSelected);
        public static bool GetIsSelected(UIElement target) => (bool)target.GetValue(IsSelectedProperty);

        // Using a DependencyProperty as the backing store for IsSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.RegisterAttached("IsSelected", typeof(bool), 
                typeof(UIElement), 
                new FrameworkPropertyMetadata(defaultValue: false, flags: FrameworkPropertyMetadataOptions.AffectsRender));

        public static UIElement? GetSelectedItem(DependencyObject obj) => (UIElement?)obj.GetValue(SelectedItemProperty);

        public static void SetSelectedItem(DependencyObject obj, UIElement? value) => obj.SetValue(SelectedItemProperty, value);

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.RegisterAttached("SelectedItem", typeof(UIElement), typeof(IAddChild), 
                new FrameworkPropertyMetadata(defaultValue: null, flags: FrameworkPropertyMetadataOptions.AffectsRender));


    }
}
