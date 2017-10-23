// A public default constructor for a value type always exists.
// By the definition of Either<A, B> the default value is IsFirst is true
// and First is default(A).
namespace System
{
    [Serializable]
    public struct Either<A, B>
    {
        private A aField;
        private B bField;
        private bool isB;

        private Either(A x, B y, bool d)
        {
            aField = x;
            bField = y;
            isB = d;
        }

        public static Either<A, B> MakeFirst(A aValue)
        {
            return new Either<A, B>(aValue, default(B), false);
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
                    throw new System.InvalidOperationException();
                return aField;
            }
        }

        public static Either<A, B> MakeSecond(B bValue)
        {
            return new Either<A, B>(default(A), bValue, true);
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
                    throw new System.InvalidOperationException();
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

        #region unifiable
        // For any Either<A,B> with A==B the following are ambiguous
    #if AllowPotentialAmbiguity
    #warning Ignore CLSCompliant not needed warnings if they occur
        [CLSCompliant(false)] 
        public Either(A aValue)
        { aField = aValue;
          bField = default(B);
          isB = false;
        }

        [CLSCompliant(false)] 
        public Either(B bValue)
        { aField = default(A);
          bField = bValue;
          isB = true;
        }

        [CLSCompliant(false)] 
        public static implicit operator Either<A, B>(A aValue) { return SetFirst(aValue); }
        [CLSCompliant(false)] 
        public static implicit operator Either<A, B>(B bValue) { return SetSecond(bValue); }
        [CLSCompliant(false)] 
        public static explicit operator A(Either<A, B> abValue) { return abValue.First; }
        [CLSCompliant(false)] 
        public static explicit operator B(Either<A, B> abValue) { return abValue.Second; }
    #endif
        #endregion
    }
}