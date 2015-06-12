using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AU
{

    public static class LinqExtend
    {

        public static T GetInterface<T>(this GameObject inObj) where T : class
        {
            return inObj.GetComponents<Component>().OfType<T>().FirstOrDefault();
        }

        public static IEnumerable<T> GetInterfaces<T>(this GameObject inObj) where T : class
        {
            return inObj.GetComponents<Component>().OfType<T>();
        }

        public static void Resize<T>(this List<T> list, int size, T element = default(T))
        {
            int count = list.Count;

            if (size < count)
            {
                list.RemoveRange(size, count - size);
            }
            else if (size > count)
            {
                if (size > list.Capacity)   // Optimization
                    list.Capacity = size;

                list.AddRange(Enumerable.Repeat(element, size - count));
            }
        }
    }

}