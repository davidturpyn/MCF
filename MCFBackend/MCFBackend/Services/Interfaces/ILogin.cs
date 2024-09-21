using MCFBackend.Services.Models;

namespace MCFBackend.Services.Interfaces
{
    public interface ILogin
    {
        Task<ms_user> GetUserByCredentialsAsync(string userName, string password);
        Task<List<ms_storage_location>> GetAllLocationsAsync();
    }
}
