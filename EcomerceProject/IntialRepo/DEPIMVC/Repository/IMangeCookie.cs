namespace DEPIMVC.Repository
{
    public interface IMangeCookie
    {
        Task<bool> SetCookie(HttpResponseMessage response, bool rememberMe = false);
    }
}
