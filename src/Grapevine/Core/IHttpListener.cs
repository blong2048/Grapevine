﻿using System;
using System.Collections.Generic;

namespace Grapevine.Core
{
    /// <summary>
    /// Interface wrapper for a programmatically controlled HTTP protocol listener
    /// </summary>
    /// <typeparam name="TListener"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public interface IHttpListener<out TListener>
    {
        TListener Advanced { get; }

        bool IsListening { get; }

        ICollection<string> Prefixes { get; }

        IAsyncResult BeginGetContext(AsyncCallback callback, object state);

        void Close();

        IHttpContext EndGetContext(IAsyncResult asyncResult);

        void Start();

        void Stop();
    }

    /// <summary>
    /// Wrapper for an instance of System.Net.HttpListener with selective functionality exposed
    /// </summary>
    public class HttpListener : IHttpListener<System.Net.HttpListener>
    {
        public System.Net.HttpListener Advanced { get; }

        public bool IsListening => Advanced.IsListening;

        public ICollection<string> Prefixes => Advanced.Prefixes;

        public HttpListener()
        {
            Advanced = new System.Net.HttpListener();
        }

        public IAsyncResult BeginGetContext(AsyncCallback callback, object state)
        {
            return Advanced.BeginGetContext(callback, state);
        }

        public void Close()
        {
            Advanced.Close();
        }

        public IHttpContext EndGetContext(IAsyncResult asyncResult)
        {
            var result = Advanced.EndGetContext(asyncResult);
            var context = new HttpContext(result);
            return context;
        }

        public void Start()
        {
            Advanced.Start();
        }

        public void Stop()
        {
            Advanced.Stop();
        }
    }
}
