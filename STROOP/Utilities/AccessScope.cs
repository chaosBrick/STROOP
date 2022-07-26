using System;
using System.Collections.Generic;

namespace STROOP
{
    public class AccessScope<T> : IDisposable
    {
        public static T content => scopes.Count == 0 ? default(T) : scopes.Peek().obj;

        static Stack<AccessScope<T>> scopes = new Stack<AccessScope<T>>();
        T obj;

        public AccessScope(T obj)
        {
            this.obj = obj;
            scopes.Push(this);
        }

        void IDisposable.Dispose()
        {
            if (scopes.Pop() != this)
                throw new Exception($"Scopes must be disposed in reverse creation order.");
        }
    }

    public abstract class Scope : IDisposable
    {
        protected Scope() { }
        protected abstract void Close();
        void IDisposable.Dispose() => Close();
    }
}
