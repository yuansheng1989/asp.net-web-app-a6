using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment3.Models
{
    public class PlaylistWithDetailViewModel : PlaylistBaseViewModel
    {
        public IEnumerable<TrackBaseViewModel> Tracks { get; set; }
    }
}