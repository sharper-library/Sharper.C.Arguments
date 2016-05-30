using System;

namespace Sharper.C.Data
{
    public interface Provider<A>
    {
        A Provide { get; }
        Provider<B> Map<B>(Func<A, B> f);
        Provider<A, T> WithTag<T>();
    }

    public interface Provider<A, T>
      : Provider<Tagged<A, T>>
    {
        Provider<A> WithoutTag { get; }
    }

    public static class Provider
    {
        public static Provider<A> Singleton<A>(Func<A> create)
        {   var la = new Lazy<A>(create);
            return new AProvider<A>(() => la.Value);
        }

        public static Provider<A> Transient<A>(Func<A> create)
        =>  new AProvider<A>(create);

        public static Provider<A> Cast<A, B>(this Provider<B> p)
          where B : A
        =>  new AProvider<A>(() => p.Provide);

        public static Provider<A, T> ToTagged<A, T>(this Provider<Tagged<A, T>> p)
        =>  new AProvider<A, T>(() => p.Provide.Untag);

        private struct AProvider<A>
          : Provider<A>
        {
            private readonly Func<A> provide;

            internal AProvider(Func<A> provide)
            {   this.provide = provide;
            }

            public A Provide
            =>  provide();

            public Provider<B> Map<B>(Func<A, B> f)
            {   var self = this;
                return new AProvider<B>(() => f(self.Provide));
            }

            public Provider<A, T> WithTag<T>()
            =>  new AProvider<A, T>(provide);
        }

        private struct AProvider<A, T>
          : Provider<A, T>
        {
            private readonly Func<A> provide;

            internal AProvider(Func<A> provide)
            {   this.provide = provide;
            }

            public Tagged<A, T> Provide
            => Tagged.Tag<A, T>(provide());

            public Provider<B> Map<B>(Func<Tagged<A, T>, B> f)
            {   var self = this;
                return new AProvider<B>(() => f(self.Provide));
            }

            public Provider<Tagged<A, T>, T1> WithTag<T1>()
            {   var self = this;
                return new AProvider<Tagged<A, T>, T1>(() => self.Provide);
            }

            public Provider<A> WithoutTag
            =>  new AProvider<A>(provide);
        }
    }
}
