namespace IntakeUTM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PagesTemplate")]
    public partial class PagesTemplate
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string FileLocation { get; set; }

        public int? SortOrder { get; set; }

        [StringLength(50)]
        public string FileType { get; set; }

        [StringLength(50)]
        public string Language { get; set; }

        public int? AppStatusListId { get; set; }

        public int? ProgrammeId { get; set; }

        [Column(TypeName = "text")]
        public string ContentText { get; set; }

        [StringLength(500)]
        public string FileUrl { get; set; }
    }
}
