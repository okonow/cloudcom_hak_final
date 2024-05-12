namespace Domain.Common
{
    public abstract class ValueObject
    {
        protected static bool EqualOperator(ValueObject? left, ValueObject? right)
        {
            if (left is null || right is null)
            {
                return false;
            }
            return ReferenceEquals(left, right) || left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObject? left, ValueObject? right)
        {
            return !(EqualOperator(left, right));
        }

        public static bool operator ==(ValueObject? one, ValueObject? two)
        {
            return EqualOperator(one, two);
        }

        public static bool operator !=(ValueObject? one, ValueObject? two)
        {
            return NotEqualOperator(one, two);
        }

        public override bool Equals(object? obj)
        {
            var right = obj as ValueObject
                ?? throw new InvalidCastException();

            return EqualOperator(this, right);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
