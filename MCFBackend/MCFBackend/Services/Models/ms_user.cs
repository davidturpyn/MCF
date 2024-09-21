using System.ComponentModel.DataAnnotations;

namespace MCFBackend.Services.Models
{
    public class ms_user
    {
        [Key]
        public Int64 user_id { get; set; }
        public String? user_name { get; set; }
        public String password { get; set; }
        public Boolean is_active { get; set; }
    }
}
