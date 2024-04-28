namespace CanvasEditor.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExecutionPriorityAttribute : Attribute
    {
        public int Priority;
        public ExecutionPriorityAttribute(int priority) {
            Priority = priority;
        }
    }
}
