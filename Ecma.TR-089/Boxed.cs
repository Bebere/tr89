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
using System.Runtime.CompilerServices;

namespace System
{
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

        // When converting between Boxed<V> and System.ValueType we preserve null's
        #if FEATURE_ACTION_BOX
            public static implicit operator StrongBox<V>(Boxed<V> box)
            {
                return box == null ? null : new StrongBox<V>(box.item);
            }

            public static implicit operator Boxed<V>(StrongBox<V> sBox)
            {
                return sBox == null ? null : new Boxed<V>(sBox.Value);
            }
        #endif
    }
}