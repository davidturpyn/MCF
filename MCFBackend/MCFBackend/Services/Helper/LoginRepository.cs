using MCFBackend.Context;
using MCFBackend.Services.Dto;
using MCFBackend.Services.Interfaces;
using MCFBackend.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MCFBackend.Services.Helper
{
    public class LoginRepository : ILogin
    {
        private readonly ApplicationContext db;

        public LoginRepository(ApplicationContext db) => this.db = db;

        public async Task<ms_user> GetUserByCredentialsAsync(string userName, string password)
        {
            return await db.ms_user
                .FirstOrDefaultAsync(u => u.user_name == userName && u.password == password);
        }
    }
}
