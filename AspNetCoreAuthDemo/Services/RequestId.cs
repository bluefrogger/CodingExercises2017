using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreAuthDemo.Services
{
    public interface IRequestId
    {
        string Id { get; }
    }

    public class RequestId : IRequestId
    {
        private string _requestId;

        public RequestId(IRequestIdFactory iRequestIdFactory)
        {
            _requestId = iRequestIdFactory.MakeRequestId();
        }

        public string Id => _requestId;

    }

    public interface IRequestIdFactory
    {
        string MakeRequestId();
    }

    public class RequestIdFactory : IRequestIdFactory
    {
        private int _requestId;

        public string MakeRequestId()
        {
            return Interlocked.Increment(ref _requestId).ToString();
        }
    }
}
