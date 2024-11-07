using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Extensions.LinqExt {

    public static class LInqExtension {

        public static void ForEach<T>(this IEnumerable<T> ts, System.Action<T> action) {
            foreach (var t in ts) action(t);
        }

		public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> iter) {
			while (iter.MoveNext()) yield return iter.Current;
		}
    }
}