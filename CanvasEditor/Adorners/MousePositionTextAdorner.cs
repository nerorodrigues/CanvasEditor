using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace CanvasEditor.Adorners
{
    internal class MousePositionTextAdorner(UIElement adornedElement, Point point) : Adorner(adornedElement)
    {
        private Point? _point;
        private Typeface _typeface = new Typeface("Arial");
        public void SetPosition(Point point)
        {
            _point = new Point(point.X, point.Y - 11);
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (_point.HasValue)
            {
                Debug.WriteLine("Updated");
                drawingContext.DrawText(new FormattedText($"X: {point.X} | Y: {point.Y})", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, _typeface,11, Brushes.Black, 1),
                    _point.Value);
            }
        }
    }
}
