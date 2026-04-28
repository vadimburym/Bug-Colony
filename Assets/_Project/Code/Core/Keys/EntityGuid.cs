using System;

namespace _Project.Code.Core.Keys
{
    [Serializable]
    public readonly struct EntityGUID : IEquatable<EntityGUID>, IComparable<EntityGUID>
    {
        public static readonly EntityGUID Null = new EntityGUID(0);

        public long Value { get; }

        public bool IsNull => Value == 0;

        public EntityGUID(long value)
        {
            Value = value;
        }

        public bool Equals(EntityGUID other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is EntityGUID other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public int CompareTo(EntityGUID other)
        {
            return Value.CompareTo(other.Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static bool operator ==(EntityGUID left, EntityGUID right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(EntityGUID left, EntityGUID right)
        {
            return !left.Equals(right);
        }

        public static bool operator <(EntityGUID left, EntityGUID right)
        {
            return left.Value < right.Value;
        }

        public static bool operator >(EntityGUID left, EntityGUID right)
        {
            return left.Value > right.Value;
        }

        public static bool operator <=(EntityGUID left, EntityGUID right)
        {
            return left.Value <= right.Value;
        }

        public static bool operator >=(EntityGUID left, EntityGUID right)
        {
            return left.Value >= right.Value;
        }

        public static implicit operator long(EntityGUID id)
        {
            return id.Value;
        }

        public static explicit operator EntityGUID(long value)
        {
            return new EntityGUID(value);
        }
    }
}