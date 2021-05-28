using System;
using System.Collections.Generic;

namespace STROOP
{
    public class AccessScope<T> : IDisposable
    {
        public static T content => scopes.Count == 0 ? default(T) : scopes.Peek().obj;

        static Stack<AccessScope<T>> scopes = new Stack<AccessScope<T>>();
        T obj;

        public AccessScope(T archive)
        {
            obj = archive;
            scopes.Push(this);
        }

        void IDisposable.Dispose()
        {
            if (scopes.Pop() != this)
                throw new Exception($"Please dispose your scopes in reverse creation order.");
        }
    }
}
