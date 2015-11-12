using System;
using System.Reflection;

namespace eOne.Common.Helpers
{
    public class FieldPropertyHelper
    {

        public static PropertyInfo GetFieldPropertyInfo(Type type, string fieldName)
        {
            return type.GetProperty(fieldName);
        }

    }
}
