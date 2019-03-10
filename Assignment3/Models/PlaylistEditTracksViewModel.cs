using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment3.Models
{
    public class PlaylistEditTracksViewModel
    {
        [Key]
        public int PlaylistId { get; set; }

        public IEnumerable<int> TrackIds { get; set; }
    }
}