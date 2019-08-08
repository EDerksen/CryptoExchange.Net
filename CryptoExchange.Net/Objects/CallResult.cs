﻿using System;
using System.Collections.Generic;
using System.Net;

namespace CryptoExchange.Net.Objects
{
    /// <summary>
    /// The result of an operation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CallResult<T>
    {
        /// <summary>
        /// The data returned by the call
        /// </summary>
        public T Data { get; internal set; }
        /// <summary>
        /// An error if the call didn't succeed
        /// </summary>
        public Error Error { get; internal set; }
        /// <summary>
        /// Whether the call was successful
        /// </summary>
        public bool Success => Error == null;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="data"></param>
        /// <param name="error"></param>
        public CallResult(T data, Error error)
        {
            Data = data;
            Error = error;
        }
    }

    /// <summary>
    /// The result of a request
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WebCallResult<T>: CallResult<T>
    {
        /// <summary>
        /// The status code of the response. Note that a OK status does not always indicate success, check the Success parameter for this.
        /// </summary>
        public HttpStatusCode? ResponseStatusCode { get; set; }

        /// <summary>
        /// The response headers
        /// </summary>
        public IEnumerable<Tuple<string, string>> ResponseHeaders { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="responseHeaders"></param>
        /// <param name="data"></param>
        /// <param name="error"></param>
        public WebCallResult(HttpStatusCode? code, IEnumerable<Tuple<string, string>> responseHeaders, T data, Error error): base(data, error)
        {
            ResponseHeaders = responseHeaders;
            ResponseStatusCode = code;
        }

        /// <summary>
        /// Create an error result
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static WebCallResult<T> CreateErrorResult(Error error)
        {
            return new WebCallResult<T>(null, null, default(T), error);
        }

        /// <summary>
        /// Create an error result
        /// </summary>
        /// <param name="code"></param>
        /// <param name="responseHeaders"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static WebCallResult<T> CreateErrorResult(HttpStatusCode? code, IEnumerable<Tuple<string, string>> responseHeaders, Error error)
        {
            return new WebCallResult<T>(code, responseHeaders, default(T), error);
        }
    }
}
