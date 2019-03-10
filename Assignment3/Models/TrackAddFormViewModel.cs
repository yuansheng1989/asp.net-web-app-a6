using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3.Models
{
    public class TrackAddFormViewModel
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "Track name")]
        public string Name { get; set; }

        [StringLength(220)]
        public string Composer { get; set; }

        [Display(Name = "Length (ms)")]
        public int Milliseconds { get; set; }

        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Album")]
        public SelectList AlbumList { get; set; }

        [Display(Name = "Media Type")]
        public SelectList MediaTypeList { get; set; }

        public TrackAddFormViewModel()
        {
            Milliseconds = 0;
            UnitPrice = 0;
        }
    }
}