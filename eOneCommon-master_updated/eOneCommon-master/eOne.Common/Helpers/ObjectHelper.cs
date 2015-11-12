using System;
using System.Collections;
using System.Collections.Generic;

namespace eOne.Common.Helpers
{
    public class ObjectHelper
    {

        public static IList CreateListObject(Type type)
        {
            var genericListType = typeof(List<>).MakeGenericType(type);
            return (IList)Activator.CreateInstance(genericListType);
        }

        public static object CreateObject(Type type)
        {
            return Activator.CreateInstance(type);
        }

    }
}
