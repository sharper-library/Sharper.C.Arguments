using System;

namespace Sharper.C.Data
{
    public delegate A Provider<out A>();

    public static class Provider
    {
        public static Provider<A> Singleton<A>(Func<A> create)
        {   var la = new Lazy<A>(create);
            return () => la.Value;
        }

        public static Provider<A> Transient<A>(Func<A> create)
        =>  () => create();

        public static Provider<B> Map<A, B>(this Provider<A> p, Func<A, B> f)
        =>  Transient(() => f(p()));
    }
}
