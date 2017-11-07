// A public default constructor for a value type always exists.
// By the definition of Either<A, B> the default value is IsFirst is true
// and First is default(A).
using System.Runtime.InteropServices;

namespace System
{
    [Serializable, StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct Either<A, B>
    {
        [FieldOffset(0)]
        private readonly bool isB;
        [FieldOffset(sizeof(bool))]
        private readonly B bField;
        [FieldOffset(sizeof(bool))]
        private readonly A aField;

        private Either(A x, B y, bool d)
        {
            isB = d;
            if(d)
            {
                aField = default(A);
                bField = y;
            }
            else
            {
                bField = default(B);
                aField = x;
            }
        }

        public static Either<A, B> MakeFirst(A aValue)
        {
            return new Either<A, B>(aValue);
        }

        public bool IsFirst
        {
            get { return !isB; }
        }

        public A First
        {
            get
            {
                if (isB)
                    throw new InvalidOperationException();
                return aField;
            }
        }

        public static Either<A, B> MakeSecond(B bValue)
        {
            return new Either<A, B>(bValue);
        }

        public bool IsSecond
        {
            get { return isB; }
        }

        public B Second
        {
            get
            {
                if (!isB)
                    throw new InvalidOperationException();
                return bField;
            }
        }

        public static bool operator ==(Either<A, B> left, Either<A, B> right)
        {
            return left.isB == right.isB
                   ? (left.isB ? left.bField.Equals(right.bField)
                               : left.aField.Equals(right.aField)
                     )
                     : false;
        }

        public static bool operator !=(Either<A, B> left, Either<A, B> right)
        {
            return !(left == right);
        }

        public override bool Equals(object other)
        {
            return other is Either<A, B> && (this == (Either<A, B>)other);
        }

        public override int GetHashCode()
        {
            return isB ? bField.GetHashCode() : aField.GetHashCode();
        }

        public override string ToString()
        {
            return isB ? bField.ToString() : aField.ToString();
        }

        [CLSCompliant(false)] 
        public Either(A aValue) : this(aValue, default(B), false)
        { }

        [CLSCompliant(false)] 
        public Either(B bValue) : this(default(A), bValue, true)
        { }

        [CLSCompliant(false)] 
        public static implicit operator Either<A, B>(A aValue) { return MakeFirst(aValue); }
        [CLSCompliant(false)] 
        public static implicit operator Either<A, B>(B bValue) { return MakeSecond(bValue); }
        [CLSCompliant(false)] 
        public static explicit operator A(Either<A, B> abValue) { return abValue.First; }
        [CLSCompliant(false)] 
        public static explicit operator B(Either<A, B> abValue) { return abValue.Second; }
    }
}