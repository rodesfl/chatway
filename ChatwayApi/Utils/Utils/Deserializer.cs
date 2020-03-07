using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Utils {
    public class Deserializer {

        public static T GetObject<T>(Dictionary<string, object> dict) {
            Type type = typeof(T);
            var obj = Activator.CreateInstance(type);

            foreach (var kv in dict) {
                type.GetProperty(kv.Key).SetValue(obj, kv.Value);
            }
            return (T)obj;
        }
    }
}
