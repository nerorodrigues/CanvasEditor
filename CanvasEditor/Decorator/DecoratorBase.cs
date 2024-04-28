using CanvasEditor.Interfaces;
using System.Windows;
using System.Windows.Input;

namespace CanvasEditor.Decorator
{
    public abstract class DecoratorBase //: IDecorator
    {
        public UIElement Parent { get; }
        
        public bool IsActive { get; internal set; }

        public DecoratorBase(UIElement parent)
        {
            Parent = parent;
        }
    }
}
