using System;
using System.Collections.Generic;

namespace STROOP.Utilities
{
    public class IgnoreScope : IDisposable
    {
        Stack<IgnoreScope> scopeStack = new Stack<IgnoreScope>();

        IgnoreScope parent;

        public IgnoreScope() { }

        private IgnoreScope(IgnoreScope parent)
        {
            this.parent = parent;
        }

        public IgnoreScope New()
        {
            var newScope = new IgnoreScope(this);
            scopeStack.Push(newScope);
            return newScope;
        }

        void IDisposable.Dispose()
        {
            if (parent.scopeStack.Pop() != this)
                throw new InvalidOperationException("IgnoreScopes popped in invalid order");
        }

        public bool ignore => scopeStack.Count > 0;

        public static implicit operator bool(IgnoreScope ignoreScope) => ignoreScope.ignore;
    }
}
