using CanvasEditor.Adorners;
using CanvasEditor.Attributes;
using CanvasEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace CanvasEditor.Decorator
{
    [ExecutionPriority(0)]
    public class MousePositionTextDecorator: DecoratorBase, IDecorator
    {
        private MousePositionTextAdorner? _mouseTextAdorner;
        private AdornerLayer? _adornerLayer;

        public MousePositionTextDecorator(UIElement parent) : base(parent)
        {
            Parent.MouseEnter += Parent_MouseEnter;
            Parent.MouseLeave += Parent_MouseLeave;
            Parent.MouseMove += Parent_MouseMove;
        }

        private void Parent_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_mouseTextAdorner != null) {
                var position = e.GetPosition(Parent);
                _mouseTextAdorner.SetPosition(position);
            }
        }

        private void Parent_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(_mouseTextAdorner != null)
            {
                Mouse.Capture(null);
                _adornerLayer?.Remove(_mouseTextAdorner);
            }
        }

        private void Parent_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_adornerLayer == null)
                _adornerLayer = AdornerLayer.GetAdornerLayer(Parent);
            _mouseTextAdorner = new MousePositionTextAdorner(Parent, e.GetPosition(Parent));
            _adornerLayer.Add(_mouseTextAdorner);
        }
    }
}
