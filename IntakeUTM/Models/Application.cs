namespace IntakeUTM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Application")]
    public partial class Application
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string RefNumber { get; set; }

        public int? ProgrammeId { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [Column(TypeName = "text")]
        public string OfferLetterText { get; set; }

        public int? OfferedProgrammeId { get; set; }

        public int? ApplicationStatusId { get; set; }
    }
}
