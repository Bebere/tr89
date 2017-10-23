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
    // We define the following class and methods to convert between the matching pairs.
    public static class DelegateCast
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
    }
}