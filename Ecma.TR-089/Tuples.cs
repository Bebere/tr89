// Hashing: the hash for a tuple should be some combination of the hashes of its elements.
// In this sample implementation we use a rotate+xor combintation for the hash function.
// The CLI has no bit rotate instructions, and C# has no bit rotate operators, so we
// define a helper here.
// This class could be the base of all Tuple<>'s. In this reference implementation it
// is not as RotateRight is not part of the specification of Tuple<>'s.
namespace System
{
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
}