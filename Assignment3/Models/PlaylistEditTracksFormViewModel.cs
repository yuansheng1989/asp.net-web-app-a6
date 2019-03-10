using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3.Models
{
    public class PlaylistEditTracksFormViewModel
    {
        [Key]
        public int PlaylistId { get; set; }

        [StringLength(120)]
        public string Name { get; set; }

        public MultiSelectList TrackList { get; set; }

        public IEnumerable<TrackBaseViewModel> TracksExisted { get; set; }
    }
}