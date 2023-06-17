using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace EchoPost.Application.Common.Extensions;

public static class CollectionExtensions
{

    /// <summary>
    /// This method allows the usage of functional style for each loops on all IEnumerable
    /// <example>
    /// <code>
    ///  myEnumerable.ForEach(item => Console.WriteLine(item));
    /// </code>
    /// </example>
    /// </summary>
    public static void ForEach<T>(this IEnumerable<T> p_collection, Action<T> p_func)
    {
        foreach (var p_var in p_collection)
        {
            p_func.Invoke(p_var);
        }
    }

    /// <summary>
    /// This method allows the usage of functional style for each loops with index on all IEnumerable
    /// <example>
    /// <code>
    ///  myEnumerable.ForEach((item, index) => Console.WriteLine($"Item at index {index}: {item}"));
    /// </code>
    /// </example>
    /// </summary>
    public static void ForEach<T>(this IEnumerable<T> p_collection, Action<T, int> p_func)
    {
        for (int i = 0; i < p_collection.Count(); i++)
        {
            p_func.Invoke(p_collection.ElementAt(i), i);
        }
    }

}