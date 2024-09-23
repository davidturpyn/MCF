using System.ComponentModel.DataAnnotations;

namespace MCFFrontend.Models
{
    public class RootBpkbs
    {
        public Bpkbs responseBpkbs { get; set; }
    }
    public class ResponseBpkbs
    {
        public List<tr_bpkb> result { get; set; }
    }
}
