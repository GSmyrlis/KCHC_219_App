using System;
using System.Collections.Generic;
using System.Text;

namespace KCHC.Models
{
    public class Artist
    {
        public string Name { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ContentImage { get; set; } = string.Empty;
        public string SpotifyAccountUrl { get; set; } = string.Empty;
        public string YoutubeAccountUrl { get; set; } = string.Empty;
        public string BandCampAccountUrl { get; set; } = string.Empty;
        public string ExtraDescription { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Not full Constructor. This constructor is only for first page
        /// </summary>
        /// <param name="name"> The Name of the Artist</param>
        /// <param name="photoPath">The photopath of the artist inside this solution</param>
        /// <param name="description">General description about the artist</param>
        public Artist(string name, string photoPath, string description)
        {
            Name = name;
            PhotoPath = photoPath;
            Description = description;
        }

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Artist()
        {
        }
    }
}
