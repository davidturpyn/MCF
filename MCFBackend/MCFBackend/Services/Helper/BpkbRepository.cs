using MCFBackend.Context;
using MCFBackend.Services.Interfaces;
using MCFBackend.Services.Models;
using static MCFBackend.Services.Helper.BpkbRepository;

namespace MCFBackend.Services.Helper
{
    public class BpkbRepository : IBpkb
    {
        private readonly ApplicationContext db;

        public BpkbRepository(ApplicationContext db) => this.db = db;

        public async Task AddAsync(tr_bpkb trBpkb)
        {
            await db.tr_bpkb.AddAsync(trBpkb);
            await db.SaveChangesAsync();
        }
    }
}
