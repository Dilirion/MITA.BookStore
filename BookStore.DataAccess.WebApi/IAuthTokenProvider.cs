namespace BookStore.DataAccess.WebApi
{
    public interface IAuthTokenProvider
    {
        string ProvideAuthToken();
    }
}