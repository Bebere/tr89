// Standard delegates to use for procedure values
//
// Note: void Action<A>(A argA) is already defined in System, it is included
// here as a comment for completeness.
namespace System
{
    public delegate void Action();
    // public delegate void Action<A>(A argA);
    public delegate void Action<A, B>(A argA, B argB);
    public delegate void Action<A, B, C>(A argA, B argB, C argC);
    public delegate void Action<A, B, C, D>(A argA, B argB, C argC, D argD);
    public delegate void Action<A, B, C, D, E>(A argA, B argB, C argC, D argD, E argE);
}