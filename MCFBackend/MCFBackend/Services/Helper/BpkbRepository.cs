using MCFBackend.Context;
using MCFBackend.Services.Interfaces;
using MCFBackend.Services.Models;
using Microsoft.EntityFrameworkCore;
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
        public async Task<List<ms_storage_location>> GetAllLocationsAsync()
        {
            return await db.ms_storage_location.ToListAsync();
        }

        public async Task<List<tr_bpkb>> GetAllBpkbAsync()
        {
            return await db.tr_bpkb.ToListAsync();
        }
        public async Task<tr_bpkb> GetBpkbByAgreementNumberAsync(string agreementNumber)
        {
            return await db.tr_bpkb.FindAsync(agreementNumber);
        }

        public async Task UpdateBpkbAsync(tr_bpkb bpkb)
        {
            db.tr_bpkb.Update(bpkb);
            await db.SaveChangesAsync();
        }
        public async Task<bool> DeleteBpkbAsync(string agreementNumber)
        {
            var bpkb = await db.tr_bpkb.FindAsync(agreementNumber);
            if (bpkb == null)
            {
                return false;
            }

            db.tr_bpkb.Remove(bpkb);
            await db.SaveChangesAsync();
            return true;
        }

    }
}
