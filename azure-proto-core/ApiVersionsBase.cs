using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_core
{
    public class ApiVersionsBase : IEquatable<string>, IComparable<string>//, IEquatable<ApiVersionsBase>, IComparable<ApiVersionsBase>
    {
        private string _value;

        protected static HashSet<string> _validValues = new HashSet<string>();

        protected ApiVersionsBase(string value)
        {
            Validate(value);
            _value = value;
        }

        public static implicit operator string(ApiVersionsBase version)
        {
            return version._value;
        }

        public static implicit operator ApiVersionsBase(string value)
        {
            return new ApiVersionsBase(value);
        }

        public static bool operator ==(ApiVersionsBase first, ApiVersionsBase second)
        {
            if (ReferenceEquals(null, first))
                return ReferenceEquals(null, second);
            if (ReferenceEquals(null, second))
                return false;
            return first.Equals(second);
        }

        public static bool operator !=(ApiVersionsBase first, ApiVersionsBase second)
        {
            if (ReferenceEquals(null, first))
                return !ReferenceEquals(null, second);
            if (ReferenceEquals(null, second))
                return true;
            return !first.Equals(second);
        }

        private static void Validate(string value)
        {
            if (!_validValues.Contains(value))
            {
                throw new ArgumentException($"{value} is not a valid version for VirtualMachines");
            }
        }

        public override string ToString()
        {
            return _value;
        }

        public override bool Equals(object obj)
        {
            if (obj is ApiVersionsBase)
                return Equals(obj as ApiVersionsBase);
            if (obj is string)
                return Equals(obj as string);

            return false;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public bool Equals(string other)
        {
            if (other == null)
                return false;
            return other == _value;
        }

        public int CompareTo(string other)
        {
            if (other == null)
                return 1;
            return DateTime.Parse(_value).CompareTo(DateTime.Parse(other));
        }
    }
}
