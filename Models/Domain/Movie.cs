using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieholicAPI.Models.Domain
{
    public class Movie
    {
        public Movie()
        {
            this.Characters = new HashSet<Character>();
            this.ReleaseYear = DateTime.Now.Year;
        }

        // Primary Key
        [Key]
        public int MovieId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(25)]
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }

        [MaxLength(50)]
        public string Director { get; set; }
        public string Picture { get; set; }
        public string Trailer { get; set; }

        public virtual ICollection<Character> Characters { get; set; }

        // Foreign Key
        public int? FranchiseId { get; set; }
        public Franchise Franchise { get; set; }
    }
}