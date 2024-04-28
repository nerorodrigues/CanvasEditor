using CanvasEditor.Adorners;
using CanvasEditor.Attributes;
using CanvasEditor.Helpers;
using CanvasEditor.Interfaces;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using static CanvasEditor.Helpers.SelectedItemHelper;

namespace CanvasEditor.Decorator
{
    [ExecutionPriority(-1)]
    internal class ResizeDecorator : DecoratorBase, IDecorator
    {
        private const double Radius = 10;
        private UIElement? _selectedItem;
        private ResizeAdorner? _resizeAdorner;
        private Anchor _anchorUnderTheMouse;
        private AdornerLayer _adornerLayer;
        private bool _startResizing;

        public ResizeDecorator(UIElement parent) : base(parent)
        {
            Parent.MouseMove += Parent_MouseMove;
            Parent.MouseLeftButtonDown += Parent_MouseLeftButtonDown;
            Parent.MouseLeftButtonUp += Parent_MouseLeftButtonUp;
            parent.KeyDown += Parent_KeyDown;
            ((Components.CanvasEditor)Parent).OnSelectedItemChanged += ResizeDecorator_OnSelectedItemChanged;
        }

        private void Parent_KeyDown(object sender, KeyEventArgs e)
        {
            if(IsActive && _anchorUnderTheMouse != null  && e.Key == Key.Escape)
            {
                DisableAdorner();
            }
        }

        private void ResizeDecorator_OnSelectedItemChanged(object sender, Events.SelectedItemChangedEventArgs e)
        {
            if (_adornerLayer == null)
                _adornerLayer = AdornerLayer.GetAdornerLayer(Parent);

            _selectedItem = e.SelectedItem;
            IsActive = _selectedItem != null;
            if(IsActive)
            {
                Debug.WriteLine("ResizeDecorator activated");
            }
        }

        private void Parent_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_anchorUnderTheMouse != null && _selectedItem != null)
            {
                _resizeAdorner = new ResizeAdorner(_selectedItem);
                _adornerLayer.Add(_resizeAdorner);
                _startResizing = true;
                e.Handled = true;
            }
        }

        private void Parent_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_anchorUnderTheMouse != null)
            {
                DisableAdorner();
                _startResizing = false;
                e.Handled = true;
            }
        }

        private void Parent_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (IsActive)
            {
                if (!_startResizing)
                {
                    Debug.WriteLine("Searching for anchor.");
                    var position = Mouse.GetPosition(Parent);
                    var anchor = AnchorUnderTheMouse(position);
                    if (_anchorUnderTheMouse == null && anchor != null)
                    {
                        Debug.WriteLine("Found.");
                        Mouse.Capture(Parent, CaptureMode.SubTree);
                    }
                    else if (_anchorUnderTheMouse != null && anchor == null)
                    {
                        Mouse.Capture(null);
                    }
                    _anchorUnderTheMouse = anchor;
                    if (_anchorUnderTheMouse != null)
                    {
                        SetCursor(_anchorUnderTheMouse);
                        Debug.WriteLine("Mouse is over anchor");
                    }
                }
                else
                {
                    var mousePosition = e.GetPosition(Parent);
                    var offSet = GetOffset(mousePosition);
                    _resizeAdorner.ApplyOffset(offSet, _anchorUnderTheMouse.AnchorDirection);
                    SetCursor(_anchorUnderTheMouse);
                }
            }
        }

        private Anchor? AnchorUnderTheMouse(Point mousePosition)
        {
            var anchors = SelectedItemHelper.GetAnchorsSelectedItem(_selectedItem, true).ToArray();
            return anchors.FirstOrDefault(pX => IsPointInsideCircunference(pX.Position, mousePosition));
        }

        private bool IsPointInsideCircunference(Point circunference, Point position)
        {
            return Math.Pow((position.X - circunference.X), 2) + Math.Pow((position.Y - circunference.Y), 2) <= Math.Pow(Radius, 2);
        }

        private void SetCursor(Anchor anchor)
        {
            switch (anchor.AnchorDirection)
            {
                case AnchorDirection.TopLeft:
                case AnchorDirection.BottomRight:
                    Mouse.SetCursor(Cursors.SizeNWSE);
                    break;
                case AnchorDirection.TopRight:
                case AnchorDirection.BottomLeft:
                    Mouse.SetCursor(Cursors.SizeNESW);
                    break;
            }

        }

        public Vector GetOffset(Point mousePosition)
        {
            if(_anchorUnderTheMouse == null)
                return new Vector(0,0);
            //switch (_anchorUnderTheMouse.AnchorDirection)
            //{
            //    case AnchorDirection.TopLeft:
            //        return Rect()
            //        break;
            //    case AnchorDirection.TopRight:
            //        break;
            //    case AnchorDirection.BottomLeft:
            //        break;
            //    case AnchorDirection.BottomRight:
            //        break;
            //    default:
            //        break;
            //}
            var offset = _anchorUnderTheMouse.Position - mousePosition;
            return new Vector(offset.X, offset.Y);
        }

        private void DisableAdorner()
        {
            if (_resizeAdorner != null)
            {
                _adornerLayer?.Remove(_resizeAdorner);
                _resizeAdorner = null;
            }
        }
    }
}
