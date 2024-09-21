using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCFBackend.Services.Models
{
    public class tr_bpkb
    {
        [Key]
        [Column("agreement_number")]
        public string AgreementNumber { get; set; }

        [Column("bpkb_no")]
        public string BpkbNo { get; set; }

        [Column("branch_id")]
        public string BranchId { get; set; }

        [Column("bpkb_date")]
        public DateTime? BpkbDate { get; set; }

        [Column("faktur_no")]
        public string FakturNo { get; set; }

        [Column("faktur_date")]
        public DateTime? FakturDate { get; set; }

        [Column("location_id")]
        public string LocationId { get; set; }

        [Column("police_no")]
        public string PoliceNo { get; set; }

        [Column("bpkb_date_in")]
        public DateTime? BpkbDateIn { get; set; }

        [Column("created_by")]
        public string CreatedBy { get; set; }

        [Column("created_on")]
        public DateTime? CreatedOn { get; set; }

        [Column("last_updated_by")]
        public string LastUpdatedBy { get; set; }

        [Column("last_updated_on")]
        public DateTime? LastUpdatedOn { get; set; }
    }
}
