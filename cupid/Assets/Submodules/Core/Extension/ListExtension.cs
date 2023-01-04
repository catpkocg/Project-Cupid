using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wayway.Engine
{
    public static class ListExtension
    {
        public static bool IsNullOrEmpty<T>(this List<T> list) => list == null || list.Count == 0;
        public static void AddUniquely<T>(this List<T> list, T item)
        {
            if (list.Contains(item)) return;
            list.Add(item);
        }

        public static void RemoveSafely<T>(this List<T> list, T item)
        {
            if (!list.Contains(item)) return;
            list.Remove(item);
        }

        public static void RemoveNull<T>(this List<T> list)
        {
            list.RemoveAll(x => x is null);
        }

        public static T Random<T>(this List<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            // note: creating a Random instance each call may not be correct for you,
            // consider a thread-safe static instance
            var r = new System.Random();            
            return list.Count == 0 ? default : list[r.Next(0, list.Count)];
        }

        public static TSource MinBy<TSource, TKey>(this List<TSource> list, Func<TSource, TKey> selector) => list.MinBy(selector, null);
        public static TSource MinBy<TSource, TKey>(this List<TSource> list, Func<TSource, TKey> selector, IComparer<TKey> comparer)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            comparer ??= Comparer<TKey>.Default;

            using var sourceIterator = list.GetEnumerator();

            if (!sourceIterator.MoveNext())
            {
                return default;
            }

            var min = sourceIterator.Current;
            var minKey = selector(min);

            while (sourceIterator.MoveNext())
            {
                var candidate = sourceIterator.Current;
                var candidateProjected = selector(candidate);

                if (comparer.Compare(candidateProjected, minKey) < 0)
                {
                    min = candidate;
                    minKey = candidateProjected;
                }
            }

            return min;
        }

        public static TSource MaxBy<TSource, TKey>(this List<TSource> list, Func<TSource, TKey> selector) => list.MaxBy(selector, null);
        public static TSource MaxBy<TSource, TKey>(this List<TSource> list, Func<TSource, TKey> selector, IComparer<TKey> comparer)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            comparer ??= Comparer<TKey>.Default;

            using var sourceIterator = list.GetEnumerator();

            if (!sourceIterator.MoveNext())
            {
                throw new InvalidOperationException("Sequence contains no elements");
            }

            var max = sourceIterator.Current;
            var maxKey = selector(max);

            while (sourceIterator.MoveNext())
            {
                var candidate = sourceIterator.Current;
                var candidateProjected = selector(candidate);

                if (comparer.Compare(candidateProjected, maxKey) > 0)
                {
                    max = candidate;
                    maxKey = candidateProjected;
                }
            }

            return max;
        }

        public static void TakeFrom<T>(this List<T> list, List<T> target, int count)
        {
            if (target.IsNullOrEmpty()) return;
            if (count > target.Count)
            {               
                Debug.LogWarning($"List.Take(n)::n is must be smaller than List.Count current n:{count}, list.Count:{target.Count}" +
                    $"return origin list itself");

                list = target;
            }
            if (count < 0)
            {
                Debug.LogWarning($"List.Take(n)::n is must be larger than 0 current n:{count}, list.Count:{target.Count}" +
                    $"return origin list itself");

                list = target;
            }

            if (list.Count > count)
            {
                var beRemoveCount = list.Count - count;

                list.RemoveRange(count, beRemoveCount);
            }

            for (var i = 0; i < count; ++i)
            {
                if (list.Count <= i) list.Add(target[i]);
                else
                    list[i] = target[i];
            }
        }

        public static void Swap<T>(this List<T> list, T firstItem, T secondItem) => Swap(list, list.IndexOf(firstItem), list.IndexOf(secondItem));
        public static void Swap<T>(this List<T> list, int firstIndex, T secondItem) => Swap(list, firstIndex, list.IndexOf(secondItem));
        public static void Swap<T>(this List<T> list, T firstItem, int secondIndex) => Swap(list, list.IndexOf(firstItem), secondIndex);
        public static void Swap<T>(this List<T> list, int firstIndex, int secondIndex)
        {
            if (firstIndex == secondIndex)
            {
                return;
            }

            (list[firstIndex], list[secondIndex]) = (list[secondIndex], list[firstIndex]);
        }

        public static void TraverseByTwoElement<T>(this List<T> list)
        {
            
        }
    }
}


