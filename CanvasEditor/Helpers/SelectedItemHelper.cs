using System.Windows;
using System.Windows.Controls;

namespace CanvasEditor.Helpers
{
    internal static class SelectedItemHelper
    {
        public static IEnumerable<Anchor> GetAnchorsSelectedItem(UIElement element, bool isParent =false)
        {
            var elementSize = new Rect(element.DesiredSize);
            var y = Canvas.GetTop(element);
            var x = Canvas.GetLeft(element);

            return new[] {
                new Anchor(isParent ? new Point(x, y) : elementSize.TopLeft, AnchorDirection.TopLeft), 
                new Anchor(isParent ? new Point (x + elementSize.Width , y) : elementSize.TopRight, AnchorDirection.TopRight),
                new Anchor(isParent ? new Point(x, y + elementSize.Height) : elementSize.BottomLeft, AnchorDirection.BottomLeft),
                new Anchor(isParent ? new Point(x + elementSize.Width, y + elementSize.Height) : elementSize.BottomRight, AnchorDirection.BottomRight)
            };
        }

        public class Anchor
        {
            public Point Position { get; private set;}
            public AnchorDirection AnchorDirection { get; private set;}
            public Anchor(Point position, AnchorDirection anchorDirection)
            {
                Position = position;
                AnchorDirection = anchorDirection;
            }
        }

        public enum AnchorDirection
        {
            TopLeft = 0,
            TopRight,
            BottomLeft,
            BottomRight
        }
    }
}
