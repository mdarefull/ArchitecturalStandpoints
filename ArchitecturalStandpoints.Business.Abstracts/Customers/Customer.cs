using Commons.Repository;
using System.ComponentModel.DataAnnotations;

namespace ArchitecturalStandpoints.Customers
{
    public class Customer : EntityBase<string>
    {
        public override string Id { get => CustomerId; set => CustomerId = value; }
        [Key]
        [StringLength(5)]
        public string CustomerId { get; set; }

        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }

        [StringLength(30)]
        public string ContactName { get; set; }

        [StringLength(30)]
        public string ContactTitle { get; set; }

        [StringLength(60)]
        public string Address { get; set; }

        [StringLength(15)]
        public string City { get; set; }

        [StringLength(15)]
        public string Region { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(15)]
        public string Country { get; set; }

        [StringLength(24)]
        [Phone]
        public string Phone { get; set; }

        [StringLength(24)]
        [Phone]
        public string Fax { get; set; }
    }
}
