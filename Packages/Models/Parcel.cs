using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packages.Models
{
    public class Parcel
    {
        [Column("PRC_ID")]
        public int ParcelID { get; set; }

        [Required]
        [Column("PRC_PCGID")]
        public int PackageID { get; set; }

        [Required]
        [Column("PRC_Name")]
        public string ParcelName { get; set; }

        [Required]
        [Column("PRC_DeliveryAdress")]
        public string DeliveryAddress { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column("PRC_CreateDate")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }

        [Required]
        [Column("PRC_Weight")]
        public double Weight { get; set; }
    }
}
