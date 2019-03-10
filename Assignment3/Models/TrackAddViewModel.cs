using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment3.Models
{
    public class TrackAddViewModel
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(220)]
        public string Composer { get; set; }

        public int Milliseconds { get; set; }

        public decimal UnitPrice { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int AlbumId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int MediaTypeId { get; set; }

        public TrackAddViewModel()
        {
            Milliseconds = 0;
            UnitPrice = 0;
        }
    }
}