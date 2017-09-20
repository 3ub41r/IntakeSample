namespace IntakeUTM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Programme")]
    public partial class Programme
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string NameMalay { get; set; }

        [StringLength(50)]
        public string ProgrammeType { get; set; }
    }
}
