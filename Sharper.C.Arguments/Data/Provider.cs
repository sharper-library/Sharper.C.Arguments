using System;

namespace Sharper.C.Data
{
    public interface Provider<A>
    {
        A Get { get; }
    }

    public static class Provider
    {
        public static SingletonProvider<A> Singleton<A>(Func<A> create)
        =>  new SingletonProvider<A>(create);

        public static TransientProvider<A> Transient<A>(Func<A> create)
        =>  new TransientProvider<A>(create);
    }
}
