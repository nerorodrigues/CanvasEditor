using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace CanvasEditor.Adorners
{
    public class MoveItemAdorner : Adorner
    {
        private Vector _elementSize;
        private readonly Point _initialPosition;
        private Point _currentPosition;
        private readonly SolidColorBrush _brush;

        public MoveItemAdorner(UIElement adornedElement, UIElement elementToMove) : base(adornedElement)
        {
            _initialPosition = new Point(Canvas.GetLeft(elementToMove), Canvas.GetTop(elementToMove));
            _elementSize = ((Vector)elementToMove.DesiredSize);
            _currentPosition = _initialPosition;
            _brush = new SolidColorBrush(Colors.LightBlue);
            _brush.Opacity = 0.4;
        }

        public void UpdatePosition(Vector offset)
        {
            _currentPosition = Point.Subtract(_initialPosition, offset);
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawRectangle(_brush, new Pen(Brushes.Blue, 0.5), GetRect());

        }

        private Rect GetRect()
        {
            return new Rect(_currentPosition, _elementSize);
        }
    }
}
