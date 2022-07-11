using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packages.Models
{
    public enum PackageState
    {
        Open,
        Closed
    }
    public class Package
    {
        [Column("PCG_ID")]
        public int PackageID { get; set; }

        [Required]
        [Display(Name="Package Name")]
        [Column("PCG_Name")]
        public string PackageName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name="Created")]
        [Column("PCG_CreateDate")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }

        [Required]
        [Column("PCG_State")]
        public PackageState State { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Closed")]
        [Column("PCG_CloseDate")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, NullDisplayText = "No date")]
        public DateTime? CloseDate { get; set; }

        [Required]
        [Display(Name="City Destination")]
        [Column("PCG_CityDestination")]
        public string CityDestination { get; set; }

        public List<Parcel>? Parcels { get; set; }
    }
}
