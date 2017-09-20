namespace IntakeUTM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Page")]
    public partial class Page
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string FileLocation { get; set; }

        [StringLength(100)]
        public string FileType { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }

        [StringLength(50)]
        public string Language { get; set; }

        public int? SortOrder { get; set; }

        public int? TemplateId { get; set; }

        public int OfferLetterId { get; set; }
    }
}
