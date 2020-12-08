// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.RegularExpressions;

namespace azure_proto_core
{
    public class ApiVersionsBase : IEquatable<string>, IComparable<string>
    {
        private readonly string _value;

        protected ApiVersionsBase(string value)
        {
            _value = value;
        }

        public static implicit operator string(ApiVersionsBase version)
        {
            return version._value;
        }

        public int CompareTo(string other)
        {
            if (other == null)
            {
                return 1;
            }

            var regPattern = @"(\d\d\d\d-\d\d-\d\d)(.*)";

            var otherMatch = Regex.Match(other, regPattern);
            var thisMatch = Regex.Match(_value, regPattern);

            var otherDatePart = otherMatch.Groups[1].Value;
            var thisDatePart = thisMatch.Groups[1].Value;

            if (otherDatePart == thisDatePart)
            {
                var otherPreviewPart = otherMatch.Groups[2].Value;
                var thisPreviewPart = thisMatch.Groups[2].Value;

                if (otherPreviewPart == thisPreviewPart)
                {
                    return 0;
                }

                if (string.IsNullOrEmpty(otherPreviewPart))
                {
                    return -1;
                }

                if (string.IsNullOrEmpty(thisPreviewPart))
                {
                    return 1;
                }

                return thisPreviewPart.CompareTo(otherPreviewPart);
            }

            return thisDatePart.CompareTo(otherDatePart);
        }

        public bool Equals(string other)
        {
            if (other == null)
            {
                return false;
            }

            return other == _value;
        }

        public static bool operator ==(ApiVersionsBase first, string second)
        {
            if (ReferenceEquals(null, first))
            {
                return ReferenceEquals(null, second);
            }

            if (ReferenceEquals(null, second))
            {
                return false;
            }

            return first.Equals(second);
        }

        public static bool operator !=(ApiVersionsBase first, string second)
        {
            if (ReferenceEquals(null, first))
            {
                return !ReferenceEquals(null, second);
            }

            if (ReferenceEquals(null, second))
            {
                return true;
            }

            return !first.Equals(second);
        }

        public override string ToString()
        {
            return _value;
        }

        public override bool Equals(object obj)
        {
            if (obj is ApiVersionsBase)
            {
                return Equals(obj as ApiVersionsBase);
            }

            if (obj is string)
            {
                return Equals(obj as string);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
