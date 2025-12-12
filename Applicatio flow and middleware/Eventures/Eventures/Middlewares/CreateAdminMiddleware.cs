namespace Eventures.Middlewares
{
    public static class CreateAdminMiddleware
    {
        public static IApplicationBuilder UseMyCreateAdminMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CreateAdmin>();
        }
    }
}
