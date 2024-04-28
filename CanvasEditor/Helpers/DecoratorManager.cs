using CanvasEditor.Attributes;
using CanvasEditor.Interfaces;
using System.Reflection;
using System.Windows;
using System.Windows.Documents;

namespace CanvasEditor.Helpers
{
    internal class DecoratorManager
    {
        public static DecoratorManager Instance { get => new DecoratorManager(); }
        private DecoratorManager() { }

        public IEnumerable<IDecorator> StackDecorators(UIElement targetElement)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var targetType = targetElement.GetType();
            var types = currentAssembly.GetTypes();
            var decoratorTypes = types.Where(pX => typeof(IDecorator).IsAssignableFrom(pX) && !pX.IsInterface && !pX.IsAbstract);
            var decorators = decoratorTypes.OrderBy(pX =>
            {
                var attr = pX.GetCustomAttribute<ExecutionPriorityAttribute>();
                return attr?.Priority;
            });
            foreach (var decoratorType in decorators)
            {
                var constructor = decoratorType.GetConstructors();
                yield return constructor.FirstOrDefault()?.Invoke(new[] { targetElement }) as IDecorator;
            }

        }
    }
}
