// Optional<T> is similar to Nullable<T> except that it is not limited to T being a value type.
namespace System
{
    [Serializable]
    public struct Optional<T> : IComparable, IComparable<Optional<T>>
    { // fields are private
        private readonly T myValue;
        private readonly bool isValid;

        // Constructors
        // The implicit nullary constructor is equivalent to:
        //    public Optional() { isValid = false; }

        public Optional(T x)
        {
            myValue = x;
            isValid = true;
        }

        // Accessors
        public bool HasValue
        {
            get { return isValid; }
        }

        public T Value
        {
            get
            {
                if (isValid)
                    return myValue;
                else
                    throw new InvalidOperationException();
            }
        }

        public T GetValueOrDefault()
        {
            return GetValueOrDefault(default(T));
        }

        public T GetValueOrDefault(T alternateDefaultValue)
        {
            if (isValid)
                return myValue;
            else
                return alternateDefaultValue;
        }

        // Interfaces
        //
        // The definitions mirror those of Nullable

        // IComparable

        public int CompareTo(object other)
        {
            if (!(other is Optional<T>))
                throw new ArgumentException();

            Optional<T> x = (Optional<T>)other;

            if (isValid)
            {
                if (x.isValid)
                {
                    if (!(myValue is IComparable))
                        throw new InvalidOperationException();
                    else
                        return ((IComparable)myValue).CompareTo(x.myValue);
                }
                else
                    return +1;
            }
            else
                return x.isValid ? -1 : 0;
        }

        // IComparable<Optional<T>>

        public int CompareTo(Optional<T> other)
        {
            if (isValid)
            {
                if (other.isValid)
                {
                    if (!(myValue is IComparable<T>))
                    {
                        if (!(myValue is IComparable))
                            throw new InvalidOperationException();
                        else
                            return ((IComparable)myValue).CompareTo(other.myValue);
                    }
                    else
                        return ((IComparable<T>)myValue).CompareTo(other.myValue);
                }
                else
                    return +1;
            }
            else
                return other.isValid ? -1 : 0;
        }

        public bool Equals(Optional<T> other)
        {
            return this == other;
        }

        // Conversion operators

        public static implicit operator Optional<T>(T value)
        {
            return new Optional<T>(value);
        }

        public static explicit operator T(Optional<T> value)
        {
            return value.Value;
        }

        // for CLS compliance

        public static Optional<T> ToOptional(T value)
        {
            return new Optional<T>(value);
        }

        public static T FromOptional(Optional<T> value)
        {
            return value.Value;
        }

        // Support conversion to/from Nullable.
        // These cannot be operators as (a) the parameter of this class is not
        // constrained to be a value type and (b) generic operators are not allowed.
        // These would be better as operators on Nullable itself.

        public static Optional<U> FromNullable<U>(Nullable<U> value) where U : struct
        {
            if (value.HasValue)
                return new Optional<U>(value.Value);
            else
                return new Optional<U>();
        }

        public static Nullable<U> ToNullable<U>(Optional<U> value) where U : struct
        {
            if (value.HasValue)
                return new Nullable<U>(value.Value);
            else
                return new Nullable<U>();
        }

        // Class library design guideline operators

        public static bool operator ==(Optional<T> left, Optional<T> right)
        {
            if (left.isValid == right.isValid)
                return left.isValid ? left.myValue.Equals(right.myValue) : true;
            return false;
        }

        public static bool operator !=(Optional<T> left, Optional<T> right)
        {
            return !(left == right);
        }

        // Standard overrides

        public override bool Equals(object other)
        {
            return (other is Optional<T>) && (this == (Optional<T>)other);
        }

        public override int GetHashCode()
        {
            return isValid ? myValue.GetHashCode() : 0;
        }

        public override string ToString()
        {
            return isValid ? myValue.ToString() : String.Empty;
        }
    }
}