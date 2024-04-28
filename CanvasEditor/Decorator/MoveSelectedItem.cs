using CanvasEditor.Adorners;
using CanvasEditor.Attributes;
using CanvasEditor.Interfaces;
using CanvasEditor.Models;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml.Linq;

namespace CanvasEditor.Decorator
{
    [ExecutionPriority(1)]
    public class MoveSelectedItem : DecoratorBase, IDecorator
    {
        private Point? _initialPosition;
        private AdornerLayer? _adornerLayer;
        private MoveItemAdorner? _moveItemAdorner;
        private UIElement? _elementoToMove;
        public MoveSelectedItem(UIElement parent) : base(parent)
        {
            Parent.MouseMove += Parent_MouseMove;
            Parent.MouseLeftButtonDown += Parent_MouseLeftButtonDown;
            Parent.MouseLeftButtonUp += Parent_MouseLeftButtonUp;
            Parent.KeyDown += Parent_KeyDown;
        }

        private void Parent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                _elementoToMove = null;
                _initialPosition = null;
            }
        }

        private void Parent_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_initialPosition.HasValue && _elementoToMove != null)
            {
                var position = e.GetPosition(Parent);
                var movedDistance = _initialPosition - position;
                var y = Canvas.GetTop(_elementoToMove);
                var x = Canvas.GetLeft(_elementoToMove);
                Canvas.SetTop(_elementoToMove, y - movedDistance.Value.Y);
                Canvas.SetLeft(_elementoToMove, x - movedDistance.Value.X);
                _adornerLayer?.Remove(_moveItemAdorner);
                _initialPosition = null;
                _elementoToMove = null;
                _adornerLayer = null;
                IsActive = false;
            }
        }

        private void Parent_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var elementUnderMouse = e.OriginalSource as UIElement;
            if (elementUnderMouse != null && elementUnderMouse != Parent)
            {
                _elementoToMove = elementUnderMouse;
                _initialPosition = e.GetPosition(Parent);
                _adornerLayer = AdornerLayer.GetAdornerLayer(Parent);
                _moveItemAdorner = new MoveItemAdorner(Parent, elementUnderMouse);
                _adornerLayer.Add(_moveItemAdorner);
                IsActive = true;
            }
        }

        private void Parent_MouseMove(object sender, MouseEventArgs e)
        {
            if (_initialPosition.HasValue)
            {
                var actualPosition = e.GetPosition(Parent);
                var newPosition = _initialPosition.Value - actualPosition;
                _moveItemAdorner?.UpdatePosition(((Vector)newPosition));
                e.Handled = true;
            }
        }
    }
}
