// Standard delegates to use for procedure values
//
// Note: void Action<A>(A argA) is already defined in System from Framework 2.0 on, so it’s always forwarded, whereas the others are forwarded from Framework 4.0 on (3.5 for Action<T>).
using System.Runtime.CompilerServices;
using System.Threading;

[assembly: TypeForwardedTo(typeof(System.Action<>))]
#if !NET20
    [assembly: TypeForwardedTo(typeof(System.Action))]
#endif
#if (!NET20 && !NET35)
    [assembly: TypeForwardedTo(typeof(System.Action<,>))]
    [assembly: TypeForwardedTo(typeof(System.Action<,,>))]
    [assembly: TypeForwardedTo(typeof(System.Action<,,,>))]
    [assembly: TypeForwardedTo(typeof(System.Action<,,,,>))]
#endif
namespace System
{
    #if NET20
        public delegate void Action();
    #endif
    #if (NET20 || NET35)
        public delegate void Action<A, B>(A argA, B argB);
        public delegate void Action<A, B, C>(A argA, B argB, C argC);
        public delegate void Action<A, B, C, D>(A argA, B argB, C argC, D argD);
        public delegate void Action<A, B, C, D, E>(A argA, B argB, C argC, D argD, E argE);
    #endif

    // System.Action is equivalent to System.Threading.ThreadStart
    // and System.Action<object> is equivalent to System.Threading.ParameterizedThreadStart.
    // Unfortunately structural equivalence is not available on delegate types
    // so these types are not interchangeable.
    // We define the following class and methods to convert between the matching pairs.
    public static partial class DelegateCast
    {
        public static Action ToAction(ThreadStart thread)
        {
            return new Action(thread);
        }

        public static ThreadStart ToThreadStart(Action act)
        {
            return new ThreadStart(act);
        }

        public static Action<object> ToAction(ParameterizedThreadStart parametrisedThread)
        {
            return new Action<object>(parametrisedThread);
        }

        public static ParameterizedThreadStart ToParameterizedThreadStart(Action<object> act)
        {
            return new ParameterizedThreadStart(act);
        }
    }
}