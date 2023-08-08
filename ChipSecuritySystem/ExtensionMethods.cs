using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Return an updated <paramref name="sequence"/> with <paramref name="element"/> appended to the end.
        /// </summary>
        public static IEnumerable<T> ExtendWith<T>(this IEnumerable<T> sequence, T element)
        {
            foreach (T item in sequence) yield return item;
            yield return element;
        }

        /// <summary>
        /// Return the provided IEnumerable <paramref name="sequence"/> with the first item removed
        /// that is equal to <paramref name="element"/> per <paramref name="areEqual"/>.
        /// </summary>
        public static IEnumerable<T> RemoveFirstInstance<T>(this IEnumerable<T> sequence, T element, IEqualityComparer<T> comparer)
        {
            bool skipped = false;

            foreach(T item in sequence)
            {
                if (!skipped && comparer.Equals(item, element))
                {
                    skipped = true;
                }
                else
                {
                    yield return item;
                }
            }
        }

    }
}
