using System;

namespace Sharper.C.Data
{
    public interface Provider<A>
    {
        A Provide { get; }
    }

    public static class Provider
    {
        public static SingletonProvider<A> Singleton<A>(Func<A> create)
        =>  new SingletonProvider<A>(create);

        public static TransientProvider<A> Transient<A>(Func<A> create)
        =>  new TransientProvider<A>(create);

        public static Func<A, SingletonProvider<B>> Singleton<A, B>(Func<A, B> create)
        =>  a => new SingletonProvider<B>(() => create(a));

        public static Func<A, TransientProvider<B>> Transient<A, B>(Func<A, B> create)
        =>  a => new TransientProvider<B>(() => create(a));
    }
}
