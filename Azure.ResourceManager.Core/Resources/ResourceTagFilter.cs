// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.ResourceManager.Core.Resources
{
    /// <summary>
    /// A class representing a tag filter used in Azure API calls.
    /// </summary>
    public class ResourceTagFilter : GenericResourceFilter
    {
        private readonly Tuple<string, string> _tag;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceTagFilter"/> class.
        /// </summary>
        /// <param name="tag"> The tag to filter by. </param>
        public ResourceTagFilter(Tuple<string, string> tag)
        {
            _tag = tag;
            Key = _tag.Item1;
            Value = _tag.Item2;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceTagFilter"/> class.
        /// </summary>
        /// <param name="tagKey"> The key of the tag to filter by. </param>
        /// <param name="tagValue">The value of the tag to filter by. </param>
        public ResourceTagFilter(string tagKey, string tagValue)
            : this(new Tuple<string, string>(tagKey, tagValue))
        {
        }

        /// <summary>
        /// Gets the key to filter by.
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Gets the value to filter by.
        /// </summary>
        public string Value { get; }

        /// <inheritdoc/>
        public override bool Equals(string other)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override bool Equals(GenericResourceFilter other)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override string GetFilterString()
        {
            return $"tagName eq '{_tag.Item1}' and tagValue eq '{_tag.Item2}'";
        }
    }
}
