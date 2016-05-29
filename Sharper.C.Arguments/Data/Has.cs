namespace Sharper.C.Data
{
    public interface Has<A>
    {
        Provider<A> GetProvider { get; }
    }

    public static class Has
    {
        public static Provider<A> ObtainProvider<A>(this Has<A> h)
        =>  h.GetProvider;

        public static A Obtain<A>(this Has<A> h)
        =>  h.GetProvider.Provide;

        public static A Obtain<A, T>(this Has<Tagged<A, T>> h)
        =>  h.GetProvider.Provide.Untag;
    }

    public interface HasSingleton<A>
      : Has<A>
    {
        new SingletonProvider<A> GetProvider { get; }
    }

    public interface HasTransient<A>
      : Has<A>
    {
        new TransientProvider<A> GetProvider { get; }
    }

    public interface HasTransientArgs<A>
    {
        TransientProvider<A> Transients { get; }
    }

    public interface HasSingletonArgs<A>
    {
        SingletonProvider<A> Singletons { get; }
    }
}
