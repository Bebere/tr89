using System.Runtime.CompilerServices;

#if (!NET20 && !NET35)
    [assembly: TypeForwardedTo(typeof(System.Tuple<,>))]
    [assembly: TypeForwardedTo(typeof(System.Tuple<,,>))]
    [assembly: TypeForwardedTo(typeof(System.Tuple<,,,>))]
    [assembly: TypeForwardedTo(typeof(System.Tuple<,,,,>))]
    [assembly: TypeForwardedTo(typeof(System.Tuple<,,,,,>))]
    [assembly: TypeForwardedTo(typeof(System.Tuple<,,,,,,>))]
    [assembly: TypeForwardedTo(typeof(System.Tuple<,,,,,,,>))]
#endif
namespace System
{
    // The Tuple types are straighforward. All fields are accessed through
    // readonly properties and are classes, as in the .Net Framework's
    // implementation.
    // A constructor, Equals, GetHashCode and ToString are defined for each type.
    // Operators == and != are provided as per class library design guidelines.
#if (NET20 || NET35)
    // Hashing: the hash for a tuple should be some combination of the hashes of its elements.
    // In this sample implementation we use a rotate+xor combintation for the hash function.
    // The CLI has no bit rotate instructions, and C# has no bit rotate operators, so we
    // define a helper here.
    // This class is here the base of all Tuple<>'s, with each inheriting from the previous one.
    public abstract class _Tuple
    {
        protected static Int32 RotateRight(Int32 value)
        {
            var tmp = (UInt32)value;
            tmp = (tmp >> 1) | (tmp << (31));
            return (Int32)tmp;
        }

        public static bool operator ==(_Tuple left, _Tuple right)
        {
            return left == null ? right == null : left.Equals(right);
        }

        public static bool operator !=(_Tuple left, _Tuple right)
        {
            return !(left == right);
        }

        public abstract override bool Equals(object other);

        public abstract override int GetHashCode();
    }

    // 2-tuple
    // This tuple has two extra methods to allow Tuple<A,B> and KeyValuePair to interwork easily
    [Serializable]
    public class Tuple<A, B> : _Tuple
    {
        private readonly A _item1;
        private readonly B _item2;

        public A Item1
        {
            get
            {
                return _item1;
            }
        }
        public B Item2
        {
            get
            {
                return _item2;
            }
        }

        public Tuple(A valueA, B valueB)
        {
            _item1 = valueA;
            _item2 = valueB;
        }

        // convert a Tuple into a KeyValuePair
        public static implicit operator Collections.Generic.KeyValuePair<A, B>(Tuple<A, B> tValue)
        {
            return new Collections.Generic.KeyValuePair<A, B>(tValue._item1, tValue._item2);
        }

        // convert a KeyValuePair into a Tuple
        public static implicit operator Tuple<A, B>(Collections.Generic.KeyValuePair<A, B> kvValue)
        {
            return new Tuple<A, B>(kvValue.Key, kvValue.Value);
        }

        public override bool Equals(object other)
        {
            var buf = other as Tuple<A, B>;
            return buf != null
                && _item1.Equals(buf._item1)
                && _item2.Equals(buf._item2);
        }

        public override int GetHashCode()
        {
            return _item1.GetHashCode()
                   ^ RotateRight(_item2.GetHashCode());
        }

        public override string ToString()
        {
            return String.Format("({0}, {1})", _item1, _item2);
        }
    }

    // 3-tuple
    [Serializable]
    public class Tuple<A, B, C> : Tuple<A, B>
    {
        private readonly C _item3;

        public C Item3
        {
            get
            {
                return _item3;
            }
        }

        public Tuple(A valueA, B valueB, C valueC) : base(valueA, valueB)
        {
            _item3 = valueC;
        }

        public override bool Equals(object other)
        {
            var tmp = other as Tuple<A, B, C>;
            return tmp != null && _item3.Equals(tmp._item3) && ((Tuple<A, B>)this).Equals(tmp);
        }

        public override int GetHashCode()
        {
            return _item3.GetHashCode()
                   ^ RotateRight(base.GetHashCode());
        }

        public override string ToString()
        {
            return String.Format("({0}, {1}, {2})", Item1, Item2, _item3);
        }
    }

    // 4-tuple
    [Serializable]
    public class Tuple<A, B, C, D> : Tuple<A, B, C>
    {
        private readonly D _item4;

        public D Item4
        {
            get
            {
                return _item4;
            }
        }

        public Tuple(A valueA, B valueB, C valueC, D valueD) : base(valueA, valueB, valueC)
        {
            _item4 = valueD;
        }

        public override bool Equals(object other)
        {
            var tmp = other as Tuple<A, B, C, D>;
            return tmp != null && _item4.Equals(tmp._item4) && ((Tuple<A, B, C>)this).Equals(tmp);
        }

        public override int GetHashCode()
        {
            return _item4.GetHashCode()
                   ^ RotateRight(base.GetHashCode());
        }

        public override string ToString()
        {
            return String.Format("({0}, {1}, {2}, {3})", Item1, Item2, Item3, _item4);
        }
    }

    // 5-tuple
    [Serializable]
    public class Tuple<A, B, C, D, E> : Tuple<A, B, C, D>
    {
        private readonly E _item5;

        public E Item5
        {
            get
            {
                return _item5;
            }
        }

        public Tuple(A valueA, B valueB, C valueC, D valueD, E valueE) : base(valueA, valueB, valueC, valueD)
        {
            _item5 = valueE;
        }

        public override bool Equals(object other)
        {
            var tmp = other as Tuple<A, B, C, D, E>;
            return tmp != null && _item5.Equals(tmp._item5) && ((Tuple<A, B, C, D>)this).Equals(tmp);
        }

        public override int GetHashCode()
        {
            return _item5.GetHashCode()
                   ^ RotateRight(base.GetHashCode());
        }

        public override string ToString()
        {
            return String.Format("({0}, {1}, {2}, {3}, {4})", Item1, Item2, Item3, Item4, _item5);
        }
    }

    // 6-tuple
    [Serializable]
    public class Tuple<A, B, C, D, E, F> : Tuple<A, B, C, D, E>
    {
        private readonly F _item6;

        public F Item6
        {
            get
            {
                return _item6;
            }
        }

        public Tuple(A valueA, B valueB, C valueC, D valueD, E valueE,
          F valueF) : base(valueA, valueB, valueC, valueD, valueE)
        {
            _item6 = valueF;
        }

        public override bool Equals(object other)
        {
            var tmp = other as Tuple<A, B, C, D, E, F>;
            return tmp != null && _item6.Equals(tmp._item6) && ((Tuple<A, B, C, D, E>)this).Equals(tmp);
        }

        public override int GetHashCode()
        {
            return _item6.GetHashCode()
                   ^ RotateRight(base.GetHashCode());
        }

        public override string ToString()
        {
            return String.Format("({0}, {1}, {2}, {3}, {4}, {5})",
                                 Item1, Item2, Item3, Item4, Item5,
                                 _item6);
        }
    }

    // 7-tuple
    [Serializable]
    public class Tuple<A, B, C, D, E, F, G> : Tuple<A, B, C, D, E, F>
    {
        private readonly G _item7;

        public G Item7
        {
            get
            {
                return _item7;
            }
        }

        public Tuple(A valueA, B valueB, C valueC, D valueD, E valueE,
          F valueF, G valueG) : base(valueA, valueB, valueC, valueD, valueE, valueF)
        {
            _item7 = valueG;
        }

        public override bool Equals(object other)
        {
            var tmp = other as Tuple<A, B, C, D, E, F, G>;
            return tmp != null && _item7.Equals(tmp._item7) && ((Tuple<A, B, C, D, E, F>)this).Equals(tmp);
        }

        public override int GetHashCode()
        {
            return _item7.GetHashCode()
                   ^ _Tuple.RotateRight(base.GetHashCode());
        }

        public override string ToString()
        {
            return String.Format("({0}, {1}, {2}, {3}, {4}, {5}, {6})",
                                 Item1, Item2, Item3, Item4, Item5,
                                 Item6, _item7);
        }
    }

    // 8-tuple
    [Serializable]
    public class Tuple<A, B, C, D, E, F, G, H> : Tuple<A, B, C, D, E, F, G>
    {
        private readonly H _item8;

        public H Item8
        {
            get
            {
                return _item8;
            }
        }

        public Tuple(A valueA, B valueB, C valueC, D valueD, E valueE,
          F valueF, G valueG, H valueH) : base(valueA, valueB, valueC, valueD, valueE, valueF, valueG)
        {
            _item8 = valueH;
        }

        public override bool Equals(object other)
        {
            var tmp = other as Tuple<A, B, C, D, E, F, G, H>;
            return tmp != null && _item8.Equals(tmp._item8) && ((Tuple<A, B, C, D, E, F, G>)this).Equals(tmp);
        }

        public override int GetHashCode()
        {
            return _item8.GetHashCode()
                   ^ RotateRight(base.GetHashCode());
        }

        public override string ToString()
        {
            return String.Format("({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})",
                                 Item1, Item2, Item3, Item4, Item5,
                                 Item6, Item7, _item8);
        }
    }
#endif

    // 9-tuple
    [Serializable]
    public class Tuple<A, B, C, D, E, F, G, H, I> : Tuple<A, B, C, D, E, F, G, H>
    {
        private readonly I _item9;

        public I Item9
        {
            get
            {
                return _item9;
            }
        }

        public Tuple(A valueA, B valueB, C valueC, D valueD, E valueE,
          F valueF, G valueG, H valueH, I valueI) : base(valueA, valueB, valueC, valueD, valueE, valueF, valueG, valueH)
        {
            _item9 = valueI;
        }

#if (!NET20 && !NET35)
        public H Item8
        {
            get
            {
                return Rest;
            }
        }

        public static bool operator ==(Tuple<A, B, C, D, E, F, G, H, I> left, Tuple<A, B, C, D, E, F, G, H, I> right)
        {
            return left == null ? right == null : left.Equals(right);
        }

        public static bool operator !=(Tuple<A, B, C, D, E, F, G, H, I> left, Tuple<A, B, C, D, E, F, G, H, I> right)
        {
            return !(left == right);
        }
        
        private static Int32 RotateHelper(Int32 value)
        {
            var tmp = (UInt32)value;
            tmp = (tmp >> 1) | (tmp << (31));
            return (Int32)tmp;
        }

        public override int GetHashCode()
        {
            return _item9.GetHashCode()
                   ^ RotateHelper(base.GetHashCode());
        }
#else
        public override int GetHashCode()
        {
            return _item9.GetHashCode()
                   ^ RotateRight(base.GetHashCode());
        }
#endif
        public override bool Equals(object other)
        {
            var tmp = other as Tuple<A, B, C, D, E, F, G, H, I>;
            return tmp != null && _item9.Equals(tmp._item9) && ((Tuple<A, B, C, D, E, F, G, H>)this).Equals(tmp);
        }

        public override string ToString()
        {
            return String.Format("({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})",
                                 Item1, Item2, Item3, Item4, Item5,
                                 Item6, Item7, Item8, _item9);
        }
    }

    // 10-tuple
    [Serializable]
    public class Tuple<A, B, C, D, E, F, G, H, I, J> : Tuple<A, B, C, D, E, F, G, H, I>
    {
        private readonly J _item10;

        public J Item10
        {
            get
            {
                return _item10;
            }
        }

        public Tuple(A valueA, B valueB, C valueC, D valueD, E valueE,
          F valueF, G valueG, H valueH, I valueI, J valueJ) : base(valueA, valueB, valueC, valueD, valueE, valueF, valueG, valueH, valueI)
        {
            _item10 = valueJ;
        }

        public override bool Equals(object other)
        {
            var tmp = other as Tuple<A, B, C, D, E, F, G, H, I, J>;
            return tmp != null && _item10.Equals(tmp._item10) && ((Tuple<A, B, C, D, E, F, G, H, I>)this).Equals(tmp);
        }

#if (!NET20 && !NET35)
        public static bool operator ==(Tuple<A, B, C, D, E, F, G, H, I, J> left, Tuple<A, B, C, D, E, F, G, H, I, J> right)
        {
            return left == null ? right == null : left.Equals(right);
        }

        public static bool operator !=(Tuple<A, B, C, D, E, F, G, H, I, J> left, Tuple<A, B, C, D, E, F, G, H, I, J> right)
        {
            return !(left == right);
        }

        private static Int32 RotateHelper(Int32 value)
        {
            var tmp = (UInt32)value;
            tmp = (tmp >> 1) | (tmp << (31));
            return (Int32)tmp;
        }

        public override int GetHashCode()
        {
            return _item10.GetHashCode()
                   ^ RotateHelper(base.GetHashCode());
        }
#else
        public override int GetHashCode()
        {
            return _item10.GetHashCode()
                   ^ RotateRight(base.GetHashCode());
        }
#endif

        public override string ToString()
        {
            return String.Format("({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9})",
                                 Item1, Item2, Item3, Item4, Item5,
                                 Item6, Item7, Item8, Item9, _item10);
        }
    }
}