// Ecma Common Generics
//
// Reference implementation of Ecma CLI Technical Report "Common Generics"
//
// This implementation serves only to describe the types and their semantics,
// it is not intended to be the way these types must be implemented.
// Note: Conventional implementation guidelines would place each type in its own
// file. We have not done this here for convenience only.
//
// Nigel Perry, New Zealand
//
// Feedback on these types is encouraged.
//
// Email: ecmacli@zoot.net.nz
// Web: www.mondrian-script.org/ecma

// Note: Serializable is not a standard attribute and is not therefore specified in the
// formal specification of these types. However the standard library requires that all
// its types be marked as serializable by some unspecified method, so in this reference
// implementation the [Serializable] attribute is included.

// Note: For some of these types there are useful members which are ambiguous if two or more
// of the type's parameters are instantiated with the same type. A decision needs to be made
// whether allowing this potential ambiguity is acceptable given the benefit. Note that such
// members are non-CLI compliant. Whether these members are included is controlled by the
// following conditional compilation flag
// #define AllowPotentialAmbiguity

//[assembly:System.CLSCompliant(true)] 
namespace System
{
    // The single-valued type.
    [Serializable]
    public enum Unit { Unit };

    #region Tuples

    // Hashing: the hash for a tuple should be some combination of the hashes of its elements.
    // In this sample implementation we use a rotate+xor combintation for the hash function.
    // The CLI has no bit rotate instructions, and C# has no bit rotate operators, so we
    // define a helper here.
    // This class could be the base of all Tuple<>'s. In this reference implementation it
    // is not as RotateRight is not part of the specification of Tuple<>'s.
    internal static class _Tuple
    {
        internal static Int32 RotateRight(Int32 value, Int32 places)
        {
            places &= 0x1F;
            if (places == 0) return value;
            Int32 mask = ~0x7FFFFFFF >> (places - 1);
            return ((value >> places) & ~mask) | ((value << (32 - places)) & mask);
        }
    }

    // The Tuple types are straighforward. All fields are public and no properties are
    // defined to access them, this follows the common usage of tuples.
    // A constructor, Equals, GetHashCode and ToString are defined for each type.
    // Operators == and != are provided as per class library design guidelines.

    // 2-tuple
    // This tuple has two extra methods to allow Tuple<A,B> and KeyValuePair to interwork easily
    [Serializable]
    public struct Tuple<A, B>
    {
        public A ItemA;
        public B ItemB;

        public Tuple(A valueA, B valueB)
        {
            ItemA = valueA;
            ItemB = valueB;
        }

        // convert a Tuple into a KeyValuePair
        public static implicit operator Collections.Generic.KeyValuePair<A, B>(Tuple<A, B> tValue)
        {
            return new Collections.Generic.KeyValuePair<A, B>(tValue.ItemA, tValue.ItemB);
        }

        // convert a KeyValuePair into a Tuple
        public static implicit operator Tuple<A, B>(Collections.Generic.KeyValuePair<A, B> kvValue)
        {
            return new Tuple<A, B>(kvValue.Key, kvValue.Value);
        }

        public static bool operator ==(Tuple<A, B> left, Tuple<A, B> right)
        {
            return left.ItemA.Equals(right.ItemA)
                   && left.ItemB.Equals(right.ItemB);
        }

        public static bool operator !=(Tuple<A, B> left, Tuple<A, B> right)
        {
            return !(left == right);
        }

        public override bool Equals(object other)
        {
            return other is Tuple<A, B> && (this == (Tuple<A, B>)other);
        }

        public override int GetHashCode()
        {
            return ItemA.GetHashCode()
                   ^ _Tuple.RotateRight(ItemB.GetHashCode(), 1);
        }

        public override string ToString()
        {
            return String.Format("({0}, {1})", ItemA, ItemB);
        }
    }

    // 3-tuple
    [Serializable]
    public struct Tuple<A, B, C>
    {
        public A ItemA;
        public B ItemB;
        public C ItemC;

        public Tuple(A valueA, B valueB, C valueC)
        {
            ItemA = valueA;
            ItemB = valueB;
            ItemC = valueC;
        }

        public static bool operator ==(Tuple<A, B, C> left, Tuple<A, B, C> right)
        {
            return left.ItemA.Equals(right.ItemA)
                   && left.ItemB.Equals(right.ItemB)
                   && left.ItemC.Equals(right.ItemC);
        }

        public static bool operator !=(Tuple<A, B, C> left, Tuple<A, B, C> right)
        {
            return !(left == right);
        }

        public override bool Equals(object other)
        {
            return other is Tuple<A, B, C> && (this == (Tuple<A, B, C>)other);
        }

        public override int GetHashCode()
        {
            return ItemA.GetHashCode()
                   ^ _Tuple.RotateRight(ItemB.GetHashCode(), 1)
                   ^ _Tuple.RotateRight(ItemC.GetHashCode(), 2);
        }

        public override string ToString()
        {
            return String.Format("({0}, {1}, {2})", ItemA, ItemB, ItemC);
        }
    }

    // 4-tuple
    [Serializable]
    public struct Tuple<A, B, C, D>
    {
        public A ItemA;
        public B ItemB;
        public C ItemC;
        public D ItemD;

        public Tuple(A valueA, B valueB, C valueC, D valueD)
        {
            ItemA = valueA;
            ItemB = valueB;
            ItemC = valueC;
            ItemD = valueD;
        }

        public static bool operator ==(Tuple<A, B, C, D> left, Tuple<A, B, C, D> right)
        {
            return left.ItemA.Equals(right.ItemA)
                   && left.ItemB.Equals(right.ItemB)
                   && left.ItemC.Equals(right.ItemC)
                   && left.ItemD.Equals(right.ItemD);
        }

        public static bool operator !=(Tuple<A, B, C, D> left, Tuple<A, B, C, D> right)
        {
            return !(left == right);
        }

        public override bool Equals(object other)
        {
            return other is Tuple<A, B, C, D> && (this == (Tuple<A, B, C, D>)other);
        }

        public override int GetHashCode()
        {
            return ItemA.GetHashCode()
                   ^ _Tuple.RotateRight(ItemB.GetHashCode(), 1)
                   ^ _Tuple.RotateRight(ItemC.GetHashCode(), 2)
                   ^ _Tuple.RotateRight(ItemD.GetHashCode(), 3);
        }

        public override string ToString()
        {
            return String.Format("({0}, {1}, {2}, {3})", ItemA, ItemB, ItemC, ItemD);
        }
    }

    // 5-tuple
    [Serializable]
    public struct Tuple<A, B, C, D, E>
    {
        public A ItemA;
        public B ItemB;
        public C ItemC;
        public D ItemD;
        public E ItemE;

        public Tuple(A valueA, B valueB, C valueC, D valueD, E valueE)
        {
            ItemA = valueA;
            ItemB = valueB;
            ItemC = valueC;
            ItemD = valueD;
            ItemE = valueE;
        }

        public static bool operator ==(Tuple<A, B, C, D, E> left, Tuple<A, B, C, D, E> right)
        {
            return left.ItemA.Equals(right.ItemA)
                   && left.ItemB.Equals(right.ItemB)
                   && left.ItemC.Equals(right.ItemC)
                   && left.ItemD.Equals(right.ItemD)
                   && left.ItemE.Equals(right.ItemE);
        }

        public static bool operator !=(Tuple<A, B, C, D, E> left, Tuple<A, B, C, D, E> right)
        {
            return !(left == right);
        }

        public override bool Equals(object other)
        {
            return other is Tuple<A, B, C, D, E> && (this == (Tuple<A, B, C, D, E>)other);
        }

        public override int GetHashCode()
        {
            return ItemA.GetHashCode()
                   ^ _Tuple.RotateRight(ItemB.GetHashCode(), 1)
                   ^ _Tuple.RotateRight(ItemC.GetHashCode(), 2)
                   ^ _Tuple.RotateRight(ItemD.GetHashCode(), 3)
                   ^ _Tuple.RotateRight(ItemE.GetHashCode(), 4);
        }

        public override string ToString()
        {
            return String.Format("({0}, {1}, {2}, {3}, {4})", ItemA, ItemB, ItemC, ItemD, ItemE);
        }
    }

    // 6-tuple
    [Serializable]
    public struct Tuple<A, B, C, D, E, F>
    {
        public A ItemA;
        public B ItemB;
        public C ItemC;
        public D ItemD;
        public E ItemE;
        public F ItemF;

        public Tuple(A valueA, B valueB, C valueC, D valueD, E valueE,
          F valueF)
        {
            ItemA = valueA;
            ItemB = valueB;
            ItemC = valueC;
            ItemD = valueD;
            ItemE = valueE;
            ItemF = valueF;
        }

        public static bool operator ==(Tuple<A, B, C, D, E, F> left, Tuple<A, B, C, D, E, F> right)
        {
            return left.ItemA.Equals(right.ItemA)
                   && left.ItemB.Equals(right.ItemB)
                   && left.ItemC.Equals(right.ItemC)
                   && left.ItemD.Equals(right.ItemD)
                   && left.ItemE.Equals(right.ItemE)
                   && left.ItemF.Equals(right.ItemF);
        }

        public static bool operator !=(Tuple<A, B, C, D, E, F> left, Tuple<A, B, C, D, E, F> right)
        {
            return !(left == right);
        }

        public override bool Equals(object other)
        {
            return other is Tuple<A, B, C, D, E, F> && (this == (Tuple<A, B, C, D, E, F>)other);
        }

        public override int GetHashCode()
        {
            return ItemA.GetHashCode()
                   ^ _Tuple.RotateRight(ItemB.GetHashCode(), 1)
                   ^ _Tuple.RotateRight(ItemC.GetHashCode(), 2)
                   ^ _Tuple.RotateRight(ItemD.GetHashCode(), 3)
                   ^ _Tuple.RotateRight(ItemE.GetHashCode(), 4)
                   ^ _Tuple.RotateRight(ItemF.GetHashCode(), 5);
        }

        public override string ToString()
        {
            return String.Format("({0}, {1}, {2}, {3}, {4}, {5})",
                                 ItemA, ItemB, ItemC, ItemD, ItemE,
                                 ItemF);
        }
    }

    // 7-tuple
    [Serializable]
    public struct Tuple<A, B, C, D, E, F, G>
    {
        public A ItemA;
        public B ItemB;
        public C ItemC;
        public D ItemD;
        public E ItemE;
        public F ItemF;
        public G ItemG;

        public Tuple(A valueA, B valueB, C valueC, D valueD, E valueE,
          F valueF, G valueG)
        {
            ItemA = valueA;
            ItemB = valueB;
            ItemC = valueC;
            ItemD = valueD;
            ItemE = valueE;
            ItemF = valueF;
            ItemG = valueG;
        }

        public static bool operator ==(Tuple<A, B, C, D, E, F, G> left, Tuple<A, B, C, D, E, F, G> right)
        {
            return left.ItemA.Equals(right.ItemA)
                   && left.ItemB.Equals(right.ItemB)
                   && left.ItemC.Equals(right.ItemC)
                   && left.ItemD.Equals(right.ItemD)
                   && left.ItemE.Equals(right.ItemE)
                   && left.ItemF.Equals(right.ItemF)
                   && left.ItemG.Equals(right.ItemG);
        }

        public static bool operator !=(Tuple<A, B, C, D, E, F, G> left, Tuple<A, B, C, D, E, F, G> right)
        {
            return !(left == right);
        }

        public override bool Equals(object other)
        {
            return other is Tuple<A, B, C, D, E, F, G> && (this == (Tuple<A, B, C, D, E, F, G>)other);
        }

        public override int GetHashCode()
        {
            return ItemA.GetHashCode()
                   ^ _Tuple.RotateRight(ItemB.GetHashCode(), 1)
                   ^ _Tuple.RotateRight(ItemC.GetHashCode(), 2)
                   ^ _Tuple.RotateRight(ItemD.GetHashCode(), 3)
                   ^ _Tuple.RotateRight(ItemE.GetHashCode(), 4)
                   ^ _Tuple.RotateRight(ItemF.GetHashCode(), 5)
                   ^ _Tuple.RotateRight(ItemG.GetHashCode(), 6);
        }

        public override string ToString()
        {
            return String.Format("({0}, {1}, {2}, {3}, {4}, {5}, {6})",
                                 ItemA, ItemB, ItemC, ItemD, ItemE,
                                 ItemF, ItemG);
        }
    }

    // 8-tuple
    [Serializable]
    public struct Tuple<A, B, C, D, E, F, G, H>
    {
        public A ItemA;
        public B ItemB;
        public C ItemC;
        public D ItemD;
        public E ItemE;
        public F ItemF;
        public G ItemG;
        public H ItemH;

        public Tuple(A valueA, B valueB, C valueC, D valueD, E valueE,
          F valueF, G valueG, H valueH)
        {
            ItemA = valueA;
            ItemB = valueB;
            ItemC = valueC;
            ItemD = valueD;
            ItemE = valueE;
            ItemF = valueF;
            ItemG = valueG;
            ItemH = valueH;
        }

        public static bool operator ==(Tuple<A, B, C, D, E, F, G, H> left, Tuple<A, B, C, D, E, F, G, H> right)
        {
            return left.ItemA.Equals(right.ItemA)
                   && left.ItemB.Equals(right.ItemB)
                   && left.ItemC.Equals(right.ItemC)
                   && left.ItemD.Equals(right.ItemD)
                   && left.ItemE.Equals(right.ItemE)
                   && left.ItemF.Equals(right.ItemF)
                   && left.ItemG.Equals(right.ItemG)
                   && left.ItemH.Equals(right.ItemH);
        }

        public static bool operator !=(Tuple<A, B, C, D, E, F, G, H> left, Tuple<A, B, C, D, E, F, G, H> right)
        {
            return !(left == right);
        }

        public override bool Equals(object other)
        {
            return other is Tuple<A, B, C, D, E, F, G, H> && (this == (Tuple<A, B, C, D, E, F, G, H>)other);
        }

        public override int GetHashCode()
        {
            return ItemA.GetHashCode()
                   ^ _Tuple.RotateRight(ItemB.GetHashCode(), 1)
                   ^ _Tuple.RotateRight(ItemC.GetHashCode(), 2)
                   ^ _Tuple.RotateRight(ItemD.GetHashCode(), 3)
                   ^ _Tuple.RotateRight(ItemE.GetHashCode(), 4)
                   ^ _Tuple.RotateRight(ItemF.GetHashCode(), 5)
                   ^ _Tuple.RotateRight(ItemG.GetHashCode(), 6)
                   ^ _Tuple.RotateRight(ItemH.GetHashCode(), 7);
        }

        public override string ToString()
        {
            return String.Format("({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})",
                                 ItemA, ItemB, ItemC, ItemD, ItemE,
                                 ItemF, ItemG, ItemH);
        }
    }

    // 9-tuple
    [Serializable]
    public struct Tuple<A, B, C, D, E, F, G, H, I>
    {
        public A ItemA;
        public B ItemB;
        public C ItemC;
        public D ItemD;
        public E ItemE;
        public F ItemF;
        public G ItemG;
        public H ItemH;
        public I ItemI;

        public Tuple(A valueA, B valueB, C valueC, D valueD, E valueE,
          F valueF, G valueG, H valueH, I valueI)
        {
            ItemA = valueA;
            ItemB = valueB;
            ItemC = valueC;
            ItemD = valueD;
            ItemE = valueE;
            ItemF = valueF;
            ItemG = valueG;
            ItemH = valueH;
            ItemI = valueI;
        }

        public static bool operator ==(Tuple<A, B, C, D, E, F, G, H, I> left, Tuple<A, B, C, D, E, F, G, H, I> right)
        {
            return left.ItemA.Equals(right.ItemA)
                   && left.ItemB.Equals(right.ItemB)
                   && left.ItemC.Equals(right.ItemC)
                   && left.ItemD.Equals(right.ItemD)
                   && left.ItemE.Equals(right.ItemE)
                   && left.ItemF.Equals(right.ItemF)
                   && left.ItemG.Equals(right.ItemG)
                   && left.ItemH.Equals(right.ItemH)
                   && left.ItemI.Equals(right.ItemI);
        }

        public static bool operator !=(Tuple<A, B, C, D, E, F, G, H, I> left, Tuple<A, B, C, D, E, F, G, H, I> right)
        {
            return !(left == right);
        }

        public override bool Equals(object other)
        {
            return other is Tuple<A, B, C, D, E, F, G, H, I> && Equals((Tuple<A, B, C, D, E, F, G, H, I>)other);
        }

        public override int GetHashCode()
        {
            return ItemA.GetHashCode()
                   ^ _Tuple.RotateRight(ItemB.GetHashCode(), 1)
                   ^ _Tuple.RotateRight(ItemC.GetHashCode(), 2)
                   ^ _Tuple.RotateRight(ItemD.GetHashCode(), 3)
                   ^ _Tuple.RotateRight(ItemE.GetHashCode(), 4)
                   ^ _Tuple.RotateRight(ItemF.GetHashCode(), 5)
                   ^ _Tuple.RotateRight(ItemG.GetHashCode(), 6)
                   ^ _Tuple.RotateRight(ItemH.GetHashCode(), 7)
                   ^ _Tuple.RotateRight(ItemI.GetHashCode(), 8);
        }

        public override string ToString()
        {
            return String.Format("({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})",
                                 ItemA, ItemB, ItemC, ItemD, ItemE,
                                 ItemF, ItemG, ItemH, ItemI);
        }
    }


    // 1--tuple
    [Serializable]
    public struct Tuple<A, B, C, D, E, F, G, H, I, J>
    {
        public A ItemA;
        public B ItemB;
        public C ItemC;
        public D ItemD;
        public E ItemE;
        public F ItemF;
        public G ItemG;
        public H ItemH;
        public I ItemI;
        public J ItemJ;

        public Tuple(A valueA, B valueB, C valueC, D valueD, E valueE,
          F valueF, G valueG, H valueH, I valueI, J valueJ)
        {
            ItemA = valueA;
            ItemB = valueB;
            ItemC = valueC;
            ItemD = valueD;
            ItemE = valueE;
            ItemF = valueF;
            ItemG = valueG;
            ItemH = valueH;
            ItemI = valueI;
            ItemJ = valueJ;
        }

        public static bool operator ==(Tuple<A, B, C, D, E, F, G, H, I, J> left, Tuple<A, B, C, D, E, F, G, H, I, J> right)
        {
            return left.ItemA.Equals(right.ItemA)
                   && left.ItemB.Equals(right.ItemB)
                   && left.ItemC.Equals(right.ItemC)
                   && left.ItemD.Equals(right.ItemD)
                   && left.ItemE.Equals(right.ItemE)
                   && left.ItemF.Equals(right.ItemF)
                   && left.ItemG.Equals(right.ItemG)
                   && left.ItemH.Equals(right.ItemH)
                   && left.ItemI.Equals(right.ItemI)
                   && left.ItemJ.Equals(right.ItemJ);
        }

        public static bool operator !=(Tuple<A, B, C, D, E, F, G, H, I, J> left, Tuple<A, B, C, D, E, F, G, H, I, J> right)
        {
            return !(left == right);
        }

        public override bool Equals(object other)
        {
            return other is Tuple<A, B, C, D, E, F, G, H, I, J> && Equals((Tuple<A, B, C, D, E, F, G, H, I, J>)other);
        }

        public override int GetHashCode()
        {
            return ItemA.GetHashCode()
                   ^ _Tuple.RotateRight(ItemB.GetHashCode(), 1)
                   ^ _Tuple.RotateRight(ItemC.GetHashCode(), 2)
                   ^ _Tuple.RotateRight(ItemD.GetHashCode(), 3)
                   ^ _Tuple.RotateRight(ItemE.GetHashCode(), 4)
                   ^ _Tuple.RotateRight(ItemF.GetHashCode(), 5)
                   ^ _Tuple.RotateRight(ItemG.GetHashCode(), 6)
                   ^ _Tuple.RotateRight(ItemH.GetHashCode(), 7)
                   ^ _Tuple.RotateRight(ItemI.GetHashCode(), 8)
                   ^ _Tuple.RotateRight(ItemJ.GetHashCode(), 9);
        }

        public override string ToString()
        {
            return String.Format("({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9})",
                                 ItemA, ItemB, ItemC, ItemD, ItemE,
                                 ItemF, ItemG, ItemH, ItemI, ItemJ);
        }
    }

    #endregion

    #region Either
    // A public default constructor for a value type always exists.
    // By the definition of Either<A, B> the default value is IsFirst is true
    // and First is default(A).

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
    #endregion

    #region Optional
    // Optional<T> is similar to Nullable<T> except that it is not limited to T being a value type.

    [Serializable]
    public struct Optional<T> : IComparable, IComparable<Optional<T>>, INullableValue
    { // fields are private
        private T myValue;
        private bool isValid;

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
            if (isValid)
                return myValue;
            else
                return default(T);
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

        // INullableValue

        object INullableValue.Value
        {
            get
            {
                if (isValid)
                    return myValue;
                else
                    throw new InvalidOperationException();
            }
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

    #endregion

    #region Function

    // Standard delegates to use for function values

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
    #endregion

    #region Action

    // Standard delegates to use for procedure values
    //
    // Note: void Action<A>(A argA) is already defined in System, it is included
    // here as a comment for completeness.

    public delegate void Action();
    // public delegate void Action<A>(A argA);
    public delegate void Action<A, B>(A argA, B argB);
    public delegate void Action<A, B, C>(A argA, B argB, C argC);
    public delegate void Action<A, B, C, D>(A argA, B argB, C argC, D argD);
    public delegate void Action<A, B, C, D, E>(A argA, B argB, C argC, D argD, E argE);

    #endregion

    #region Boxed
    // This type is not part of the proposal.
    // ======================================
    //
    // Boxed<V> is a boxed version of the value type V, it is
    // similar to a CLI boxed value type (from the 'box' instruction).
    // The key feature is that it is named, the CLI does not (yet) have
    // named boxed types. This type is not being suggested for standardisation.
    //
    // The implicit operators allow Boxed<V>, V, and a System.ValueType which
    // is a CLI boxed V to be used interchangeably.

    [Serializable]
    class Boxed<V> where V : struct
    {
        private V item;

        // Constructors

        public Boxed()
        {
            item = default(V);
        }

        public Boxed(V value)
        {
            item = value;
        }

        // Accessor

        public V Value
        {
            get { return item; }
        }

        // Implicit conversion operators

        // This might throw NullReferenceException so the operator should
        // be explicit by the guidelines. However it appears to be overkill
        // requiring an explicit cast in this case so we bend the guidelines...
        public static implicit operator V(Boxed<V> box)
        {
            return box.item;
        }

        public static implicit operator Boxed<V>(V value)
        {
            return new Boxed<V>(value);
        }

        // When converting between Boxed<V> and System.ValueType we preserve null's

        public static implicit operator ValueType(Boxed<V> box)
        {
            return box == null ? null : (ValueType)box.item;
        }

        public static implicit operator Boxed<V>(ValueType vtBox)
        {
            return vtBox == null ? null : new Boxed<V>((V)vtBox);
        }
    }

    #endregion
}