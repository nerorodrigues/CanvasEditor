using CanvasEditor.Adorners;
using CanvasEditor.Models;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CanvasEditor.Components
{
    public class EditorRetangle : Shape
    {
        public EditorRetangle() {
            Fill = Brushes.Black;
        }
        protected override Geometry DefiningGeometry => new RectangleGeometry(new Rect(DesiredSize));
    }
}
