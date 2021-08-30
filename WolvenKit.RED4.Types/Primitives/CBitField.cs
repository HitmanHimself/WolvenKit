using System;

namespace WolvenKit.RED4.Types
{
    public class CBitField<T> : IRedEnum<T>, IEquatable<CBitField<T>> where T : Enum
    {
        public T Value { get; set; }

        public Enum GetValue() => Value;
        public void SetValue(object value) => Value = (T)value;

        public static implicit operator CBitField<T>(T value) => new() { Value = value };
        public static implicit operator Enum(CBitField<T> value) => value.Value;


        public override bool Equals(object obj)
        {
            if (obj is CBitField<T> cObj)
            {
                return Equals(cObj);
            }

            return false;
        }

        public bool Equals(CBitField<T> other)
        {
            if (other == null)
            {
                return false;
            }

            return Value.Equals(other.Value);
        }
    }
}
