// Standard delegates to use for function values
namespace System
{
    public delegate A Function<A>();
    public delegate B Function<A, B>(A argA);
    public delegate C Function<A, B, C>(A argA, B argB);
    public delegate D Function<A, B, C, D>(A argA, B argB, C argC);
    public delegate E Function<A, B, C, D, E>(A argA, B argB, C argC, D argD);
    public delegate F Function<A, B, C, D, E, F>(A argA, B argB, C argC, D argD, E argE);

    // System.Predicate<T> is equivalent to System.Function<T, bool>,
    // System.Converter<T, U> is equivalent to System.Function<T, U>,
    // and System.Comparison<T> is equivalent to System.Function<T, T, int>.
    // Unfortunately structural equivalence is not available on delegate types
    // so these types are not interchangeable.
    // Likewise for the pairs Func (from Framework 4.0)/Function (from TR/87).
    // We define the following class and methods to convert between the matching pairs.
    public static partial class DelegateCast
    {
        public static Function<T, bool> ToFunction<T>(Predicate<T> pred)
        {
            return new Function<T, bool>(pred);
        }

        public static Predicate<T> ToPredicate<T>(Function<T, bool> func)
        {
            return new Predicate<T>(func);
        }

        public static Function<T, U> ToFunction<T, U>(Converter<T, U> conv)
        {
            return new Function<T, U>(conv);
        }

        public static Converter<T, U> ToConverter<T, U>(Function<T, U> func)
        {
            return new Converter<T, U>(func);
        }

        public static Function<T, T, int> ToFunction<T>(Comparison<T> comp)
        {
            return new Function<T, T, int>(comp);
        }

        public static Comparison<T> ToComparison<T>(Function<T, T, int> func)
        {
            return new Comparison<T>(func);
        }

        #if (!NET20 && !NET35)
            public static Function<T> ToFunction<T>(Func<T> f)
            {
                return new Function<T>(f);
            }

            public static Func<T> ToFunc<T>(Function<T> func)
            {
                return new Func<T>(func);
            }

            public static Function<T, U> ToFunction<T, U>(Func<T, U> f)
            {
                return new Function<T, U>(f);
            }

            public static Func<T, U> ToFunc<T, U>(Function<T, U> func)
            {
                return new Func<T, U>(func);
            }

            public static Function<T, U, V> ToFunction<T, U, V>(Func<T, U, V> f)
            {
                return new Function<T, U, V>(f);
            }

            public static Func<T, U, V> ToFunc<T, U, V>(Function<T, U, V> func)
            {
                return new Func<T, U, V>(func);
            }
        
            public static Function<T, U, V, W> ToFunction<T, U, V, W>(Func<T, U, V, W> f)
            {
                return new Function<T, U, V, W>(f);
            }

            public static Func<T, U, V, W> ToFunc<T, U, V, W>(Function<T, U, V, W> func)
            {
                return new Func<T, U, V, W>(func);
            }

            public static Function<T, U, V, W, X> ToFunction<T, U, V, W, X>(Func<T, U, V, W, X> f)
            {
                return new Function<T, U, V, W, X>(f);
            }

            public static Func<T, U, V, W, X> ToFunc<T, U, V, W, X>(Function<T, U, V, W, X> func)
            {
                return new Func<T, U, V, W, X>(func);
            }
        
            public static Function<T, U, V, W, X, Y> ToFunction<T, U, V, W, X, Y>(Func<T, U, V, W, X, Y> f)
            {
                return new Function<T, U, V, W, X, Y>(f);
            }

            public static Func<T, U, V, W, X, Y> ToFunc<T, U, V, W, X, Y>(Function<T, U, V, W, X, Y> func)
            {
                return new Func<T, U, V, W, X, Y>(func);
            }
        #endif
    }
}