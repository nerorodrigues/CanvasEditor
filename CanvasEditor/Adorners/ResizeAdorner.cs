using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using static CanvasEditor.Helpers.SelectedItemHelper;

namespace CanvasEditor.Adorners
{
    internal class ResizeAdorner : Adorner
    {
        private const double Radius = 4.5;
        private Rect? _elementSize;
        private SolidColorBrush _brush = new SolidColorBrush(Colors.LightCyan);
        private Pen _pen = new Pen(Brushes.Blue, 0.5);
        private double _minHeight = 0;
        private double _minWidth = 0;

        public ResizeAdorner(UIElement adornedElement) : base(adornedElement)
        {
            _elementSize = new Rect(AdornedElement.DesiredSize);
            var frameworkElement = AdornedElement as FrameworkElement;
            if (frameworkElement != null)
            {
                _minHeight = frameworkElement.MinHeight;
                _minWidth = frameworkElement.MinWidth;
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (_elementSize.HasValue)
            {
                _brush.Opacity = 0.8;
                drawingContext.DrawRectangle(_brush, _pen, _elementSize.Value);
            }
        }

        public void ApplyOffset(Vector offset, AnchorDirection anchorDirection)
        {
            if (_elementSize.HasValue)
            {
                var height = _elementSize.Value.Height - offset.Y;
                var width = _elementSize.Value.Width - offset.X;
                var newRect = new Rect(new Size(width < _minWidth ? _minWidth : width, height < _minHeight ? _minHeight : height));
                switch (anchorDirection)
                {
                    case AnchorDirection.TopLeft:
                        newRect.X = _elementSize.Value.X - offset.X;
                        newRect.Y = _elementSize.Value.Y - offset.Y;
                        break;
                    case AnchorDirection.TopRight:
                        newRect.Y = _elementSize.Value.Y - offset.Y;
                        break;
                    case AnchorDirection.BottomLeft:
                        newRect.X = _elementSize.Value.Y - offset.Y;
                        break;
                }
                _elementSize = newRect;
            }
            InvalidateVisual();
        }
    }
}
