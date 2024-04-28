using System.Windows;
using System.Windows.Input;

namespace CanvasEditor.Interfaces
{
    public interface IDecorator
    {
        UIElement Parent { get; }
        
        bool IsActive { get; }
    }
}
