using Azure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core.Adapters
{
    public class MultiCallAsyncPageable<T, U> : AsyncPageable<U> where T: notnull where U: notnull
    {
        IAsyncEnumerable<T> _source;
        Func<T, AsyncPageable<U>> _converter;
        public MultiCallAsyncPageable(IAsyncEnumerable<T> source, Func<T, AsyncPageable<U>> converter)
        {
            _source = source;
            _converter = converter;
        }
        public MultiCallAsyncPageable(IEnumerable<T> source, Func<T, AsyncPageable<U>> converter)
        {
            _source = new SyncAsyncEnumerable<T>(source);
            _converter = converter;
        }


        public async override IAsyncEnumerable<Page<U>> AsPages(string continuationToken = null, int? pageSizeHint = null)
        {
            await foreach (var item in _source)
            {
                await foreach (var page in _converter(item).AsPages(continuationToken, pageSizeHint).ConfigureAwait(false))
                {
                    yield return page;
                }
            }
        }
    }

    public class SyncAsyncEnumerable<T> : IAsyncEnumerable<T>
    {
        IEnumerable<T> _enumerable;

        public SyncAsyncEnumerable(IEnumerable<T> enumerable)
        {
            _enumerable = enumerable;
        }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new SyncAsyncEnumerator<T>(_enumerable.GetEnumerator());
        }
    }

    public class SyncAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        IEnumerator<T> _enumerator;

        public SyncAsyncEnumerator(IEnumerator<T> enumerator)
        {
            _enumerator = enumerator;
        }
        public T Current => _enumerator.Current;

        public ValueTask DisposeAsync()
        {
            return new ValueTask();
        }

        public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(_enumerator.MoveNext());
    }

    public class MultiCallPageable<T, U> : Pageable<U> where T : notnull where U : notnull
    {
        IEnumerable<T> _source;
        Func<T, Pageable<U>> _converter;
        public MultiCallPageable(IEnumerable<T> source, Func<T, Pageable<U>> converter)
        {
            _source = source;
            _converter = converter;
        }

        public override IEnumerable<Page<U>> AsPages(string continuationToken = null, int? pageSizeHint = null)
        {
            foreach (var item in _source)
            {
                foreach (var page in _converter(item).AsPages(continuationToken, pageSizeHint))
                {
                    yield return page;
                }
            }
        }
    }
    public class TaskDeferringAsyncPageable<T> : AsyncPageable<T> where T : notnull
    {
        Func<Task<AsyncPageable<T>>> _task;
        public TaskDeferringAsyncPageable(Func<Task<AsyncPageable<T>>> task)
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
    public class WrappingPageable<T, U> : Pageable<U> where U : class where T : class
    {
        IEnumerable<Pageable<T>> _wrapped;
        Func<T, U> _converter;

        public WrappingPageable(Pageable<T> wrapped, Func<T, U> converter)
        {
            _wrapped = new[] { wrapped };
            _converter = converter;
        }

        public WrappingPageable(IEnumerable<Pageable<T>> wrapped, Func<T, U> converter)
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

 
    public class WrappingAsyncPageable<T, U> : AsyncPageable<U> where U: class where T: class
    {
        IEnumerable<AsyncPageable<T>> _wrapped;
        Func<T, U> _converter;
        public WrappingAsyncPageable(AsyncPageable<T> wrapped, Func<T, U> converter)
        {
            _wrapped = new[] { wrapped };
            _converter = converter;
        }

        public WrappingAsyncPageable(IEnumerable<AsyncPageable<T>> wrapped, Func<T, U> converter)
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
