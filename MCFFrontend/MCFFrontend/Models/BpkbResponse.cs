namespace MCFFrontend.Models
{
    public class BpkbResponse
    {
        public Data data { get; set; }
    }
    public class Data
    {
        public user user { get; set; }
        public locations Locations { get; set; }
    }
}
