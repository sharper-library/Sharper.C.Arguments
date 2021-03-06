namespace Sharper.C.Data
{
    public interface Has<A>
    {
        Provider<A> GetProvider { get; }
    }

    public interface Has<A, T>
      : Has<Tagged<A, T>>
    {
    }

    public static class Has
    {
        public static Provider<A> ObtainProvider<A>(this Has<A> h)
        =>  h.GetProvider;

        public static Provider<Tagged<A, T>> ObtainProvider<A, T>
          ( this Has<Tagged<A, T>> h
          )
        =>  h.GetProvider;

        public static A Obtain<A>(this Has<A> h)
        =>  h.GetProvider.Provide;

        public static A Obtain<A, T>(this Has<Tagged<A, T>> h)
        =>  h.GetProvider.Provide.Untag;
    }
}
