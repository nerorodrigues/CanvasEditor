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
        private readonly Rect? _elementRect;
        private Rect? _elementNewRect;
        private SolidColorBrush _brush = new SolidColorBrush(Colors.LightCyan);
        private Pen _pen = new Pen(Brushes.Blue, 0.5);
        private double _minHeight = 0;
        private double _minWidth = 0;

        public ResizeAdorner(UIElement adornedElement) : base(adornedElement)
        {
            _elementRect = new Rect(AdornedElement.DesiredSize);
            var frameworkElement = AdornedElement as FrameworkElement;
            if (frameworkElement != null)
            {
                _minHeight = frameworkElement.MinHeight;
                _minWidth = frameworkElement.MinWidth;
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (_elementNewRect.HasValue)
            {
                _brush.Opacity = 0.8;
                drawingContext.DrawRectangle(_brush, _pen, _elementNewRect.Value);
            }
        }

        public Rect? GetElementRect() => _elementNewRect ?? _elementRect;

        public void ApplyOffset(Vector offset, AnchorDirection anchorDirection)
        {
            if (_elementRect.HasValue)
            {
                Double height = 0;
                Double width = 0;
                Double X = 0;
                Double Y = 0;
                
                switch (anchorDirection)
                {
                    case AnchorDirection.TopLeft:
                        height = _elementRect.Value.Height + offset.Y;
                        width = _elementRect.Value.Width + offset.X;
                        X = _elementRect.Value.X - offset.X;
                        Y = _elementRect.Value.Y - offset.Y;
                        break;
                    case AnchorDirection.TopRight:
                        Y = _elementRect.Value.Y - offset.Y;
                        height = _elementRect.Value.Height + offset.Y;
                        width = _elementRect.Value.Width - offset.X;
                        break;
                    case AnchorDirection.BottomLeft:
                        X = _elementRect.Value.X - offset.X;
                        height = _elementRect.Value.Height - offset.Y;
                        width = _elementRect.Value.Width + offset.X;
                        break;
                    case AnchorDirection.BottomRight:
                        height = _elementRect.Value.Height - offset.Y;
                        width = _elementRect.Value.Width - offset.X;
                        break;
                }
                var newRect = new Rect(X, Y, width < _minWidth ? _minWidth : width, height < _minHeight ? _minHeight : height);
                _elementNewRect = newRect;
            }
            InvalidateVisual();
        }
    }
}
