namespace IntakeUTM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OfferLetter")]
    public partial class OfferLetter
    {
        public int Id { get; set; }

        public DateTime? IssuedDate { get; set; }

        [StringLength(50)]
        public string ReferenceNumber { get; set; }

        public int ApplicationId { get; set; }

        public int ApplicationStatusId { get; set; }
    }
}
