namespace Hyperai
{
    public static class HyperaiServerOptionsBuilderExtensions
    {
        public static HyperaiServerOptionsBuilder Use<TMiddleware>(this HyperaiServerOptionsBuilder builder)
        {
            return builder.Use(typeof(TMiddleware));
        }
    }
}
