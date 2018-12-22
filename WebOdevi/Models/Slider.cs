namespace WebOdevi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slider")]
    public partial class Slider
    {
        public int SliderId { get; set; }

        [StringLength(50)]
        public string SliderText { get; set; }

        [StringLength(50)]
        public string SliderFoto { get; set; }
    }
}
