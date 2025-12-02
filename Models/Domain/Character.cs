using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieCatalogAPI.Models.Domain
{
    public class Character
    {
        // Primary Key
        [Key]
        public int CharacterId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }

        [MaxLength(50)]
        public string Alias { get; set; }

        [MaxLength(10)]
        public string Gender { get; set; }
        public string Picture { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}