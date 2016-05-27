using System;

namespace Sharper.C.Data
{
    public struct SingletonProvider<A>
      : Provider<A>
    {
        private readonly Lazy<A> value;

        internal SingletonProvider(Func<A> create)
        {   value = new Lazy<A>(create);
        }

        public A Get
        =>  value.Value;
    }
}
