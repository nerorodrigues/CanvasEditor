using CanvasEditor.Helpers;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace CanvasEditor.Adorners
{
    internal class SelectedAdorner(UIElement adornedElement) : Adorner(adornedElement)
    {
        private const double Radius = 5.0;

        protected override void OnRender(DrawingContext drawingContext)
        {
            
            var brush = new SolidColorBrush(Colors.LightCyan);
            brush.Opacity = 0.8;
            var selectionAnchors = SelectedItemHelper.GetAnchorsSelectedItem(AdornedElement);
            foreach (var anchor in selectionAnchors)
            {
                drawingContext.DrawEllipse(brush, new Pen(Brushes.Blue, 0.5), anchor.Position, Radius, Radius);
            }
        }
    }
}
