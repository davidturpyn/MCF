using MCFBackend.Services.Models;

namespace MCFBackend.Services.Interfaces
{
    public interface IBpkb
    {
        Task AddAsync(tr_bpkb trBpkb);
    }
}
