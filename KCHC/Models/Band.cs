using System;
using System.Collections.Generic;
using System.Text;

namespace KCHC.Models
{
    public class Band : Artist
    {
        public List<Artist> Members { get; set; }
        public Band(Artist artistdata, List<Artist> members)
        {
            IsABand = true;
            this.PhotoPath = artistdata.PhotoPath;
            this.ExtraDescription = artistdata.ExtraDescription;
            this.DateTime = artistdata.DateTime;
            this.Name = artistdata.Name;   
            this.Description = artistdata.Description;
            this.BandCampAccountUrl = artistdata.BandCampAccountUrl;
            this.YoutubeAccountUrl = artistdata.YoutubeAccountUrl;
            this.SpotifyAccountUrl = artistdata.SpotifyAccountUrl;
            this.SoundcloudAccountUrl = artistdata.SoundcloudAccountUrl; 
            this.ContentImage = artistdata.ContentImage;
            Members = members;
        }

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Band()
        {
        }
    }
}
