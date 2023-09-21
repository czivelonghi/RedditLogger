namespace UnitTest.Common
{

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal class TestPriorityAttribute : Attribute
    {
        public int Priority { get; private set; }

        public TestPriorityAttribute(int priority) => Priority = priority;
    }
}
