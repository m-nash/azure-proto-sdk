#nullable enable

using Azure;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace azure_proto_core.Adapters
{
    /// <summary>
    /// Returns an AsyncPageable that executes a given task before retrieving the first page of results
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PhTaskDeferringAsyncPageable<T> : AsyncPageable<T> where T : notnull
    {
        Func<Task<AsyncPageable<T>>> _task;
        public PhTaskDeferringAsyncPageable(Func<Task<AsyncPageable<T>>> task)
        {
            _task = task;
        }
        public async override IAsyncEnumerable<Page<T>> AsPages(string continuationToken = null, int? pageSizeHint = null)
        {
            await foreach (var page in (await _task()).AsPages())
            {
                yield return page;
            }
        }
    }

    /// <summary>
    /// This class allows performing conversions on pages of data as they are accessed - used in the prototype to convett between underlying model types and the new model types that extend Resource, 
    /// and also for returning Operations classes for those underlying objects.
    /// </summary>
    /// <typeparam name="T">The type parameter of the Pageable we are wrapping</typeparam>
    /// <typeparam name="U">The desired type parameter of the returned pageable</typeparam>
    public class PhWrappingPageable<T, U> : Pageable<U> where U : class where T : class
    {
        IEnumerable<Pageable<T>> _wrapped;
        Func<T, U> _converter;

        public PhWrappingPageable(Pageable<T> wrapped, Func<T, U> converter)
        {
            _wrapped = new[] { wrapped };
            _converter = converter;
        }

        public PhWrappingPageable(IEnumerable<Pageable<T>> wrapped, Func<T, U> converter)
        {
            _wrapped = wrapped;
            _converter = converter;
        }
        public override IEnumerable<Page<U>> AsPages(string continuationToken = null, int? pageSizeHint = null)
        {
            foreach (var pages in _wrapped)
            {
                foreach (var page in pages.AsPages())
                {
                    yield return new WrappingPage<T, U>(page, _converter);
                }
            }
        }
    }

    /// <summary>
    /// As above, returns an AsyncPageable that transforms each page of contents  after they are retrieved from the server accorgin to the profived transformation function
    /// </summary>
    /// <typeparam name="T">The generic type parameter of the underlying wrapped AsyncPageable</typeparam>
    /// <typeparam name="U">The desired generic type parameter fo the returned AsyncPageable</typeparam>
    public class PhWrappingAsyncPageable<T, U> : AsyncPageable<U> where U: class where T: class
    {
        IEnumerable<AsyncPageable<T>> _wrapped;
        Func<T, U> _converter;
        public PhWrappingAsyncPageable(AsyncPageable<T> wrapped, Func<T, U> converter)
        {
            _wrapped = new[] { wrapped };
            _converter = converter;
        }

        public PhWrappingAsyncPageable(IEnumerable<AsyncPageable<T>> wrapped, Func<T, U> converter)
        {
            _wrapped = wrapped;
            _converter = converter;
        }

        public async override IAsyncEnumerable<Page<U>> AsPages( string continuationToken = null, int? pageSizeHint = null)
        {
            foreach (var pageEnum in _wrapped)
            {
                await foreach (var page in pageEnum.AsPages().WithCancellation(CancellationToken))
                {
                    yield return new WrappingPage<T, U>(page, _converter);
                }
            }
        }
    }

    internal class WrappingPage<T, U> : Page<U> where U : class where T : class
    {
        Page<T> _wrapped;
        Func<T, U> _converter;

        internal WrappingPage(Page<T> wrapped, Func<T, U> converter)
        {
            _wrapped = wrapped;
            _converter = converter;
        }

        public override IReadOnlyList<U> Values => _wrapped.Values.Select(_converter).ToImmutableList();

        public override string ContinuationToken => _wrapped.ContinuationToken;

        public override Response GetRawResponse()
        {
            return _wrapped.GetRawResponse();
        }
    }
}
