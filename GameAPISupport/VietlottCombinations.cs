using System.Collections.Generic;
using System.Linq;

namespace GameAPISupport
{
    public static class VietlottCombinations
    {
        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> elements, int k)
        {
            return k == 0 ? new[] { new T[0] } :
              elements.SelectMany((e, i) =>
                elements.Skip(i + 1).Combinations(k - 1).Select(c => (new[] { e }).Concat(c)));
        }
        //TODO: how to use: var result = Combinations(new[] { 1, 2, 3, 4, 5 }, 3);
    }
}
