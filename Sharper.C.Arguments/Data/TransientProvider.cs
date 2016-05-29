using System;

namespace Sharper.C.Data
{
    public struct TransientProvider<A>
      : Provider<A>
    {
        private readonly Func<A> create;

        internal TransientProvider(Func<A> create)
        {   this.create = create;
        }

        public A Provide
        =>  create();
    }
}
