using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EphemeralEx.Extensions
{
    public static class TreeExtensions
    {
        public static IEnumerable<T> TraverseTree<T>(this T element, Func<T, IEnumerable<T>> detree)
        {
            yield return element;

            var subElements = detree(element)
                .SelectMany(subParent => subParent.TraverseTree(detree));

            foreach (var subElement in subElements)
            {
                yield return subElement;
            }
        }
    }
}
