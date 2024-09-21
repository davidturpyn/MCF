using System.ComponentModel.DataAnnotations;

namespace MCFBackend.Services.Models
{
    public class ms_storage_location
    {
        [Key]

        public String location_id { get; set; }
        public String location_name { get; set; }
    }
}
