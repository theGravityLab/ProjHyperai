using System;

namespace Hyperai.Units.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FilterByAttribute : Attribute
    {
        public FilterByAttribute(IFilter filter, string message = null)
        {
            Filter = filter;
            FailureMessage = message;
        }

        public IFilter Filter { get; set; }
        public string FailureMessage { get; set; }
    }
}
