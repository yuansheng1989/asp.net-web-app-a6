using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment3.Models
{
    public class TrackWithDetailViewModel : TrackBaseViewModel
    {
        public string AlbumArtistName { get; set; }
        public string AlbumTitle { get; set; }
        public MediaTypeBaseViewModel MediaType { get; set; }
    }
}