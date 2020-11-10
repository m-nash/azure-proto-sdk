using System;
using System.Text.RegularExpressions;

namespace azure_proto_core
{
    public class ApiVersionsBase : IEquatable<string>, IComparable<string>
    {
        private static readonly string RegPattern = @"(\d\d\d\d-\d\d-\d\d)(.*)";

        private readonly string _value;

        protected ApiVersionsBase(string value)
        {
            _value = value;
        }

        public static implicit operator string(ApiVersionsBase version)
        {
            return version._value;
        }

        public static bool operator ==(ApiVersionsBase first, string second)
        {
            if (ReferenceEquals(null, first))
                return ReferenceEquals(null, second);
            if (ReferenceEquals(null, second))
                return false;
            return first.Equals(second);
        }

        public static bool operator !=(ApiVersionsBase first, string second)
        {
            if (ReferenceEquals(null, first))
                return !ReferenceEquals(null, second);
            if (ReferenceEquals(null, second))
                return true;
            return !first.Equals(second);
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

            Match otherMatch = Regex.Match(other, RegPattern);
            Match thisMatch = Regex.Match(_value, RegPattern);

            string otherDatePart = otherMatch.Groups[1].Value;
            string thisDatePart = thisMatch.Groups[1].Value;

            if(otherDatePart == thisDatePart)
            {
                string otherPreviewPart = otherMatch.Groups[2].Value;
                string thisPreviewPart = thisMatch.Groups[2].Value;
                if (otherPreviewPart == thisPreviewPart)
                    return 0;

                if (String.IsNullOrEmpty(otherPreviewPart))
                    return -1;

                if (String.IsNullOrEmpty(thisPreviewPart))
                    return 1;

                return thisPreviewPart.CompareTo(otherPreviewPart);
            }
            else
            {
                return thisDatePart.CompareTo(otherDatePart);
            }
        }
    }
}
