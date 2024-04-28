using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace CanvasEditor.Adorners
{
    internal class Test : Adorner
    {
        private readonly Point point;

        public Test(UIElement adornedElement, Point point) : base(adornedElement)
        {
            this.point = point;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawEllipse(Brushes.Coral, new Pen(Brushes.Red, 1), this.point, 5,5);
        }
    }
}
