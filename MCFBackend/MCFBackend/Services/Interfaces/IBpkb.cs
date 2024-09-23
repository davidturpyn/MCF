using MCFBackend.Services.Models;

namespace MCFBackend.Services.Interfaces
{
    public interface IBpkb
    {
        Task AddAsync(tr_bpkb trBpkb);
        Task<List<ms_storage_location>> GetAllLocationsAsync();
        Task<List<tr_bpkb>> GetAllBpkbAsync();
        Task<tr_bpkb> GetBpkbByAgreementNumberAsync(string agreementNumber);
        Task UpdateBpkbAsync(tr_bpkb bpkb);
        Task<bool> DeleteBpkbAsync(string agreementNumber);
    }
}
