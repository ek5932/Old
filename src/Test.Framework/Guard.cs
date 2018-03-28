using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.Framework
{
    public static class Guard
    {
        public static void IsNotNull(object value, string property)
        {
            if (value == null)
                throw new ArgumentNullException($"{value} is Null");
        }

        public static void IsNotNullOrEmpty(string value, string property)
        {
            IsNotNull(value, property);
            if(property == string.Empty)
                throw new ArgumentException($"{value} is Empty"); 
        }

        public static void IsNotNullOrEmpty<T>(IEnumerable<T> value, string property)
        {
            IsNotNull(value, property);
            if (property.Count() == 0)
                throw new ArgumentException($"{value} is Empty");
        }
    }
}
