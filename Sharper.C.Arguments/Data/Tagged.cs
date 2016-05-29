namespace Sharper.C.Data
{
    public struct Tagged<A, T>
    {
        public A Untag { get; }

        internal Tagged(A a)
        {   Untag = a;
        }
    }

    public static class Tagged
    {
        public static Tagged<A, T> Tag<A, T>(A a)
        =>  new Tagged<A, T>(a);
    }
}
