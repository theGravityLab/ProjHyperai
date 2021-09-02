using System.Collections.Generic;

namespace Hyperai.Receipts
{
    public class GenericReceipt
    {
        public bool Successful { get; set; } = true;
        public Dictionary<string, object> Fields { get; set; } = new();

        public object this[string key]
        {
            get
            {
                if (Fields.ContainsKey(key))
                    return Fields[key];
                return null;
            }
            set
            {
                if (Fields.ContainsKey(key))
                    Fields[key] = value;
                else
                    Fields.Add(key, value);
            }
        }

        public T Value<T>(string key)
        {
            if (Fields.ContainsKey(key))
                return (T) Fields[key];
            return default;
        }
    }
}
