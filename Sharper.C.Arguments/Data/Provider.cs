using System;

namespace Sharper.C.Data
{
    public struct Provider<A>
    {
        private readonly Func<A> provide;

        internal Provider(Func<A> provide)
        {   this.provide = provide;
        }

        public A Provide
        =>  provide();

        public Provider<B> Map<B>(Func<A, B> f)
        {   var self = this;
            return new Provider<B>(() => f(self.Provide));
        }

        public Provider<Tagged<A, T>> WithTag<T>()
        =>  Map(Tagged.Tag<A, T>);
    }

    public static class Provider
    {
        public static Provider<A> Singleton<A>(Func<A> create)
        {   var la = new Lazy<A>(create);
            return new Provider<A>(() => la.Value);
        }

        public static Provider<A> Transient<A>(Func<A> create)
        =>  new Provider<A>(create);
    }
}
