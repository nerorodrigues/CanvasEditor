using CanvasEditor.Adorners;
using CanvasEditor.Attributes;
using CanvasEditor.Interfaces;
using CanvasEditor.Models;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Linq;

namespace CanvasEditor.Decorator
{
    [ExecutionPriority(0)]
    public class SelectItemDecorator : DecoratorBase, IDecorator
    {
        private AdornerLayer? _adornerLayer;
        private SelectedAdorner? _selectedItemAdorner;
        private UIElement? _selectedItem;

        public SelectItemDecorator(UIElement parent) : base(parent) 
        {
            Parent.MouseLeftButtonDown += _decoratedItem_MouseLeftButtonDown;
        }

        private void _decoratedItem_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var element = e.OriginalSource as UIElement;
            if (element != null)
            {
                if (element == Parent)
                    DeselectItem();
                else if (_selectedItem != element)
                {
                    SelectItem(element);
                    e.Handled = true;
                }
            }
        }

        private void SelectItem(UIElement selectedItem)
        {
            if (selectedItem != null)
            {
                IsActive = true;
                DeselectItem();
                _selectedItem = selectedItem;
                SelectableItem.SetSelectedItem(Parent, _selectedItem);
                SelectableItem.SetIsSelected(_selectedItem, true);
                _adornerLayer = AdornerLayer.GetAdornerLayer(_selectedItem);
                _selectedItemAdorner = new SelectedAdorner(_selectedItem);
                _adornerLayer.Add(_selectedItemAdorner);
            }
        }

        private void DeselectItem()
        {
            if (_selectedItem != null)
            {
                IsActive = false;
                SelectableItem.SetSelectedItem(Parent, null);
                SelectableItem.SetIsSelected(_selectedItem, false);
                _adornerLayer?.Remove(_selectedItemAdorner);
                _adornerLayer = null;
                _selectedItem = null;
            }
        }
    }
}
