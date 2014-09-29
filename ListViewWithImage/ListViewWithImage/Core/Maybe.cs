using System;
using System.Collections.Generic;

namespace Runtime.Kernel.Core
{
    public class Maybe<T> : IEquatable<Maybe<T>>
    {
        #region Static Fields

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
        public static readonly Maybe<T> Empty = new Maybe<T>(default(T), false);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
        public static readonly Maybe<T> Nothing = new Maybe<T>();

        #endregion

        #region Constructors and Destructors

        internal Maybe()
        {
            HasValue = false;
        }

        internal Maybe(T value, bool hasValue = true)
        {
            Value = value;
            HasValue = hasValue;
        }

        #endregion

        #region Public Properties

        public bool HasValue { get; private set; }
        public bool HasNotValue { get { return !HasValue; } }

        public T Value { get; private set; }

        #endregion

        #region Public Methods and Operators

        public static bool operator ==(Maybe<T> left,
                                       Maybe<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Maybe<T> left,
                                       Maybe<T> right)
        {
            return !Equals(left, right);
        }

        public bool Equals(Maybe<T> other)
        {
            if(ReferenceEquals(null, other))
            {
                return false;
            }
            if(ReferenceEquals(this, other))
            {
                return true;
            }

            if(HasValue != other.HasValue)
            {
                return false;
            }
            if(!HasValue)
            {
                return true;
            }
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj))
            {
                return false;
            }
            if(ReferenceEquals(this, obj))
            {
                return true;
            }

            var maybe = obj as Maybe<T>;
            return maybe != null && Equals(maybe);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T>.Default.GetHashCode(Value) * 397) ^ HasValue.GetHashCode();
            }
        }

        #endregion
    }
}