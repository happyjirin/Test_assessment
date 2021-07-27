using System;
using System.Collections.Generic;

#nullable disable

namespace Test.Models
{
    public partial class Platform
    {
        public int Id { get; set; }
        public string UniqueName { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual List<Well> well { get; set; }
    }
}
