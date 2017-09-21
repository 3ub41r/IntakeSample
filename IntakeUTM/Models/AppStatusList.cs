namespace IntakeUTM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppStatusList")]
    public partial class AppStatusList
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? SortOrder { get; set; }
    }
}
