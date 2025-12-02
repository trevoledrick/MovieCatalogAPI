using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieCatalogAPI.Models.Domain
{
    public class Franchise
    {
        public Franchise()
        {
            this.Movies = new HashSet<Movie>();
        }

        // Primary Key
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}